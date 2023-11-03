using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class PhysicsManager : MonoBehaviour
{


   public UnityEvent OnFinishAllPhysicsUpdates;
    public int totalPhysicsUpdates=0;

#region declarations
    public static PhysicsManager instance;
    public List<Physics> allObjects;
    public List<Stairs> allStairs;
    public static int maxLevels=50;
    public int idCounter=0;

#endregion


    //add object to phyics engine update queue
   public void RegisterEntity(Physics p){
    allObjects.Add(p);
    p.entityID=idCounter;
    idCounter++;
   }



   
    void Start(){
        if(instance==null){
            instance=this;
        }else{
            Destroy(this);
        }

        allObjects=new List<Physics>(FindObjectsOfType<Physics>());
        allStairs = new List<Stairs>(FindObjectsOfType<Stairs>());

        //assign each object an ID
        idCounter=0;
          foreach(Physics p in allObjects){
                p.entityID=idCounter;
                idCounter++;
        }
        /*
        int CompareByZPos(Physics a, Physics b) {
            if (a.zpos < b.zpos) {
                return -1;
            } else if (a.zpos > b.zpos) {
                return 1;
            } else {
                return 0;
            }
        }

        // sort the list using the custom comparison function
        allObjects.Sort(CompareByZPos);
        */
        
      
        // allObjects=;
    }
    public void ResolveAllCollisions(){
        for(int i=0;i<PhysicsSettings.num_iterations;i++){
                foreach(Physics p in allObjects){
                    if(p.isTrigger){continue;}

                    p.ResolveCollisions();
                    p.collisionsResolved=true;
                
                }
           }
            foreach(Physics p in allObjects){
                    if(p.entityType==Physics.EntityType.Prop){continue;}
                    p.SetZFloor();       
        }

    }
    void FixedUpdate(){ 
        
        //initialize object properties
        foreach(Physics p in allObjects){
           
              p.entitiesDirectlyAbove.Clear();
              p.entitiesDirectlyBelow.Clear();
              p.entitiesDirectlyAboveWithSpace.Clear();
              p.entitiesDirectlyBelowWithSpace.Clear(); 
              p.entitiesAbove.Clear();
              p.entitiesBelow.Clear();
              p.positionUpdated=false;
              p.collisionsResolved=false;
              //p.zfloor=0f;
             
        }
      
         //player-specific physics updates 
        // MovementController.instance.handleHook();
         //MovementController.instance.SetVelocity();
         // MovementController.instance.handleJumping();
        
         
        //add gravity and update positions based on velocities
        foreach(Physics p in allObjects){
                if(p.entityType==Physics.EntityType.Prop){continue;}
                p.PositionUpdate();
               
                p.positionUpdated=true;
               
        }  

      
           //resolve resulting collisions,setting resultand positions and velocities
           for(int i=0;i<PhysicsSettings.num_iterations;i++){
                foreach(Physics p in allObjects){
                    if(p.isTrigger){continue;}

                    p.ResolveCollisions();
                    p.collisionsResolved=true;
                
                }
           }

        //update above and below
        foreach(Physics p in allObjects){
                if(p.entityType==Physics.EntityType.Prop){continue;}
                p.UpdateEntitiesAboveAndBelow();
        }
       
        foreach(Physics p in allObjects){
                    if(p.entityType==Physics.EntityType.Prop){continue;}
                   p.SetZFloor();       
        }
        foreach(Stairs s in allStairs){
            s.StairsUpdate();
        }

        foreach(Physics p in allObjects){
                    if(p.entityType==Physics.EntityType.Prop){continue;}
                    p.ReactToFluid();         
        }

         foreach(Physics p in allObjects){
            p.UpdateDeltaPos();
         }
           foreach(Physics p in allObjects){
            p.UpdateGrounded();
         }

         foreach(Physics p in allObjects){
           p.SetParents();
         }

        OnFinishAllPhysicsUpdates.Invoke();
        totalPhysicsUpdates+=1;
    }
}
