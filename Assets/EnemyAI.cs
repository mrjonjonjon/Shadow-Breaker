using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{
    
    public Transform target;
    public Seeker seeker;   
    public AIDestinationSetter aidestset;
    public AIPath aipath;
    Rigidbody2D rb;
    public float test=1.0f;
    Transform player;
    public float radius=3f;
     Animator anim;
    GameObject followPoint;
    public bool canFlip=true;
    public float aggroRange=10f;
    public bool aggroed=false;

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


       // InvokeRepeating("UpdatePath",0f,0.5f);
        InvokeRepeating("UpdateFollowPoint",0f,2f);
       // InvokeRepeating("Attack",0f,2f);

      
        if(MovementController.instance!=null){
            player=MovementController.instance.transform;
        }
    }


    void UpdateFollowPoint(){

         if(Vector3.Distance(transform.position,player.position)<aggroRange){
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
       }else{
        followPoint.transform.position=transform.position;
       }
       
     
       
   
    }

    public void Attack(){
       
            anim.SetTrigger("attack");
            StartCoroutine(DashTowardsPlayer());
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
       // if(laser.laserActive){aipath.canMove=false;}
      
        if(IsPlayerInPosition()){
            Attack();
        }

        if(anim.GetCurrentAnimatorStateInfo(0).IsName("attack")||anim.GetCurrentAnimatorStateInfo(0).IsName("charge")||anim.GetCurrentAnimatorStateInfo(0).IsName("hurt")){
            aipath.canMove=false;
            canFlip=false;
        }else{
            aipath.canMove=true;
            canFlip=true;
        }
   
        if(aipath.velocity.magnitude<0.00001f){
            anim.SetBool("running",false);
        }else{
            anim.SetBool("running",true);
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

       
        if (hit.collider != null)
        {
          return true;
        }

          hit = Physics2D.Raycast(transform.position, Vector2.right,radius,LayerMask.GetMask("PlayerHurtbox"));

       
        if (hit.collider != null)
        {
          return true;
        }
        return false;
    }

   
}
