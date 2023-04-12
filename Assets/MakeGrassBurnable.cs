using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGrassBurnable : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem ps;
    void Start()
    {
        ps=GetComponent<ParticleSystem>();
         var trigger = GetComponent<ParticleSystem>().trigger;
        trigger.enabled = true;
        GameObject[] allGrass=GameObject.FindGameObjectsWithTag("foliage");
        foreach(GameObject go in allGrass){
             trigger.AddCollider(go.GetComponent<BoxCollider2D>());
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnParticleTrigger()
         {
             //Debug.Log("FireCollision.OnParticleTrigger");
 
             // particles
             List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
 
             // get
             int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, inside, out var insideData);
 
             // iterate
             for (int i = 0; i < numInside; i++)
             {
                             ParticleSystem.Particle p = inside[i];

                 if (insideData.GetColliderCount(i) >= 1)
                 {
                     for(int j=0;j<1;j++){

                    var other = insideData.GetCollider(i, j);
                                        other.GetComponent<MeshRenderer>().material.SetColor("_Color",Color.black);
                                        other.GetComponent<MeshRenderer>().material.SetFloat("Vector1_2d61041f8dfd46289cb8aafd27290417",0f);
                                        
                                                                other.GetComponent<MeshRenderer>().material.SetFloat( "Vector1_e19a6f73e9824493998ba7bebae9c03c"  ,0f);
                                                                print(p.remainingLifetime);
                                        p.remainingLifetime=5f;
                                        inside[i]=p;
                                        
                     }
                   
                    
                 }
                 
             }
                     ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);

         }

    
}
