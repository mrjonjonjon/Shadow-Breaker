using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    public Vector3 startPos=Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        if(MovementController.instance!=null){
            MovementController.instance.transform.position=startPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
