using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class zasy : MonoBehaviour
{
    public float factor=20f;
    public float offset=0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(transform.position.x,transform.position.y,offset + (transform.position.y/factor));
       // transform.parent.Find("sprite").transform.localPosition=new Vector3( transform.parent.Find("sprite").transform.localPosition.x,-transform.parent.position.z, transform.parent.Find("sprite").transform.localPosition.z);
    }
}
