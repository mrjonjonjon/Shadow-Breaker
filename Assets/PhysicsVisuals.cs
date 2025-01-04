using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
//controls the visuals
//NOTE: ALL SPRITERENDERERS MUST BE SLICED, NOT TILED OR SIMPLE
public class PhysicsVisuals : MonoBehaviour
{                 public SpriteRenderer _spriteRenderer;

                  public SpriteRenderer _srTop,_srBottom;
                  public List<GameObject> allShadows=new List<GameObject>();
                  public GameObject shadowPrefab;
                  public SpriteRenderer spriteMask;
                  public GameObject selfShadow;
                  public bool drawShadows;
                  public float shadowFactor;
                  public Physics _physics;
                  public static float thickness=0.3f;
                  public static Color gizmoColor=Color.yellow;
                //  public AnimationCurve brightnessCurve;




    private void OnDrawGizmos()
    {
        BoxCollider2D _box = (BoxCollider2D) GetComponent<Physics>()._collider2D;

        Vector2 boxPosition = _box.transform.position + (Vector3)_box.offset;
        Vector2 boxSize = _box.size;

        // Draw the main box
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(boxPosition, boxSize);

        // Draw offset boxes for "thickness"
        for (float i = 0; i < thickness; i += 0.05f)
        {
            Gizmos.DrawWireCube(boxPosition, boxSize + new Vector2(i, i));
            Gizmos.DrawWireCube(boxPosition, boxSize - new Vector2(i, i));
        }
    }


//lateupdate because we want this to run after animator has updated. can force an update manually later
void LateUpdate(){
                        
                        float width=GetComponent<Physics>().width;
                        float height=GetComponent<Physics>().height;
                        float depth=GetComponent<Physics>().depth;
                        float zpos=GetComponent<Physics>().zpos;
                        SortingGroup sg=GetComponent<Physics>().sg;
                        Physics.EntityType entityType=GetComponent<Physics>().entityType;
                        //SpriteRenderer _srBottom=GetComponent<Physics>()._srBottom;
                       //SpriteRenderer _srTop=GetComponent<Physics>()._srTop;
                        
                        Collider2D _collider2D=GetComponent<Physics>()._collider2D;
                       // s.pivot.y/s.pixelsPerUnit
                        if(entityType==Physics.EntityType.Player &&transform.Find("collider/shadow")!=null){
                                  if(_physics.entitiesDirectlyBelowWithSpace.Count>0){
                                        transform.Find("collider/shadow").localPosition = new Vector3(0,
                                        _physics.entitiesDirectlyBelowWithSpace[0].GetComponent<Physics>().zpos+
                                        _physics.entitiesDirectlyBelowWithSpace[0].GetComponent<Physics>().height

                                        
                                        ,-(0.2f+_physics.entitiesDirectlyBelowWithSpace[0].GetComponent<Physics>().zpos+
                                        _physics.entitiesDirectlyBelowWithSpace[0].GetComponent<Physics>().height));

                                  }else{
                                         transform.Find("collider/shadow").localPosition = new Vector3(0,0,0);
                                  }
                            
                                 // Sprite s =transform.Find("sprite/bottom").GetComponent<SpriteRenderer>().sprite;
                                //  float distance = height*0.3141892f;
                                //  transform.Find("sprite").localPosition= (Vector3.up-Vector3.forward) * zpos
                                //  -distance*Vector3.forward;
                                   
                        }
                        if(entityType==Physics.EntityType.Box){
                          //print(gameObject.name + zpos);
                                //set collider width and depth sizes
                                ((BoxCollider2D)_collider2D).size= new Vector2(width,depth);
                                
                                //set top,bottom sprite sizes
                                _srBottom.size=new Vector2(width,height); 
                                _srTop.size=new Vector2(width,depth);

                                //fix collider2d pos to pivot
                                _collider2D.transform.localPosition=Vector3.zero;

                                //offset sprite bottom and sprite top to match up correctly
                                //0.3141892f is the y pivot point of the srbottom sprite

                                //transform.Find("sprite").transform.position = new Vector3(0,0,-zpos);
                                _srBottom.transform.position = new Vector3(_collider2D.transform.position.x,
                                                                           _collider2D.transform.position.y + height/2 - depth/2+zpos,
                                                                           -zpos);
                             // _srBottom.sharedMaterial.SetFloat("_Height", height);
                                _srTop.transform.position=new Vector3(_srBottom.transform.position.x,
                                                                      _srBottom.bounds.max.y+depth/2,
                                                                      _srBottom.transform.position.z-height);
                                  //zpos based brightness
//                                  AnimationCurve brightnessCurve = SerializedSingletons.instance.brightnessCurve;
 //                                float brightness = brightnessCurve.Evaluate(zpos+height);

                                  // Set the brightness (you can clamp the values to ensure they're between 0 and 1)
                                  Color color = _srTop.color;
                                  

//                                  _srTop.color = new Color(brightness,brightness,brightness);



               
                  
                        

                      }else{
                        //dworldspace distance from bottom of sprite to pivot
                          float distance = (_spriteRenderer.sprite.pivot.y/_spriteRenderer.sprite.pixelsPerUnit);

                          if(transform.Find("sprite")!=null){
                                 transform.Find("sprite").localPosition= (Vector3.up-Vector3.forward) * zpos 
                                 -Vector3.forward*(distance)*transform.Find("sprite").localScale.x;//new Vector3(transform.Find("sprite").localPosition.x,  zpos/transform.localScale.y,0);
                          }
                      }
                }
}
