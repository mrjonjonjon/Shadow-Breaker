using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spritepivot : MonoBehaviour
{
    public Sprite s;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(s.bounds.size.y*s.pixelsPerUnit);//bounds.size is in world space
        print(s.pivot);//in texture space
        print(s.pivot.y/s.pixelsPerUnit);
    }
}
