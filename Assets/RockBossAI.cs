using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RockBossAI : MonoBehaviour
{
    
    public Transform target;


    public Seeker seeker;   
    public AIDestinationSetter aidestset;
    public AIPath aipath;

    public float warningTime=1f;
    public float laserHoldTime=1f;

    Rigidbody2D rb;
    public float test=1.0f;
    Transform player;
    public float radius=3f;
     Animator anim;
    GameObject followPoint;
    public bool canFlip=true;
    ReflectionLaser laser;
    EnergyBallShooter ebs;
    Exploder exploder;
    AudioSource audioSource;
    public Transform headPosition;

    public AudioClip warningSFX;

    [ColorUsage(true, true)]
     public Color warningColor;

      [ColorUsage(true, true)]
     public Color shootColor;

      bool doneWithLaserAttack=true;

      public Collider2D arena;
    // Start is called before the first frame update

    
    void Start()
    {
        seeker=GetComponent<Seeker>();
        rb=GetComponent<Rigidbody2D>();
       aidestset=GetComponent<AIDestinationSetter>();
       aipath=GetComponent<AIPath>();
       anim= GetComponent<Animator>();
        target=MovementController.instance.transform;
         followPoint = new GameObject();
        aidestset.target=followPoint.transform;


        laser=GetComponent<ReflectionLaser>();
        ebs=GetComponent<EnergyBallShooter>();
        exploder=GetComponent<Exploder>();

        audioSource=GetComponent<AudioSource>();

        InvokeRepeating("UpdateFollowPoint",0f,2f);
        InvokeRepeating("Attack",0f,5f);

        if(MovementController.instance!=null){
            player=MovementController.instance.transform;
        }
    }


    void UpdateFollowPoint(){
        int numFollowPoints = player.Find("FollowPoints").childCount;
        int nextFollowPointIndex= Random.Range(0,numFollowPoints);


         switch (Random.Range(0,4)){
            case 0:  
                followPoint.transform.position = player.position + Vector3.left*radius;
                break;
            case 1:
                followPoint.transform.position = player.position + Vector3.right*radius;
                break;
            case 2:
                 followPoint.transform.position = player.position + (Vector3)Random.insideUnitCircle.normalized*radius;
                break;
            default:
                followPoint.transform.position = player.position + (Vector3)Random.insideUnitCircle.normalized*radius;
                break;
            }
     
       
   
    }


    public void BOSSShootLaser(){
        doneWithLaserAttack=false;
        StartCoroutine(LaserCoroutine());
    }

    public void BOSSShootEnergyBalls(){
                doneWithLaserAttack=false;

        for(int i=0;i<10;i++){
            ebs.ShootEnergyBall(headPosition.position,Random.insideUnitCircle,5f);
        }
        doneWithLaserAttack=true;
    }

    public void BOSSSpawnExplosions(){
                doneWithLaserAttack=false;

       StartCoroutine(SpawnExplosions());
    }

    public IEnumerator SpawnExplosions(){
         for(int i=0;i<20;i++){
            exploder.SpawnExplosion(Extensions.RandomPointInBounds(arena.bounds));
            yield return new WaitForSecondsRealtime(0.2f);
        }
        doneWithLaserAttack=true;
    }
    public IEnumerator LaserCoroutine2(){
        
        int numRevolutions=1;
        float timer=0f;
        float totaltime=10f;
        float angle=0f;
        while(angle<360f*numRevolutions){
             Vector2 dir=(Vector2)(Quaternion.Euler(0,0,angle) * Vector2.right);
             laser.ShootLaser(headPosition.position,dir,true,false,2);
             angle=Mathf.Lerp(0f,360f*numRevolutions,timer/totaltime);
             timer+=Time.deltaTime;
             yield return null;
        }
        laser.RemoveLaser();
        doneWithLaserAttack=true;
       

    }
    public IEnumerator LaserCoroutine(){
        float timer=0f;
        Vector2 shotDir=ChooseDirection().normalized;
        Vector3 startPos=headPosition.position;
      
        //shoot warning laser
        laser.SetColor(warningColor);
        while(timer<warningTime){
            laser.ShootLaser(headPosition.position,shotDir,true,false);
            timer+=Time.deltaTime;
            yield return null;
        }

        //shoot actual laser
        laser.SetColor(shootColor);

        timer=0f;
        while(timer<laserHoldTime){
             laser.ShootLaser(headPosition.position,shotDir,true,false);
             timer+=Time.deltaTime;
             yield return null;
        }
        
        laser.RemoveLaser();
        doneWithLaserAttack=true;
    }

    public void PlayAudioClip(AudioClip ac){
        audioSource.PlayOneShot(ac);
    }

    public void Melee(){
        anim.SetTrigger("melee");
    }

    public void Attack(){
      // anim.SetTrigger("laser");
       //return;
       if(!doneWithLaserAttack){return;}
       anim.SetInteger("random",Random.Range(0,3));
       anim.SetTrigger("attack");
       return;




           switch (Random.Range(0,2)){
            case 0:  
               anim.SetTrigger("laser");
                break;
            case 1:
                BOSSShootEnergyBalls();
                break;
            default:
                followPoint.transform.position = player.position + (Vector3)Random.insideUnitCircle.normalized*radius;
                break;
            }
     
    }

    public IEnumerator DashTowardsPlayer(){
            float timer=0;
            float totaltime=0.5f;
            Vector3 startPos= transform.position;
            /*
            while(transform.position!=MovementController.instance.transform.position){
                Vector3 temp= Vector3.Lerp(transform.position,MovementController.instance.transform.position,timer/totaltime);
                rb.MovePosition(temp); 
                timer+=Time.deltaTime;
                yield return null;
            }
            */
            yield return null;
           
            

    }
    // Update is called once per frame
    void Update()
    {
anim.SetBool("donewithlaserattack",doneWithLaserAttack);
      

        if(IsPlayerInPosition()){
           // Attack();
        }

      

        if(canFlip){
                if(Vector3.Dot(MovementController.instance.transform.position-transform.position,Vector3.right)>0.01f){
                    transform.localScale= new Vector3(1f,1f,1f);
                }else{
                    transform.localScale= new Vector3(-1f,1f,1f);

                }
        }

               
    }

    bool IsPlayerInPosition(){
         RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left,radius,LayerMask.GetMask("PlayerHurtbox"));

       
        if (hit.collider != null){
          return true;
        }

        hit = Physics2D.Raycast(transform.position, Vector2.right,radius,LayerMask.GetMask("PlayerHurtbox"));

       
        if (hit.collider != null){
          return true;
        }
        return false;
    }

    Vector2 ChooseDirection(){
        return (Vector2)(MovementController.instance.transform.position-transform.position);
            switch (Random.Range(0,4)){
                    case 0:  
                        return new Vector2(1,1);
                        break;
                    case 1:
                        return new Vector2(-1,1);
                        break;
                    case 2:
                        return new Vector2(1,-1);
                        break;
                    default:
                        return new Vector2(-1,-1);
                        break;
            }
     
    }

   
}
