using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCloneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelfDestruct(float time){
        Destroy(gameObject,time);
    }
}
