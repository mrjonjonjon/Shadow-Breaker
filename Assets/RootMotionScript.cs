using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class RootMotionScript : MonoBehaviour {    
   public bool applyingAttackRootMotion=false;
   public IEnumerator AttackRootMotionCoroutine;
            public MovementController mc;
            public Animator anim;
           // Rigidbody2D rb;
            void Start(){
                 Application.targetFrameRate = 60;
                mc=GetComponent<MovementController>();
                anim=GetComponent<Animator>();
                //rb=GetComponent<Rigidbody2D>();
                AttackRootMotionCoroutine=ApplyAttackRootMotion();
            }
    IEnumerator ApplyAttackRootMotion(){
       
        //rb.velocity=Vector3.zero;
        applyingAttackRootMotion=true;
        while(mc.isAttacking){
           
                           // rb.velocity= (Vector2) anim.deltaPosition / Time.unscaledDeltaTime;
                           transform.position+=anim.deltaPosition;

        
            yield return null;
        }
        applyingAttackRootMotion=false;
              // rb.velocity=Vector3.zero;
                                AttackRootMotionCoroutine=ApplyAttackRootMotion();


    }
public void test(){
    StopCoroutine(AttackRootMotionCoroutine);
    AttackRootMotionCoroutine=ApplyAttackRootMotion();

    applyingAttackRootMotion=false;
                    //rb.velocity=Vector3.zero;

}
    void OnAnimatorMove(){
       if(mc.isAttacking){
              
            if(Input.GetAxisRaw("Horizontal")==0 && Input.GetAxisRaw("Vertical")==0){
               // rb.velocity=Vector3.zero;
            }else{
               
              // anim.ApplyBuiltinRootMotion();
              if(mc.isDashing){
                 // mc.StopCoroutine(mc.DashCoroutine); 
                //  mc.DashCoroutine=mc.Dash();
                //  mc.isDashing=false;
                
              }
             
              if(!applyingAttackRootMotion){
                 
                  AttackRootMotionCoroutine=ApplyAttackRootMotion();
                 StartCoroutine(AttackRootMotionCoroutine);

              }
                 //rb.velocity+= (Vector2) anim.deltaPosition / Time.deltaTime;
               //print("cne");
           }
          
       }else if(mc.canMove){
                          anim.ApplyBuiltinRootMotion();

                            // rb.isKinematic=false;
                            Vector2 temp=(Vector2)anim.deltaPosition;
                            float temp2=Time.unscaledDeltaTime;
                    if(temp2>0 && !mc.isDashing){
                        //  rb.velocity+= temp/temp2;
                    }
                  
            }else{
                  //rb.velocity+= (Vector2) anim.deltaPosition / Time.deltaTime;
               
               
                anim.ApplyBuiltinRootMotion();
              
        }
                       

 
    }
}

