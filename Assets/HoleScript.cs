using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{

    public Vector3 respawnPos;
    public float respawnZpos;
    float zpos;
    float zfloor;
    public bool falling=false;
    public bool inContact=false;
    // Start is called before the first frame update
    void Start()
    {
        respawnPos=transform.position;
    }
void OnTriggerEnter2D(Collider2D col){
    if(col.CompareTag("hole")){
        inContact=true;
    }
}

void OnTriggerExit2D(Collider2D col){
    if(col.CompareTag("hole")){
        inContact=false;
    }
}

public void CheckFall(){
 if(inContact && GetComponent<Physics>().zpos<=0){
    
            GetComponent<Physics>().zfloor=-10f;
            falling=true;
        }else if(GetComponent<Physics>().zpos>=0){
            GetComponent<Physics>().zfloor=0f;
            falling=false;
        }


        //set respawnpoint
     if(GetComponent<Physics>().zfloor>=0 && GetComponent<Physics>().zpos==zfloor){
            respawnPos=transform.position;
           
           //set position to respawnpoint if zpos is low enough
        }else if(GetComponent<Physics>().zpos<=-10f){
            transform.position=respawnPos;
             GetComponent<Physics>().zpos=5f;
             GetComponent<Physics>().xvel=0f;
            GetComponent<Physics>().yvel=0f;


        }
}
    // Update is called once per frame
    void Update()
    {
      // CheckFall();
    }

    void OnTriggerStay2D(Collider2D col){
       
    }
}
