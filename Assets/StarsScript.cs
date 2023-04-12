using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool onleftstairs=false;//left to ritght
    public bool onrightstairs=false;

    bool done=false;

    public int currentFloor=1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(!onleftstairs && !onrightstairs){
                   // transform.position =new Vector3(transform.position.x,transform.position.y,0f);

            return;}
       // transform.position =new Vector3(transform.position.x,transform.position.y,0.2f);

        if(onleftstairs){
            done=true;
            if(Input.GetAxisRaw("Horizontal")>0){
                  
                   GetComponent<Rigidbody2D>().velocity +=3*Vector2.up;
                   
            }else if(Input.GetAxisRaw("Horizontal")<0){
                                   GetComponent<Rigidbody2D>().velocity-=3*Vector2.up;


            }
        }else if(onrightstairs){

                 if(Input.GetAxisRaw("Horizontal")>0){
                  
                   GetComponent<Rigidbody2D>().velocity -=3*Vector2.up;
                   
            }else if(Input.GetAxisRaw("Horizontal")<0){
                                   GetComponent<Rigidbody2D>().velocity+=3*Vector2.up;


            }
        }

        
       
    }

    void OnTriggerEnter2D(Collider2D col){
         if(col.CompareTag("rightstairs")){
            onleftstairs=true;
         }else if(col.CompareTag("leftstairs")){
            onrightstairs=true;
         }
    }

      void OnTriggerExit2D(Collider2D col){
         if(col.CompareTag("rightstairs")){
            onleftstairs=false;
            done=false;
         }else if(col.CompareTag("leftstairs")){
            onrightstairs=false;
            done=false;
         }
    }
}
