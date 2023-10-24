using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MovementCloneScript : MonoBehaviour
{
    public int frameDelay=4;
    Rigidbody2D rb;
    MovementController mc;
    SpriteRenderer _cloneSpriteRenderer;
    SpriteRenderer _playerSpriteRenderer;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        mc=MovementController.instance;
        StartCoroutine(InitiateCoroutines());
        MaterialPropertyBlock propBlock = new MaterialPropertyBlock();

        transform.Find("sprite").GetComponent<SpriteRenderer>().GetPropertyBlock(propBlock);

        propBlock.SetColor("_Color", Color.blue);

       // transform.Find("sprite").GetComponent<SpriteRenderer>().SetPropertyBlock(propBlock);



    }

IEnumerator InitiateCoroutines(){
   for(int i=0;i<frameDelay;i++){
        StartCoroutine(CopyMovement());
        yield return null;
    }
    
   
   
}
    IEnumerator CopyMovement(){


        while(true){
            //Vector3 dp=mc.transform.GetComponent<Physics>().deltaPos;
            Vector3 dp = mc.deltaPos;
            Sprite s=mc.transform.Find("sprite").GetComponent<SpriteRenderer>().sprite;
            bool f = mc.transform.Find("sprite").GetComponent<SpriteRenderer>().flipX;
            //bool hitboxState=mc.transform.Find("Hitbox").GetComponent<BoxCollider2D>().enabled;
            //Vector2 offset=mc.transform.Find("Hitbox").GetComponent<BoxCollider2D>().offset;
                int initialNumUpdates = PhysicsManager.instance.totalPhysicsUpdates;
            for(int i=0;i<frameDelay;i++){
                yield return null;
            }
           //yield return new  WaitUntil(() => PhysicsManager.instance.totalPhysicsUpdates >= frameDelay+initialNumUpdates);
           // transform.Find("Hitbox").GetComponent<BoxCollider2D>().enabled=hitboxState;
            //transform.Find("Hitbox").GetComponent<BoxCollider2D>().offset=offset;
           // yield return new WaitForFixedUpdate();
            //transform.Find("sprite").position +=dp;
            transform.GetComponent<Physics>().position+=dp;
            //transform.position +=dp;
            transform.Find("sprite").GetComponent<SpriteRenderer>().sprite=s;
            transform.Find("sprite").GetComponent<SpriteRenderer>().flipX=f;


        }
    }
}
