using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRenderQueue : MonoBehaviour
{

    public int renderQueuePos;
    // Start is called before the first frame update
    void Start()
    {
        
        for(int i=0;i<GetComponent<Renderer>().materials.Length;i++){
           Material t= GetComponent<Renderer>().materials[i];
           t.renderQueue=renderQueuePos;
           GetComponent<Renderer>().materials[i]=t;

                       // GetComponent<SpriteRenderer>().materials[i].renderQueue=renderQueuePos;

        }
       // GetComponent<SpriteRenderer>().sharedMaterial.renderQueue=renderQueuePos;
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
