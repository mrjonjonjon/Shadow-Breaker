using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{

    SpriteRenderer r;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
     r=GetComponent<SpriteRenderer>();
     anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(anim.GetCurrentAnimatorStateInfo(0).IsName()){
        //    r.material.SetColor(,Color.Black);
        //}
    }
}
