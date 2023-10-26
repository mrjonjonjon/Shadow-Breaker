using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{

    public float width;
    public float depth;
    public int direction;//0,1,2,3 for left,up,right,down
    public float z_bot;
    public float z_top;
    public float interpolation_factor;
    public ContactFilter2D _contactFilter;

    public Collider2D _collider2D;

    void Update()
    {



            List<RaycastHit2D> results=new List<RaycastHit2D>();
           
            int num_completed_iters=0;
            int num_entites_above_or_below=Physics2D.BoxCast(_collider2D.gameObject.transform.position,_collider2D.bounds.size, 0f, Vector2.zero, _contactFilter,results,Mathf.Infinity); 

            GameObject go=null;
            float mymax=-Mathf.Infinity;

            GameObject belowWithSpace=null;
            float highestBelow=-100000f;
            float smallestYBelow=10000000f;
                if(gameObject.name=="Player"){}

            foreach(RaycastHit2D hit in results){
                        
                        Collider2D other_collider=hit.collider;     

                        if(other_collider.gameObject.tag!="PhysicsEntity")continue;
                        if(GameObject.ReferenceEquals( gameObject, other_collider.transform.parent.gameObject))continue;

                #region get properties of other object
                    Physics other_physics = other_collider.transform.parent.GetComponent<Physics>();
	                float z =other_collider.transform.parent.GetComponent<Physics>().zpos;
	                float zv=other_collider.transform.parent.GetComponent<Physics>().zvel;
	                float w=other_collider.transform.parent.GetComponent<Physics>().width;
	                float xv=other_collider.transform.parent.GetComponent<Physics>().xvel;
	                float h=other_collider.transform.parent.GetComponent<Physics>().height;
	                float d=other_collider.transform.parent.GetComponent<Physics>().depth;
	                float yv=other_collider.transform.parent.GetComponent<Physics>().yvel;
	                float zf=other_collider.transform.parent.GetComponent<Physics>().zfloor;
	                float m=other_collider.transform.parent.GetComponent<Physics>().mass;
	                float r=other_collider.transform.parent.GetComponent<Physics>().restitution;
                #endregion


                float left_xpos = _collider2D.bounds.min.x;
                float right_xpos = _collider2D.bounds.max.x;
                interpolation_factor = (other_collider.transform.position.x-left_xpos)/(right_xpos-left_xpos);
                float target_z_floor = Mathf.Lerp(z_bot,z_top,interpolation_factor);
                other_physics.zfloor=Mathf.Max(zf,target_z_floor);





                    
                 
            }

    }
}
