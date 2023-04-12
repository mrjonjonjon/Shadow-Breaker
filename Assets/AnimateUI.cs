using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//[ExecuteInEditMode]
public class AnimateUI : MonoBehaviour
{
    Material clone;

    [ColorUsage(true, true)]

    public Color c;
  
    Color sc;
    public float alph;

    void Start()
    {
        
        clone = new Material(GetComponent<Image>().material);
        GetComponent<Image>().material=clone;
        GetComponent<Animator>().enabled=false;
        GetComponent<Animator>().enabled=true;

    }

    // Update is called once per frame
    void Update()
    {
     //   GetComponent<Image>().material.SetColor("_Color",c);
     sc.r=c.r*alph;
     sc.g=c.g*alph;

     sc.b=c.b*alph;
    // c.r=staticColor.r*alph;

           clone.SetColor("_Color",sc);

    }
}
