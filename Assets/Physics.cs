using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

//[ExecuteInEditMode]
//NOTE: 
//transform.position = scene position (ie x,y,-z)
//(transform.position.x,transform.position.y,0) = "shadow position" (ie x,y,0)
//do not use (x,y+z,0) here. that is where the sprite child is displayed

public class Physics : MonoBehaviour
{
   #region declarations

        
        public ContactFilter2D _contactFilter;
        public bool lockGravityScale=false;

        [HideInInspector]
        public bool ignorePositionUpdate=false;

        [HideInInspector]
        public float originalGravityScale;

        //controls whether solidobject or trigger
        public bool isTrigger=false;

        [HideInInspector]
	    public int entityID;

        [HideInInspector]
	    public bool positionUpdated=false;
        [HideInInspector]
	    public bool collisionsResolved=false;

        //includes the entire stack of entities above/below this
	    public List<GameObject> entitiesAbove;
	    public List<GameObject> entitiesBelow;

        //includes only the entities that touch this
	    public List<GameObject> entitiesDirectlyAbove;
        public List<GameObject> entitiesDirectlyBelow;

        //includes the entities you can draw a line between with no entities between.
        //ASSUMPTION: each object can have multiple entitiesabovewithspace, but only 1 entitybelowwith space.
        public List<GameObject> entitiesDirectlyAboveWithSpace;
        public List<GameObject> entitiesDirectlyBelowWithSpace;
	
	    public float mass=1f;
        [HideInInspector]
        public float inv_mass;
        public bool infinite_mass=false;

	    public float restitution=0f;


        [Header("Constraints")]

	    public bool fixX,fixY,fixZ;
	    public bool fixXvel,fixYvel,fixZvel;
	    
	    [Range(-1f, 1f)]
	    public float xvel,yvel,zvel;
        
	    public Vector3 lastPos;
	    public Vector3 deltaPos;
        //position/velocity in the simulated 3d space (x,y,z)

        
        //pseudo-velocity
        public Vector3 velocity{
            get{return new Vector3(xvel,yvel,zvel);}
            set {
                    xvel=value.x;
                    yvel=value.y;
                    zvel=value.z;
                }
        }
        //pseudo-position
        public Vector3 position{
            get{return new Vector3(transform.position.x,transform.position.y,zpos);}
            set{transform.position = new Vector3(value.x,value.y,0);zpos=value.z;}
        }

        public Vector2 screenPosition{
            get{return new Vector2(transform.position.x,transform.position.y+zpos);}
            
        }


	    [Range(-10f, 100f)]
	    public float zfloor;
	
	    [Range(-10f, 300f)]
	    public float zpos;
			
	    public float staticFriction=1f;
	    public float dynamicFriction=0.5f;


		[Header("Dimensions")]
	    [Range(1f, 100f)]
	    public float width,height,depth;

       // [HideInInspector]
	    public Collider2D _collider2D;
	    //for box type entities
        [HideInInspector]
	    public SpriteRenderer _srTop,_srBottom;
	
	    //for player type entities
        [HideInInspector]
	    public SpriteRenderer _sr;
        [HideInInspector]
	    public SortingGroup sg;

	    public bool isGrounded=false;
	
	    public float gravityScale=1f;

	    public enum EntityType{Player,Box,Prop};
	    public EntityType entityType;
#endregion


public void Start(){
   originalGravityScale = gravityScale;

}
//resolve collisions for entity-tile interactions
//TODO: change logic so it works for non-player entities too
public void SetZFloor(){
    if(isTrigger)return;
        //int num_completed_iters=0;
        List<RaycastHit2D> results=new List<RaycastHit2D>();
        
        float maxtilefloor=-1000000f;
        bool foundFloor=false;
        int num_collisions=0;
        Vector3 originalPos=transform.position;
        //not sure why i need 3 iterations instead of 2.
        //idea was: try positional correction on each axis. then both axes if that fails. allows for sliding along walls
        for(int num_completed_iters=0;num_completed_iters<3;num_completed_iters++){
            num_collisions=0;
            results.Clear();
            maxtilefloor=-1000000f;
            foundFloor=false;
           
                                            Physics2D.BoxCast(
                                            _collider2D.gameObject.transform.position,
                                            _collider2D.bounds.size, 
                                            0f,
                                            Vector2.zero, 
                                            _contactFilter,
                                            results,
                                            Mathf.Infinity);

                        
                                    // num_completed_iters++;
                                    Vector2 hitpoint=Vector2.zero;
                                    foreach(RaycastHit2D hit in results){

                  
                                        Collider2D col=hit.collider;
                                        //if(col.transform.parent==null)continue;
                                        if(GameObject.ReferenceEquals(gameObject, col.transform.parent.gameObject))continue;
                                        Physics p = col.transform.parent.GetComponent<Physics>();

                                       // if(p==null){continue;} 
                                       if(col.gameObject.GetComponent<Tilemap>()==null){continue;}
                                        if(p!=null && zpos>=p.zpos+p.height){maxtilefloor=Mathf.Max(maxtilefloor,p.zpos+p.height);}
                                       
                                        Tilemap tilemap = col.gameObject.GetComponent<Tilemap>();
                                        Vector3 extremum=Vector3.zero;
                                        //test the 4 corners of this collider. only works for small things
                                        for(int j=0;j<4;j++){
                                            if(j==0){
                                                extremum = _collider2D.bounds.min;
                                            }else if(j==1){
                                                extremum = _collider2D.bounds.max;
                                            }else if(j==2){
                                                extremum = new Vector2(_collider2D.bounds.min.x,_collider2D.bounds.max.y);
                                            }else{
                                                extremum = new Vector2(_collider2D.bounds.max.x,_collider2D.bounds.min.y);

                                            }


                                                        TileBase tile = tilemap.GetTile(tilemap.WorldToCell((Vector3)extremum));
                                                        if(gameObject.name=="rock"){
                                                            print(col.transform.gameObject.name);
                                                            print(tile);
                                                        } 
                                                       
                                                        if(tile==null)continue;
                                                        //tilemap.SetColor(tilemap.WorldToCell((Vector3)extremum), new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
                                                        
                                                        float tilefloor=-1000000000f;
                                                        //should make this into function
                                                        switch (tile.name)
                                                            {
                                                                case "h0":
                                                                    tilefloor=0f;
                                                                    foundFloor=true;
                                                                    break;
                                                                case "h1":
                                                                    tilefloor=1f;
                                                                    foundFloor=true;
                                                                    break;
                                                                case "h2":
                                                                    tilefloor=2f;
                                                                    foundFloor=true;
                                                                    break;
                                                                case "h3":
                                                                    tilefloor=3f;
                                                                    foundFloor=true;
                                                                    break;
                                                                case "h4":
                                                                    tilefloor=4f;
                                                                    foundFloor=true;
                                                                    break;
                                                                case "hn5":
                                                                    tilefloor=-5f;
                                                                    foundFloor=true;
                                                                    break;
                                                                default:
                                                                    tilefloor=0f;
                                                                    break;
                                                            }
                                                            if(tilefloor>zpos){num_collisions++;}
                                                    
                                                        

                                                        
                                                            if(tilefloor>maxtilefloor){
                                                                maxtilefloor = tilefloor;
                                                                hitpoint=hit.point;
                                                            }                                            


                                        }
                                       
                                }//end of foreach
                                if(num_collisions>0){
                                     if(num_completed_iters==0){
                                        //try only moving x-axis. need to update them both
                                                        transform.position = new Vector3(lastPos.x,originalPos.y,0);
                                            }else if(num_completed_iters==1){
                                                //try only moving y-axis
                                                        transform.position = new Vector3(originalPos.x,lastPos.y,0);

                                            }

                                }else{
                                        if(!foundFloor && maxtilefloor<-200){maxtilefloor=0f;}
                
                            zfloor = maxtilefloor;
                            //print("GOT HERE");
                            
                            return;
                                }
                                
        }
                    //end of for
                    //move both axes
                if(!foundFloor){return;}
                if(maxtilefloor>zpos){ 
                    
                    transform.position = new Vector3(lastPos.x,lastPos.y,0);

                }else{
                 zfloor= maxtilefloor;// maxtilefloor;
                }
                return;
 
            
}

 public void ReactToFluid(){
    if(isTrigger)return;
           int num_completed_iters=0;
        List<RaycastHit2D> results=new List<RaycastHit2D>();
       
        float maxtilefloor=-1f;
        Physics2D.BoxCast(
                _collider2D.gameObject.transform.position,
                _collider2D.bounds.size, 
                0f,
                Vector2.zero, 
                _contactFilter,
                results,
                Mathf.Infinity);
   bool inWater=false;
        
                   // num_completed_iters++;
                     foreach(RaycastHit2D hit in results){

                
                        Collider2D col=hit.collider;
                        if(col.transform.parent==null)continue;

                        if(GameObject.ReferenceEquals(gameObject, col.transform.parent.gameObject))continue;
                        Tilemap tilemap = col.gameObject.GetComponent<Tilemap>();
                        if(tilemap==null)continue;
                        TileBase tile = tilemap.GetTile(tilemap.WorldToCell(hit.point));
                        if(tile==null)continue;
                        
                        if(tile.name =="WATER"){
                            float waterLevel=col.gameObject.GetComponent<PoolScript>().waterLevel;
                            if(zpos<=  waterLevel){
                                inWater=true;
                                        //react to water
                                        
                                    //float factor = (zfloor + waterLevel -zpos)/(waterLevel);
                                    zvel +=col.gameObject.GetComponent<PoolScript>().acceleration;
                                    if(!lockGravityScale){
                                        gravityScale=col.gameObject.GetComponent<PoolScript>().localGravity;
                                    }
                                    
                            }
                        }
                        
                          
                            

               
                }//end of for
                if(!inWater){
                    if(!lockGravityScale){
                         gravityScale=originalGravityScale;
                    }
                   
                    }
 
 }   

public void UpdateDeltaPos(){
   
    Vector3 currentPos = position;
    deltaPos = currentPos - lastPos;
    lastPos = currentPos;
}
void FixedUpdate(){
       
        if(fixX){xvel=0f;fixXvel=true;}
        if(fixY){yvel=0f;fixYvel=true;}
        if(fixZ){zvel=0f;fixZvel=true;}


        if(infinite_mass){
            inv_mass=0f;
        }else{
            inv_mass=1/mass;
        }
        if(isTrigger){
            if(transform.Find("collider")){
                 //transform.Find("collider").GetComponent<Collider2D>().enabled=false;
            }
           
        }

}

public bool IsGrounded(){
    return isGrounded;
}

 public void UpdateGrounded(){
    if(entitiesDirectlyBelow.Count>0 || zpos<=zfloor){
        isGrounded=true;
    }else {
        isGrounded=false;
    }
    if(isGrounded){
       // zvel=0f;
    }
}

public void PositionUpdate(){

    //apply gravity
        lastPos =new Vector3(transform.position.x,transform.position.y,zpos);
        if(!fixZvel){
            zvel += PhysicsSettings.gravity*gravityScale;
            if(zvel>PhysicsSettings.maxzvelup){
                zvel=PhysicsSettings.maxzvelup;
            }
            if(-zvel>PhysicsSettings.maxzveldown){
                zvel = -PhysicsSettings.maxzveldown;
            }
            //velocity+=Vector3.forward*PhysicsSettings.gravity*gravityScale;
        }
    


        //position update
       
        if(!fixZ){
            zpos+=zvel*PhysicsSettings.deltatime;
            //zpos=Mathf.Max(zfloor,zpos);
            //position+=Vector3.forward*velocity.z*PhysicsSettings.deltatime;

        }

        if(!fixX){
            transform.position+=Vector3.right*xvel*PhysicsSettings.deltatime;
            //position+=Vector3.right*velocity.x*PhysicsSettings.deltatime;

        }
        if(!fixY){
            transform.position+=Vector3.up*yvel*PhysicsSettings.deltatime;
           //position += Vector3.up*velocity.y*PhysicsSettings.deltatime;
        }
        //z must always be 0. the sprite has the z offsrt, not the parent.
        //transform.position = new Vector3(position.x,position.y,0);
        
         //position = new Vector3(transform.position.x,transform.position.y,zpos);
        //velocity = new Vector3(xvel,yvel,zvel);
        
     
}


//updates the entitiesabove list
 public void UpdateEntitiesAboveAndBelow(){

            List<RaycastHit2D> results=new List<RaycastHit2D>();
           
            int num_completed_iters=0;
            int num_entites_above_or_below=Physics2D.BoxCast(_collider2D.gameObject.transform.position,_collider2D.bounds.size, 0f, Vector2.zero, _contactFilter,results,Mathf.Infinity); 





            GameObject go=null;
            float mymax=-Mathf.Infinity;

            GameObject belowWithSpace=null;
            float highestBelow=-100000f;
            float smallestYBelow=10000000f;
                if(gameObject.name=="Player"){print(num_entites_above_or_below);}

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
                num_completed_iters++;

                //if at or below this...
                if(z <= zpos+height){
                    //NOTE:abs(zv)+abs(zvel) is probably too loose. prob also need slop
                    if(z+h< zpos+0.5 && 
                    z>=highestBelow /*&& other_collider.transform.parent.position.y<smallestYBelow*/){
                            highestBelow=z;
                            smallestYBelow=other_collider.transform.parent.position.y;
                            belowWithSpace=other_collider.transform.parent.gameObject;
                    }
                }


                //entites directly above this
                //probably loose
                 if(z >= zpos+height-0.5 && z <= zpos + height){
                    entitiesDirectlyAbove.Add(other_collider.transform.parent.gameObject);
                    other_collider.transform.parent.GetComponent<Physics>().entitiesDirectlyBelow.Add(gameObject);

                }
                //entities above
                if(z+h< zpos  + Mathf.Abs(zv)+Mathf.Abs(zvel) +PhysicsSettings.slop){
                    other_collider.transform.parent.GetComponent<Physics>().entitiesAbove.Add(gameObject);
                    entitiesBelow.Add(other_collider.transform.parent.gameObject);
                }
                
               

            }//end of for
               // UpdateGrounded();
            if (belowWithSpace!=null){
                entitiesDirectlyBelowWithSpace.Add(belowWithSpace);
                belowWithSpace.GetComponent<Physics>().entitiesDirectlyAboveWithSpace.Add(gameObject);

                float tzp=belowWithSpace.GetComponent<Physics>().zpos;
                float th=belowWithSpace.GetComponent<Physics>().height;
                if(zpos>tzp+th){
                   // zfloor=tzp+th;
                    //print(tzp+th);
                    }

                }else{
                    zfloor=0f;
                }
            
}



public void SetParents(){

  if(entitiesDirectlyBelow.Count>0){
                    //xvel = entitiesDirectlyBelow[0].GetComponent<Physics>().xvel;
                   // yvel = entitiesDirectlyBelow[0].GetComponent<Physics>().yvel;
                   transform.parent = entitiesDirectlyBelow[0].transform;
                  } else{
                    transform.parent = null;
                  }
}
public void ResolveCollisions(){
        if(isTrigger)return;
        List<RaycastHit2D> results=new List<RaycastHit2D>();
       

        int numhits=0;       
        int num_completed_iters=0;

        //ground collision
            if(zpos<zfloor){  
                    //zpos=zfloor;
                    //zvel=0;//-restitution*zvel;   
                   
                    if(!fixZ){
                      zpos=zfloor;  
                    }
                    if(!fixZvel){
                          zvel=0f; 
                    }
                 
            }
/*
          //ground friction
          if(entitiesDirectlyBelow.Count==0 && zpos<=zfloor){
                if(!fixXvel){
                    xvel*=0.99f;
                    //velocity.x*=0.99f;
                }
                if(!fixYvel){
                    yvel*=0.99f;
                    //velocity.y*=0.99f;
                }
            }
            */

            //parenting
           /* if(entitiesDirectlyBelow.Count>0){
                    //xvel = entitiesDirectlyBelow[0].GetComponent<Physics>().xvel;
                   // yvel = entitiesDirectlyBelow[0].GetComponent<Physics>().yvel;
                   transform.parent = entitiesDirectlyBelow[0].transform;
                  } else{
                    transform.parent = null;
                  }
*/
         float maxfloor=-1000000f;
        while(num_completed_iters<1 &&

              Physics2D.BoxCast(
                _collider2D.gameObject.transform.position,
                _collider2D.bounds.size, 
                0f,
                Vector2.zero, 
                _contactFilter,
                results,
                Mathf.Infinity)>=0){

            num_completed_iters++;

            
            //block to block collisions
            foreach(RaycastHit2D hit in results){

                //skip self and skip non physicsentities
                Collider2D other_collider=hit.collider;
                if(other_collider.gameObject.tag!="PhysicsEntity")continue;
                if(GameObject.ReferenceEquals(gameObject, other_collider.transform.parent.gameObject))continue;
               
               
                #region retrieve variables of other object
                Physics other_physics = other_collider.transform.parent.GetComponent<Physics>();
	                float x= other_collider.transform.position.x;
	                float y= other_collider.transform.position.y;
	                float z =other_physics.zpos;
	                float zv=other_physics.zvel;
	                float w=other_physics.width;
	                float xv=other_physics.xvel;
	                float h=other_physics.height;
	                float d=other_physics.depth;
	                float yv=other_physics.yvel;
	                float zf=other_physics.zfloor;
	                float m=other_physics.mass;
	                float r=other_physics.restitution;
                    float other_df=other_physics.dynamicFriction;
                    float other_sf=other_physics.staticFriction;
                    float im= other_physics.inv_mass;
                    Vector3 other_pos = other_physics.position;
                    Vector3 other_vel = other_physics.velocity;
                    bool it = other_physics.isTrigger;

                #endregion

                if(it)continue;
                //if not colliding
                //if i'm above it
                if(zpos>z+h){
                     maxfloor = Mathf.Max(z+h,maxfloor);
                }
               //if im below it
                if((zpos > z+h || zpos+height<z )){continue;}

  
                numhits++;

              
                //relative velocity of this object relative to other object. 
                //arrow is from other object to this object
                Vector3 relative_velocity = new Vector3(xvel-xv,yvel-yv,zvel-zv);
                                            
                //vector from this to other
                Vector3 relative_position = new Vector3(other_collider.transform.position.x-_collider2D.transform.position.x,
                                                        other_collider.transform.position.y-_collider2D.transform.position.y,
                                                        z-zpos);
                Vector3 penetration_depth = new Vector3(
                                        relative_position.x>0?//this object is to the left of other object
                                                Mathf.Abs(_collider2D.transform.position.x+width/2 - (other_collider.transform.position.x-w/2)):
                                                Mathf.Abs(other_collider.transform.position.x+w/2 - (_collider2D.transform.position.x-width/2)),

                                        relative_position.y>0?//this object is front of the other object
                                                Mathf.Abs(_collider2D.transform.position.y+depth/2 - (other_collider.transform.position.y-d/2)):
                                                Mathf.Abs(other_collider.transform.position.y+d/2 - (_collider2D.transform.position.y-depth/2)),
                                        //relative_position.y>0?Mathf.Abs(_collider2D.transform.position.y+depth - (other_collider.transform.position.y)):Mathf.Abs(other_collider.transform.position.y+d - (_collider2D.transform.position.y)),

                                        relative_position.z>0?//this object is under the other object
                                                Mathf.Abs(zpos+height- z):
                                                Mathf.Abs(z+h-zpos)
                                        );
                float reciprocal_mass_sum = im+inv_mass;
               

                //this will point in the direction THIS object will be moved after the collision.("The collision normal is the direction in which the impulse will be applied.")
                //The penetration depth (along with some other things) determine how large of an impulse will be used.
                // the dot product of this and relative velocity must be negative
                Vector3 normal = Vector3.zero;

                float smallest_axis = Mathf.Min(penetration_depth.x,Mathf.Min(penetration_depth.y,penetration_depth.z));
               
                #region compute collision normal based on rel. position
	                if(smallest_axis == penetration_depth.x){
	                    //if this object is to the right, move it to the right, otherwise left
	                    normal = Vector3.right;//0,0,1

	                    if(relative_position.x>0){normal*=-1;}
	
	                }else if(smallest_axis == penetration_depth.y){
                        //if this object is behind, move it "up", else move it down
	                    normal = Vector3.up;//0,1,0
	                    if(relative_position.y>0){normal*=-1;}
	
	                }else/* if(smallest_axis==penetration_depth.z)*/{
                        //if this object is above, move it up, else move it down
	                    normal = Vector3.forward;//0,0,1
	                    if(relative_position.z>0){normal*=-1;}
	
	                }
                #endregion
                //restitution is form 0 to 1, where 0 is perfectly inelastic, 1 is perfectly elastic
                float min_restitition = Mathf.Min(restitution,r);

                float velocity_along_normal=Vector3.Dot(relative_velocity,normal);
                // print("PENETRATION DEPTH: "+gameObject.name + " , "+other_collider.transform.parent.gameObject.name+" , " + penetration_depth);
               
                //if the velocities are already separating, do not resolve. could cause phasing if velocities are separating but actual motion is not
                if(velocity_along_normal>=0){
           /*         
                    #region preliminary position correction

	                 if(smallest_axis<=PhysicsSettings.slop){continue;}
	                   
		                if(smallest_axis == penetration_depth.x){
		                     smallest_axis-=PhysicsSettings.slop;
	                    smallest_axis*=PhysicsSettings.correctionRatio;
	
		                    transform.position += normal* inv_mass*(smallest_axis) /( reciprocal_mass_sum);
		                    other_collider.transform.parent.position -= im* normal*(smallest_axis)/(reciprocal_mass_sum);
		
		                }else if(smallest_axis == penetration_depth.y){
	                         smallest_axis-=PhysicsSettings.slop;
	                    smallest_axis*=PhysicsSettings.correctionRatio;
	
		                    transform.position += normal* inv_mass*(smallest_axis) / (reciprocal_mass_sum);
		                    other_collider.transform.parent.position -= normal* im *(smallest_axis) /(reciprocal_mass_sum);
		
		                }else if(smallest_axis==penetration_depth.z){ 
	                        smallest_axis-=PhysicsSettings.slop;
	                        //only the one on top gets moved
	                        if(relative_position.z>0){
	                           
	                            other_collider.transform.parent.GetComponent<Physics>().zpos -= (normal.z)*(smallest_axis);
	
	                            //entitiesDirectlyAbove.Add(other_collider.transform.parent.gameObject);
	                            //other_collider.transform.parent.GetComponent<Physics>().entitiesDirectlyBelow.Add(gameObject);
	                        }else{
	                            zpos+= (normal.z)*(smallest_axis);
	
	                            //entitiesDirectlyBelow.Add(other_collider.transform.parent.gameObject);
	                            //other_collider.transform.parent.GetComponent<Physics>().entitiesDirectlyAbove.Add(gameObject);
	
	
	                        }
		                    //zpos+= (normal.z)*(smallest_axis) /(mass* reciprocal_mass_sum);
		                    //other_collider.transform.parent.GetComponent<Physics>().zpos -= (normal.z)*(smallest_axis)  /(m * reciprocal_mass_sum);
		
		                }
#endregion
  */
                   // return;
                }
            

                Vector3 impulse = normal* -(1+min_restitition) * velocity_along_normal / reciprocal_mass_sum;
                
                #region velocity computation
                
                if(velocity_along_normal<0){

                  	    if(!other_physics.fixXvel){
	                          other_physics.xvel-= impulse.x*im;
	                    }
	                    if(!other_physics.fixYvel){
	                          other_physics.yvel-= impulse.y*im;
	
	                    }
	                    if(!other_physics.fixZvel){
	                             other_physics.zvel-= impulse.z*im;
	
	                    }
	                    if(!fixXvel){
	                        xvel +=impulse.x*inv_mass;
	
	                    }
	                    if(!fixYvel){
	                        yvel +=impulse.y*inv_mass;
	
	                    }
	                    if(!fixZvel){
	                        zvel += impulse.z*inv_mass;
	                    }  

                }

#endregion

                #region positional correction
                if(smallest_axis<=PhysicsSettings.slop){continue;}
                   
	                if(smallest_axis == penetration_depth.x){
	                    smallest_axis-=PhysicsSettings.slop;
                        smallest_axis*=PhysicsSettings.correctionRatio;

	                    //transform.position += normal* inv_mass*(smallest_axis) /( reciprocal_mass_sum);
	                    //other_collider.transform.parent.position -= im* normal*(smallest_axis)/(reciprocal_mass_sum);
	
                        position += normal* inv_mass*(smallest_axis) /( reciprocal_mass_sum);
                        other_physics.position -= im* normal*(smallest_axis)/(reciprocal_mass_sum);


	                }else if(smallest_axis == penetration_depth.y){
                        smallest_axis-=PhysicsSettings.slop;
                        smallest_axis*=PhysicsSettings.correctionRatio;

	                    //transform.position += normal* inv_mass*(smallest_axis) / (reciprocal_mass_sum);
	                    //other_collider.transform.parent.position -= normal* im *(smallest_axis) /(reciprocal_mass_sum);

                        position += normal* inv_mass*(smallest_axis) /( reciprocal_mass_sum);
                        other_physics.position -= im* normal*(smallest_axis)/(reciprocal_mass_sum);
	
	                }else if(smallest_axis==penetration_depth.z){ 
                        smallest_axis-=PhysicsSettings.slop;
                        smallest_axis*=PhysicsSettings.correctionRatio;
                       

                        //position += normal* inv_mass*(smallest_axis) /( reciprocal_mass_sum);
                        //other_physics.position -= im* normal*(smallest_axis)/(reciprocal_mass_sum);
                        if(relative_position.z>0){
                           //only correct the one on top(fully)
                            other_collider.transform.parent.GetComponent<Physics>().zpos -= (normal.z)*(smallest_axis);

                            //entitiesDirectlyAbove.Add(other_collider.transform.parent.gameObject);
                            //other_collider.transform.parent.GetComponent<Physics>().entitiesDirectlyBelow.Add(gameObject);
                        }else{
                           // if(!fixZ){
                        //         zpos+= (normal.z)*(smallest_axis);
                         //   }
                           

                            //entitiesDirectlyBelow.Add(other_collider.transform.parent.gameObject);
                            //other_collider.transform.parent.GetComponent<Physics>().entitiesDirectlyAbove.Add(gameObject);


                        }
	                    //zpos+= (normal.z)*(smallest_axis) /(mass* reciprocal_mass_sum);
	                    //other_collider.transform.parent.GetComponent<Physics>().zpos -= (normal.z)*(smallest_axis)  /(m * reciprocal_mass_sum);
	
	                }
                    
                #endregion
 
                #region friction computation
	            /*  //  float j = -(1 + e) * velAlongNormal
	                    Vec2 rv = VB - VA
	                    Vec2 tangent = rv - Dot( rv, normal ) * normal
	                    tangent.Normalize( )
	
	                    float jt = -Dot( rv, tangent )
	                    jt = jt / (1 / MassA + 1 / MassB)
	
	                    float mu = PythagoreanSolve( A->staticFriction, B->staticFriction )
	
	                    Vec2 frictionImpulse
	                    if(abs( jt ) < j * mu)
	                    frictionImpulse = jt * tangent
	                    else
	                    {
	                    dynamicFriction = PythagoreanSolve( A->dynamicFriction, B->dynamicFriction )
	                    frictionImpulse = -j * tangent * dynamicFriction
	                    }
	                    // Apply 
	                    A->velocity -= (1 / A->mass) * frictionImpulse
	                    B->velocity += (1 / B->mass) * frictionImpulse
	                    
	                   
                        
	                    Vector3 tangent = relative_velocity;// - Vector3.Dot(relative_velocity,normal)*normal;
                        tangent = Vector3.Dot(tangent,Vector3.right)*Vector3.right;
	                    Vector3.Normalize(tangent);
                        
	                    float jt = -Vector3.Dot(relative_velocity,tangent);
	                    jt = jt/reciprocal_mass_sum;
	
	                    float collision_mu = Mathf.Sqrt(staticFriction*staticFriction + other_sf*other_sf);
	                    Vector3 friction_impulse = Vector3.zero;
                        float j = -(1+ min_restitition) * velocity_along_normal ;
	                    if(Mathf.Abs(jt)< j* collision_mu){
	                        friction_impulse = jt*tangent;
	                    }else{
	                        float collision_df = Mathf.Sqrt(dynamicFriction*dynamicFriction +other_df*other_df );
	                        friction_impulse = -j*tangent*collision_df;
	                    }
	
	                    xvel+=friction_impulse.x*inv_mass;
	                    yvel+=friction_impulse.y*inv_mass;
	                    zvel+=friction_impulse.z*inv_mass;
	
	                      other_collider.transform.parent.GetComponent<Physics>().xvel-=friction_impulse.x*im;
	                        other_collider.transform.parent.GetComponent<Physics>().yvel-=friction_impulse.y*im;
	                          other_collider.transform.parent.GetComponent<Physics>().zvel-=friction_impulse.z*im;

                              */
                        #endregion

                  
                  
            
         }//end of foreach
         zfloor = Mathf.Max(zfloor,maxfloor);
       }//end of while 
      
    }//end of function

        
}//end of class



    
