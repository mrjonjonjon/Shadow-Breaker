using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class HitBoxScript : MonoBehaviour
{


     void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, 1);
    }
    // Start is called before the first frame update
    public float amountOfHitstop=0.1f;
    public float whiteFlashTime=0.05f;
    public AudioClip hitSFX;
    public bool applyingHitstop=false;
     Coroutine hitStopCoroutine=null;
     public Material flashMaterial;
     public GameObject hitFX;
     public float screenshakeIntensity=1.0f;
     public int numHits=1;
     public string targetTag="enemy";
      Animator anim;
     //public bool shadowClone=false;

      ComboManager comboManager;
    [SerializeField]
     bool inContact=false;

     public AudioSource audioSource;
    void Start(){
        audioSource=GetComponent<AudioSource>();
        comboManager=transform.parent.GetComponent<ComboManager>();
        anim=transform.parent.Find("sprite").GetComponent<Animator>();
    }
   

   
    public void applyHitstop(){
    
       if(hitStopCoroutine!=null)StopCoroutine(hitStopCoroutine);

        hitStopCoroutine = StartCoroutine( HitStop() );
 
    }

    public IEnumerator FlashWhite(GameObject go){
        Material oc =   go.GetComponent<SpriteRenderer>().material;
        go.GetComponent<SpriteRenderer>().material=flashMaterial;
        yield return new WaitForSecondsRealtime(amountOfHitstop);
        go.GetComponent<SpriteRenderer>().material=oc;
    }


public IEnumerator HitStop(){
    Time.timeScale = 0f;
    applyingHitstop=true;
          yield return new WaitForSecondsRealtime(amountOfHitstop);
          applyingHitstop=false;
          Time.timeScale=1f;
          hitStopCoroutine=null;
}
    // Update is called once per frame
    void LateUpdate()
    {
        float multiplier = transform.parent.Find("sprite").GetComponent<SpriteRenderer>().flipX?-1:1;
          if (MovementController.instance.isAttacking){
            GetComponent<BoxCollider2D>().offset= new Vector2(GetComponent<BoxCollider2D>().offset.x*multiplier,GetComponent<BoxCollider2D>().offset.y);
          }
    }

    public IEnumerator AllEffects(Collider2D col){
        int count=0;
        while(inContact &&count<numHits  && !MovementController.instance.GetComponent<SuperMoveScript>().isPlayingSuperMove){
              Instantiate(hitFX,col.transform.position,hitFX.transform.rotation);
           // StartCoroutine(FlashWhite(col.gameObject));
           col.GetComponent<Damageable>().StartCoroutine( col.GetComponent<Damageable>().FlashWhite(whiteFlashTime));
              applyHitstop();
              col.GetComponent<Damageable>().TakeDamage(transform.position);
              audioSource.PlayOneShot(hitSFX);
           if(transform.parent.Find("ImpulseSource")!=null)transform.parent.Find("ImpulseSource").GetComponent<CinemachineImpulseSource>().GenerateImpulse(Vector3.up*screenshakeIntensity);


              //numHitstops+=1f;
              count++;

           yield return new WaitUntil(() => !applyingHitstop);
           
                yield return null;
           
                  
                  //  yield return null;

;
        }

    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("breakable")){
             col.GetComponent<Destructible>().Cut();
        }
        if(col.CompareTag("foliage")){
           MovementController.instance.GetComponent<CutController>().Cut(col.gameObject);
        } 
        
         if(col.CompareTag(targetTag)){
            inContact=true;
        
           if(comboManager){
                comboManager.IncreaseComboCount(1);
           }
           //note, damageable must be on base gameobject, hurtbox must be child. hitboxscript must also be child.
          if(transform.parent.Find("hurtbox").GetComponent<Damageable>()!=null){
            if(transform.parent.Find("hurtbox").GetComponent<Damageable>().parryable && col.transform.GetComponent<Damageable>().parrying){
               
                transform.parent.Find("hurtbox").GetComponent<Damageable>().Parried();
            return;
            }else{
                           ;

            }
          }
          StartCoroutine(AllEffects(col));
        }
    }

    void OnTriggerExit2D(Collider2D col ){
         if(col.CompareTag(targetTag)){
               inContact=false;
         }
      
    }
}
