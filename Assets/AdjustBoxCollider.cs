using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class AdjustBoxCollider : MonoBehaviour
{
    public Vector2 newSize= Vector2.zero;
    // Start is called before the first frame update
    void Awake()
    {
        newSize.x=GetComponent<SpriteRenderer>().size.x-2f;
        newSize.y=4;
    }

    // Update is called once per frame
    void Update()
    {
       // print(GetComponent<SpriteRenderer>().size);
       GetComponent<BoxCollider2D>().size=newSize;
    }

    
}
