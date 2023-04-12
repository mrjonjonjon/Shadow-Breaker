using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Events;

public class PhysicsManager : MonoBehaviour
{


   public UnityEvent OnFinishAllPhysicsUpdates;
    public int totalPhysicsUpdates=0;


    public static PhysicsManager instance;
    public List<Physics> allObjects;
   public static int maxLevels=50;
    public int idCounter=0;
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

        //assign each object an ID
        idCounter=0;
          foreach(Physics p in allObjects){
                p.entityID=idCounter;
                idCounter++;
        }
      
      
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
             
        }
      
         //player-specific physics updates 
        // MovementController.instance.handleHook();
         //MovementController.instance.SetVelocity();
         // MovementController.instance.handleJumping();
        
         
        //update positions based on velocities
        foreach(Physics p in allObjects){
                if(p.entityType==Physics.EntityType.Prop){continue;}
                p.PositionUpdate();
               
                p.positionUpdated=true;
               
        }  

        //update above and below
        foreach(Physics p in allObjects){
                if(p.entityType==Physics.EntityType.Prop){continue;}
                p.UpdateEntitiesAboveAndBelow();
        }
       
           //resolve resulting collisions,setting resultand positions and velocities
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

        foreach(Physics p in allObjects){
                    if(p.entityType==Physics.EntityType.Prop){continue;}
                    p.ReactToFluid();
                      
        }

         foreach(Physics p in allObjects){
            p.UpdateDeltaPos();
         }

         OnFinishAllPhysicsUpdates.Invoke();
        totalPhysicsUpdates+=1;
    }
}
