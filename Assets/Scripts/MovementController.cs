using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
public static MovementController instance;



//public PlayerState _movementState;// = PlayerState._idleMovementState;
#region components
public enum Direction {up,down,left,right};
public IEnumerator DashCoroutine;
public InputManager _inputManager;
public Animator anim;
public AudioSource audioSource;
public AudioClip dashSFX;
public ResourceManager rsm;
public RootMotionScript rms;
public Physics phys;
public SpriteRenderer _spriteRenderer;
public HookshotScript _hookShot;
public HitBoxScript _hitBoxScript;
#endregion


public float dashMultiplier=3f;
public float dashTime=0.5f;
public float dashDistance=1f;
public Vector3 jumpvel;
public Vector2 directionBuffer=Vector2.zero;
public float inputX;
public float inputY;
public bool hookPress=false;
public bool jumpPress=false;
public bool dashPress = false;
public bool attackBuffer=false;

//so you can input dash while applying hitstop
public bool dashBuffer=false;



public Direction lastDirection=Direction.down;
public float playerSpeed=1.0f;
public float staticPlayerSpeed;

//deltas for Update. Fixed-Update deltas stored in physics component
public Vector3 lastPos;
public Vector3 deltaPos;
public Transform originalParent;



float attackHeldDownTime=0f;
float attackHeldDownBuffer=0f;

#region restrictions
public bool canMove=true;
public bool canAttack=true;
public bool lockDirection=false;
#endregion

#region states
public bool isAttacking=false;
public bool isDashing=false;
public bool isRolling=false;
public bool parrying=false;
public bool jumping=false;
#endregion

public InputState _currentInputState;


    void Start()
    {
        originalParent=transform.parent;
        GetComponent<MovementController>().enabled=true;
       
        if(instance==null){
            instance=this;
        }
       
       anim=GetComponent<Animator>();
       rms=GetComponent<RootMotionScript>();
       rsm=GetComponent<ResourceManager>();
       audioSource=GetComponent<AudioSource>();
       phys=GetComponent<Physics>();
       _hookShot = transform.Find("hookshot").GetComponent<HookshotScript>();
       _spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
       _hitBoxScript = transform.Find("Hitbox").GetComponent<HitBoxScript>();
       _inputManager = GetComponent<InputManager>();
       lastDirection =Direction.down;
       staticPlayerSpeed=playerSpeed;
      // _movementState = PlayerState._idleMovementState;
    }
//note: canmove reserved for locking movement by dialogue systems, gameplay events,etc

public void handleSprintHold(){
    if(_currentInputState.sprintHold){
        playerSpeed=2*staticPlayerSpeed;
    }else{
        playerSpeed = staticPlayerSpeed;
    }
}

public void UpdateDeltaPos(){
   
    Vector3 currentPos = phys.position;
    deltaPos = currentPos - lastPos;
    lastPos = currentPos;
}


//sets hookpress
public void handleHook(){
 if(_currentInputState.hookPress){
    print("PRESSED HOOK");
    //hookPress=false;
    _hookShot.Shoot();
 }
}

//sets jumppress and jumps
public void handleJumping(){
    if(_currentInputState.jumpPress){

        //Consume jump
        //jumpPress=false;

        anim.SetTrigger("jump");
        jumping=true;
        //airborne
        if(phys.isGrounded){
            phys.zvel=jumpvel.z;
        }else{

            phys.xvel=jumpvel.x *inputX;
            phys.yvel=jumpvel.y * inputY;
            phys.zvel=jumpvel.z;
        }
   

    }
}

//tells animator about inputx,inputy
public void handleMovement(){
            if(isRolling){return;}

            if(_currentInputState.inputVector.x>0){
                _spriteRenderer.flipX=false;
            }else if(_currentInputState.inputVector.x<0){
                _spriteRenderer.flipX=true;
            }else{
                //
            }
      /*      inputX = _currentInputState.inputVector.x;
            if(inputX > 0){
                inputX=1;
                _spriteRenderer.flipX=false;
            }else if(inputX<0){
                inputX=-1;
                _spriteRenderer.flipX=true;
            }else{
                //_spriteRenderer.flipX=true;
            }

            inputY=_currentInputState.inputVector.y;
            if(inputY>0){
                inputY=1;
            }else if(inputY<0){
                inputY=-1;
            }else{
                inputY=0;
            }
            */
            anim.SetFloat("inputX",Mathf.Round(_currentInputState.inputVector.x));
            anim.SetFloat("inputY",Mathf.Round(_currentInputState.inputVector.y));
         

}

public void handleDashing(){
    if(_currentInputState.dashPress){
            //dashPress=false;
            if(_hitBoxScript.applyingHitstop){
                   if(rms.applyingAttackRootMotion){
                        rms.test();
                        rms.applyingAttackRootMotion=false;
                        isAttacking=false;
                    }
                dashBuffer=true;
                directionBuffer=Vector2.right*Input.GetAxisRaw("Horizontal")+ Vector2.up*Input.GetAxisRaw("Vertical");
               
            }else{
                //rsm.DecreaseStamina(0.2f);
                anim.SetTrigger("dash");
                if(isAttacking){
                    if(rms.applyingAttackRootMotion){
                    rms.test();
                    rms.applyingAttackRootMotion=false;
                    isAttacking=false;
                    }
                }
                
                if(!isDashing){
                    
                   
                    Vector2 directionToDash=Vector2.right*_currentInputState.inputVector.x+ Vector2.up*_currentInputState.inputVector.y;
                    if(directionToDash!=Vector2.zero){
                          DashCoroutine=Dash(directionToDash);
                    
                    StartCoroutine(DashCoroutine);
                    }
                  
                }else{
                  
                    StopCoroutine(DashCoroutine);
                    Vector2 directionToDash=Vector2.right*_currentInputState.inputVector.x+ Vector2.up*_currentInputState.inputVector.y;
                     if(directionToDash!=Vector2.zero){

                            DashCoroutine=Dash(directionToDash);
                            StartCoroutine(DashCoroutine);
                     }
                   
                }

            }

           
        
       }
}

public IEnumerator Dash(Vector2 directionToDash){
    //play dash sfx
    audioSource.PlayOneShot(dashSFX);

    //decrease stamina
    if(rms!=null){
            rsm.DecreaseStamina(0.2f);
    }
       

    float timer=0f;
       

    //  if(directionToDash==Vector2.zero){yield break;}
    //tell animator to play dash animation
    anim.SetTrigger("dash");
    
    Vector2 startPos=(Vector2)transform.position; 
    directionToDash=directionToDash.normalized;

    //calculate end position of dash
    int mask=LayerMask.GetMask("Wall");
    RaycastHit2D hit= Physics2D.Raycast(transform.position, directionToDash,dashDistance,mask);
    Vector2 destination=Vector2.zero;
    if(hit){
        destination = (Vector2)hit.point;
    }else{
        destination=(Vector2)transform.position + dashDistance*directionToDash;
    }
    //Vector2 destination = (Vector2)hit.point;//(Vector2)transform.position + dashDistance*directionToDash;
    
       
    isDashing=true;
        
    
    //the actual dashing movement
    while((Vector2)transform.position!=destination){

            Vector2 temp=Vector2.Lerp(startPos,destination,timer/dashTime);
            //rb.MovePosition((Vector3)temp);
            transform.position=(Vector3)temp;
            timer+=Time.deltaTime;
            yield return null;
    }
       
        isDashing=false;
        canMove=true;
        canAttack=true;
       

    

}

void faceLeft(){
    transform.localScale=new Vector2(-1,1);
}

void faceRight(){
    transform.localScale= new Vector2(1,1);
}

void handleRoll(){
    if(_currentInputState.rollPress){
        anim.SetTrigger("roll");
        isRolling=true;
    }

    
}

void Update(){
        //gets jumppress,hookpress,etc
        _currentInputState = _inputManager.GetInputState();
      handleSprintHold();
      
        SetVelocity();


        anim.SetFloat("zvel",phys.zvel);
        /*
        if(Input.GetKeyDown(KeyCode.Space)){
            jumpPress=true;
        }
        if(Input.GetKeyDown(KeyCode.L)){
            dashPress=true;
        }

        if(Input.GetKeyDown(KeyCode.I)){
            hookPress=true;
        }
        */
        

        /*
       if(_movementState==null){
        _movementState = PlayerState._idleMovementState;
       }
       if(_movementState!=null){
         _movementState.
         handleInput(
            this,
            _inputManager.GetInputState());
        _movementState.update(this);
       }
       */
       

      
      //set inputx,inputy. Note: setvelocity is called in physics manager
      handleMovement();

       //dashing funcitonality
      handleDashing();



      handleRoll();

        //update groundedstate
        phys.UpdateGrounded();

        if(phys.isGrounded){
            anim.SetBool("grounded",true);
        }else{
            anim.SetBool("grounded",false);
        }
        handleJumping();

        handleHook();

        //consume dash
       if(dashBuffer && !_hitBoxScript.applyingHitstop){
        
           dashBuffer=false;
          
           Vector2 directionToDash=Vector2.right*Input.GetAxisRaw("Horizontal")+ Vector2.up*Input.GetAxisRaw("Vertical");
           if(directionBuffer!=Vector2.zero){

                DashCoroutine=Dash(directionBuffer);
        
                StartCoroutine(DashCoroutine); 
            }
       
        
            directionBuffer=Vector2.zero;
       }


        if(_currentInputState.inputVector!=Vector2.zero && !isDashing && !isAttacking){
             anim.SetBool("running",true);   
        }else{
             anim.SetBool("running",false);
        }

        //set last direction
        if(!isDashing && canMove){
             if(_currentInputState.inputVector.x<0){
               
              lastDirection=Direction.left;
    
                }else if(_currentInputState.inputVector.x>0){
                    
                    lastDirection=Direction.right;
                
                }else if(_currentInputState.inputVector.y>0){

                    lastDirection=Direction.up;

                }else if(_currentInputState.inputVector.y<0){

                    lastDirection=Direction.down;

                }  
        }
     

        //tell animator which direction we're facing
        
        if(canMove && !isDashing && !isAttacking && !isRolling){
                switch (lastDirection){
                    case Direction.up:
                        anim.SetFloat("direction",0);
                        break;
                    case Direction.down:
                        anim.SetFloat("direction",1);

                        break;
                    case Direction.left:
                        anim.SetFloat("direction",2);
                        break;
                    default:
                        anim.SetFloat("direction",3);
                        break;

                }
        }
        
        //handle parry
         if(_currentInputState.parryPress){
           // rb.velocity=Vector3.zero;
            anim.SetTrigger("parry");
         }


        //handle normal attack
        if(_currentInputState.attackPress){
           // rb.velocity=Vector3.zero;
            if(transform.Find("Hitbox").GetComponent<HitBoxScript>().applyingHitstop){
                attackBuffer=true;   
            }else{

                 if(isDashing){
                        isDashing=false; 
                        StopCoroutine(DashCoroutine);
                        //rb.velocity=Vector3.zero;
                 }
                        anim.SetTrigger("attack");
                        isAttacking=true;
                        Vector3 temp=transform.Find("FrontOfPlayer/slashvfx").localScale;
                        temp.x*=-1;
                        
                        transform.Find("FrontOfPlayer/slashvfx").localScale=temp;

            }
            
            
        }
        
        //handle attack buffer
        if(attackBuffer && !_hitBoxScript.applyingHitstop){
                        attackBuffer=false;
                        anim.SetTrigger("attack");
                        isAttacking=true;
                        Vector3 temp=transform.Find("FrontOfPlayer/slashvfx").localScale;
                        temp.x*=-1;
                        
                        transform.Find("FrontOfPlayer/slashvfx").localScale=temp;
        }
        //handle charge attack
        if(Input.GetKey(KeyCode.P)){
            attackHeldDownTime+=Time.fixedUnscaledDeltaTime;
            anim.SetBool("holdingattack",true);
        }
        if(Input.GetKeyUp(KeyCode.P)){

            if(attackHeldDownTime>0.5f){
                anim.SetTrigger("charge");
                
            }
            attackHeldDownTime=0f;
            anim.SetBool("holdingattack",false);
          
        }
        anim.SetFloat("holdtime",attackHeldDownTime);


        //handle movement restriction
        if(!canMove){
            playerSpeed=0f;
        }else if(isAttacking){
            playerSpeed=0f;//staticPlayerSpeed/4f;
        }else{
            playerSpeed=staticPlayerSpeed;
        }



        UpdateDeltaPos();
}//end of update


//called in physicsmanager
public void SetVelocity(){
    if(!isDashing && !isAttacking && !parrying && !rms.applyingAttackRootMotion){
            phys.xvel=playerSpeed*_currentInputState.inputVector.x;
            phys.yvel=playerSpeed*_currentInputState.inputVector.y;
             
    }
}

#region utility
public void DisableMovement(){
        canMove=false;
}

public void EnableMovement(){
            canMove=true;
}

public Vector2 getDirection(Direction d){
        switch (lastDirection){
            case Direction.up:
                return Vector2.up;
                break;
            case Direction.down:
                return Vector2.down;
                break;
            case Direction.left:
                return Vector2.left;
                break;
            default:
                return Vector2.right;
                break;
        }
}

public Vector2 GetFrontOfPlayer(){
        return (Vector2)transform.position + getDirection(lastDirection);
}

public Vector2 GetForwardOfPlayer(){
    return getDirection(lastDirection);
}

public Quaternion GetRotationOfPlayer(){
        
        float angle;
        switch (lastDirection){
        case Direction.up:
            angle=0f;
            break;
        case Direction.down:
            angle = 180f;
            break;
        case Direction.left:
            angle=90f;
            break;
        default:
            angle=-90f;
            break;
        }
        return Quaternion.Euler(0f, 0f, angle);
}

void OnCollisionStay2D(Collision2D col){
     //   print("collision");
       /// rb.velocity=Vector2.zero;
    }
}

#endregion