using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public enum StairType{LeftToRight, RightToLeft, DownToUp,UpToDown};
    public StairType stairType;
    public GameObject lowFloor;
    public GameObject highFloor;
    bool canEnter=true;
    bool canExit=false;
    bool done=false;
    bool inContact=false;
        public float distanceBetweenFloors=25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   
 void OnTriggerExit2D(Collider2D col){
   
    if(!col.CompareTag("Player")){return;}
    inContact=false;
    done=false;
   


 }

 void OnTriggerEnter2D(Collider2D col){
     if(!col.CompareTag("Player")){return;}
     inContact=true;
 }
    void Update(){
    
        if(!inContact){return;}
        if(done){return;}
        if(Input.GetAxisRaw("Horizontal")==0){return;}
       

            if(stairType==StairType.RightToLeft){//going down

                    if(Input.GetAxisRaw("Horizontal")>0){//holding right
                                    //go down
                                    //col.transform.position += -1*Input.GetAxisRaw("Horizontal")* Vector3.forward*100;
                                   // highFloor.SetActive(false);
                                   // lowFloor.SetActive(true);
                                    print("going down");
                                  
                                    MovementController.instance.transform.GetComponent<StarsScript>().currentFloor--;
                                    MovementController.instance.transform.GetComponent<SpriteRenderer>().sortingLayerName="Floor"+ MovementController.instance.transform.GetComponent<StarsScript>().currentFloor;
                                                                        MovementController.instance.transform.position+=Vector3.forward*distanceBetweenFloors;

                     }else if(Input.GetAxisRaw("Horizontal")<0){//holding left

                              
                                    //col.transform.position += -1*Input.GetAxisRaw("Horizontal")* Vector3.forward*100;

                                    //highFloor.SetActive(true);
                                    //lowFloor.SetActive(false);
                                    print("going up");
                                    
                                   
                                     MovementController.instance.transform.GetComponent<StarsScript>().currentFloor++;
                                    MovementController.instance.transform.GetComponent<SpriteRenderer>().sortingLayerName="Floor"+ MovementController.instance.transform.GetComponent<StarsScript>().currentFloor;
                                     MovementController.instance.transform.position-=Vector3.forward*distanceBetweenFloors;


                    }
            }else if(stairType==StairType.LeftToRight){


            }
           
          
        done=true;
    }
}
