using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ParallaxScript : MonoBehaviour
{
    //atach to parent. adjust child as necessary
    //-1 is foreground, 1 background
    [Range(-1f, 1f)]
    public float parallaxFactor=-1f;


    void Update()
    {
        transform.position = new Vector3(parallaxFactor *Camera.main.transform.position.x,
                                        parallaxFactor *Camera.main.transform.position.y,
                                        transform.position.z);

    }
}
