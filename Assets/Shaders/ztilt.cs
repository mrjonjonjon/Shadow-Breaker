using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[ExecuteInEditMode]
public class ztilt : MonoBehaviour
{
    void Start(){

        // transform.position +=Vector3.forward* (GetComponent<Tilemap>().cellBounds.min.y -transform.position.y );
    }
    // Start is called before the first frame update
    void Update()
    {//consider using tilemap bounds
        
         transform.rotation = Quaternion.Euler(-45, 0, 0);
         transform.localScale= new Vector3(1,Mathf.Sqrt(2),1);
        
    }

    
}
