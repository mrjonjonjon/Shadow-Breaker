using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour
{
    AudioSource audioSource;
    Animator anim;
    public AudioClip footStepSound;
    public float footStepInterval=0.5f;

 void Start()
    {
        
        audioSource=GetComponent<AudioSource>();
        anim=GetComponent<Animator>();
        StartCoroutine(footsteps());
    }

    IEnumerator footsteps(){
        while(true){
            if(anim.GetBool("running")){
                 audioSource.PlayOneShot(footStepSound);
                 transform.Find("Particles").GetComponent<ParticleSystem>().Play();
            
            }
           yield return new WaitForSeconds(footStepInterval);
        }
      
    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
    }
}
