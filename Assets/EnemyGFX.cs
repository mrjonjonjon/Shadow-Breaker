using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyGFX : MonoBehaviour
{

    public AIPath aipath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aipath.desiredVelocity.x>=0.01f){
            transform.localScale=new Vector3(1f,1f,1f);
        }else if(aipath.desiredVelocity.x<-0.01f){
                        transform.localScale=new Vector3(-1f,1f,1f);

        }
    }
}
