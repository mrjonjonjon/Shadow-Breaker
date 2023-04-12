using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSWrap : MonoBehaviour
{
ParticleSystem.Particle[] mCurrentParticles_ForManualUpdate;
 ParticleSystem ps;
 public float length;

 void Start(){        ps=GetComponent<ParticleSystem>();

     mCurrentParticles_ForManualUpdate = new ParticleSystem.Particle[ps.main.maxParticles]; 

 }
private void LateUpdate()
    {
        
 
        // Check
        if (true)
        {
           
            // GetParticles is allocation free because we reuse the m_Particles buffer between updates
            int numParticlesAlive = ps.GetParticles(mCurrentParticles_ForManualUpdate);
 
            // Iterate through alive particles
            for (int i = 0; i < numParticlesAlive; i++)
            {
               
                    mCurrentParticles_ForManualUpdate[i].position = new Vector3(
                        Mathf.Repeat(mCurrentParticles_ForManualUpdate[i].position.x+length,2*length)-length,
                        Mathf.Repeat(mCurrentParticles_ForManualUpdate[i].position.y+length,2*length)-length,
                        Mathf.Repeat(mCurrentParticles_ForManualUpdate[i].position.z+length,2*length)-length
                    );                
                
            }
 
            // Apply the particle changes to the Particle System
            ps.SetParticles(mCurrentParticles_ForManualUpdate, numParticlesAlive);
        }
    }   
}
