using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;


public class Destructible : MonoBehaviour
{
    //public GameObject objectToSlice;
     Vector3 startPos;


   //public GameObject[] Slice(Vector3 planeWorldPosition, Vector3 planeWorldDirection) {
	//return objectToSlice.SliceInstantiate(planeWorldPosition, planeWorldDirection);
//}

    // Start is called before the first frame update
    void Start()
    {
       // startPos=objectToSlice.transform.position;
       //Cut(gameObject);
    }

    

        public void Cut(){
                GameObject objectToSlice=gameObject;
                startPos=objectToSlice.transform.position;
                Vector2 randomVector = Random.insideUnitCircle;//.Normalize();
             
                GameObject[] pieces = objectToSlice.SliceInstantiate(objectToSlice.transform.position,randomVector);


                int count=0;
                foreach(GameObject go in pieces){
                    
                /*
                if(count==0){
                    go.transform.position=objectToSlice.transform.position;
                    go.transform.rotation=objectToSlice.transform.rotation;
                    count++;
                    SetSortingLayer ssl2= go.AddComponent<SetSortingLayer>();
                    ssl2.MySortingLayer="Player";
                    continue;

                }
                */
                Rigidbody2D rb= go.AddComponent<Rigidbody2D>() as Rigidbody2D;
                go.transform.position=startPos;
                go.AddComponent<FadeAwayScript>();
                SetSortingLayer ssl= go.AddComponent<SetSortingLayer>();
                ssl.MySortingLayer=GetComponent<SetSortingLayer>().MySortingLayer;
                ssl.color=GetComponent<SetSortingLayer>().color;
               
                rb.mass=1f;
                rb.gravityScale=0f;
                rb.AddForce(Random.insideUnitCircle.normalized*5f,ForceMode2D.Impulse);
                rb.drag=2f;
                rb.isKinematic=false;
                count++;
                
            }
            Destroy(objectToSlice);
        }
    

}
