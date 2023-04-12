using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PetAI : MonoBehaviour
{
    Animator anim;
    AIPath aipath;
    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();
        aipath=GetComponent<AIPath>();
    }

    float fun(float x){
        if(x<-0.01f){
            return -1;
        }else if(x>0.01f){
            return 1;
        }else{
            return 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xvel= aipath.velocity.x;
        float yvel=aipath.velocity.y;
        
     // float angle = Mathf.Atan2(new Vector2(xvel,yvel));
        
        anim.SetFloat("horizontal",xvel);
        anim.SetFloat("vertical",yvel);
    }
}
