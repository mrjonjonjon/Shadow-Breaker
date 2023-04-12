using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class PuddleReflectionScript : MonoBehaviour
{
    public SpriteRenderer _playerSprite;
    public Physics _playerPhysics;



    void Update()
    {
      
    }

    void LateUpdate(){  GetComponent<SpriteRenderer>().sprite=_playerSprite.sprite;
        GetComponent<SpriteRenderer>().flipX=_playerSprite.flipX;
        transform.localPosition= new Vector3(0f,-_playerPhysics.zpos,0);
        transform.localPosition-=Vector3.up*(
                        2*_playerSprite.sprite.pivot.y/_playerSprite.sprite.pixelsPerUnit
                        );
    }
}
