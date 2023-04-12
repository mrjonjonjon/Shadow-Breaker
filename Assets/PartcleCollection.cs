using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartcleCollection : MonoBehaviour
{

    public AudioClip particleCollectionSFX;
    AudioSource audioSource;
    public ParticleSystem ps;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        ps = GetComponent<ParticleSystem>();
       
    }

    void OnParticleCollision(GameObject other)
    {
       // print("zxzxzxzxzzx");
        audioSource.PlayOneShot(particleCollectionSFX);
        //int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);

       
       // int i = 0;

    }
}
