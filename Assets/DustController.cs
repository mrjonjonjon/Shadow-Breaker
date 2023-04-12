using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustController : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem ps;
    MovementController.Direction prev,cur;
    Vector2 prevSpeed,curSpeed;
    void Start()
    {
        ps=transform.GetComponent<ParticleSystem>();
        prev=transform.parent.GetComponent<MovementController>().lastDirection;
        cur=prev;

        prevSpeed=Vector2.zero;
        curSpeed=Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        prevSpeed=curSpeed;
        curSpeed=transform.parent.GetComponent<Rigidbody2D>().velocity;
        prev=cur;
        cur=transform.parent.GetComponent<MovementController>().lastDirection;
        //changed direction
        if(prev!=cur){
            CreateDust();
        }

        if(prevSpeed.magnitude==0 && curSpeed.magnitude>0){
            CreateDust();
        }
       
    }

    public void CreateDust(){
        ps.Play();
    }


}
