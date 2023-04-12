using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
//controls the visuals
public class PhysicsVisuals : MonoBehaviour
{                 public SpriteRenderer _spriteRenderer;
                  public List<GameObject> allShadows=new List<GameObject>();
                  public GameObject shadowPrefab;
                  public SpriteRenderer spriteMask;
                  public GameObject selfShadow;
                  public bool drawShadows;
                  public float shadowFactor;


//lateupdate because we want this to run after animator has updated
void LateUpdate(){
                        
                        float width=GetComponent<Physics>().width;
                        float height=GetComponent<Physics>().height;
                        float depth=GetComponent<Physics>().depth;
                        float zpos=GetComponent<Physics>().zpos;
                        SortingGroup sg=GetComponent<Physics>().sg;
                        Physics.EntityType entityType=GetComponent<Physics>().entityType;
                        SpriteRenderer _srBottom=GetComponent<Physics>()._srBottom;
                        SpriteRenderer _srTop=GetComponent<Physics>()._srTop;
                        Collider2D _collider2D=GetComponent<Physics>()._collider2D;
                       // s.pivot.y/s.pixelsPerUnit
                        float distance = (_spriteRenderer.sprite.pivot.y/_spriteRenderer.sprite.pixelsPerUnit);

                          if(transform.Find("sprite")!=null){
                                 transform.Find("sprite").localPosition= (Vector3.up-Vector3.forward) * zpos 
                                 -Vector3.forward*(distance)*transform.Find("sprite").localScale.x;//new Vector3(transform.Find("sprite").localPosition.x,  zpos/transform.localScale.y,0);
                          }

                        if(entityType==Physics.EntityType.Box){
                  
                                ((BoxCollider2D)_collider2D).size= new Vector2(width,depth);

                                _srBottom.size=new Vector2(width,height); 
                                _srTop.size=new Vector2(width,depth);
                                _collider2D.transform.localPosition=Vector3.up*depth/2f;
                                _srBottom.transform.localPosition = Vector3.zero;
                                _srTop.transform.position=new Vector3(_srBottom.transform.position.x,_srBottom.bounds.max.y,_srBottom.transform.position.z-height);

            
                  

                      }
                }
}
