using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordFXController : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    ParticleSystem ps;
    public List<AudioClip> swordSwingSFX=new List<AudioClip>();
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        ps=GetComponentInChildren(typeof(ParticleSystem)) as ParticleSystem;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.P)){
          //  PlaySwordSwing();
        }
        
    }
    public void PlaySwordSwing(){
        //print(e.animatorClipInfo.weight);
        audioSource.PlayOneShot(swordSwingSFX[Random.Range(0,swordSwingSFX.Count)]);
      // PlayVFX();


    }

    void PlayVFX(){
          Component[] particleSystems=transform.Find("FrontOfPlayer/slashvfx").GetComponentsInChildren<ParticleSystem>();
          foreach(ParticleSystem ps in particleSystems){
               ps.Stop();
               ps.Play();
          }
    }
}
