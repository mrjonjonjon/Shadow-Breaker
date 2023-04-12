using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ParallaxScript : MonoBehaviour
{
    //atach to parent. adjust child as necessary
    //-1 is foreground, 1 background
    public float parallaxFactor=-1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position =parallaxFactor * new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,transform.position.z);

    }
}
