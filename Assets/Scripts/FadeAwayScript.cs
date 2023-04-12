using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAwayScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool done=false;
    public enum FadeType{rigidbody,time};
    public  FadeType ft=FadeType.rigidbody;
    void Start()
    {
      if(ft==FadeType.time){
                      StartCoroutine(fadeOut());

      }
    }
IEnumerator fadeOut(){
    
    float timer=0f;
    if(GetComponent<MeshRenderer>()!=null){

        float curalpha=  GetComponent<MeshRenderer>().material.GetFloat("_Alpha");
    while(curalpha>0f){
        
        GetComponent<MeshRenderer>().material.SetFloat("_Alpha",curalpha);
        curalpha-=Time.fixedDeltaTime;
        yield return new WaitForFixedUpdate();
    }
    }else{

        float curalpha=  GetComponent<SpriteRenderer>().color.a;//.GetFloat("_Alpha");
    while(curalpha>0f){
        float r=GetComponent<SpriteRenderer>().color.r;
        float g=GetComponent<SpriteRenderer>().color.g;
        float b=GetComponent<SpriteRenderer>().color.b;
        float a=GetComponent<SpriteRenderer>().color.a;
        GetComponent<SpriteRenderer>().color = new Color(r,g,b,curalpha);//.a=curalpha;//.SetFloat("_Alpha",curalpha);
        curalpha-=Time.fixedDeltaTime;
        yield return new WaitForFixedUpdate();
    }

    }
   
    Destroy(gameObject,0.5f);
}
    // Update is called once per frame
    void Update()
    {
        if(done)return;
        if(ft==FadeType.rigidbody){
            if(GetComponent<Rigidbody2D>().velocity.magnitude<0.1f){
            StartCoroutine(fadeOut());
            done=true;
        }
        }
        
    }
}
