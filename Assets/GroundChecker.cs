using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!grounded){transform.parent.GetComponent<MovementController>().enabled=false;}
    }

    void OnTriggerEnter2D(Collider2D col){
if(col.CompareTag("2dground")){grounded=true;}
    }

     void OnTriggerExit2D(Collider2D col){
if(col.CompareTag("2dground")){grounded=false;}
    }
}
