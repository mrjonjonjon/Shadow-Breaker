using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PuddleReflectionScript : MonoBehaviour
{
    public SpriteRenderer _playerSprite;
    public Physics _playerPhysics;
    private bool _reflectionVisible;

    public void Start(){
        _reflectionVisible=true;
    }
    public void DeActivate(){
        _reflectionVisible=false;
    }

    public void Activate(){
        _reflectionVisible=true;
    }
    void Update()
    {
      
    }

    void LateUpdate(){  
        if(!_reflectionVisible){GetComponent<SpriteRenderer>().enabled=false; return;} 
        
        
                float height =  _playerPhysics.height;
                float width = _playerPhysics.width;
                float zpos = _playerPhysics.zpos;
                float zfloor = _playerPhysics.zfloor;
        if(_playerPhysics.entityType==Physics.EntityType.Box){
               
                BoxCollider2D  col =  transform.parent.Find("collider").GetComponent<BoxCollider2D>();
                
            GetComponent<SpriteRenderer>().sprite = _playerSprite.sprite;
            GetComponent<SpriteRenderer>().size = new Vector2(width,height);
             transform.position = new Vector3(_playerSprite.transform.position.x,
                                              col.bounds.min.y -height*(0.3141892f)+zfloor-(zpos-zfloor),
                                              col.bounds.min.z-0.01f);
        }else{

            GetComponent<SpriteRenderer>().sprite=_playerSprite.sprite;
            GetComponent<SpriteRenderer>().flipX=_playerSprite.flipX;
            transform.localPosition= new Vector3(0f,zfloor-(zpos-zfloor),-zfloor-0.01f);
            transform.localPosition-=Vector3.up*(
                            2*_playerSprite.sprite.pivot.y/_playerSprite.sprite.pixelsPerUnit
                            );
        }
       
    }
}
