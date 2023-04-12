using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using TMPro;
using UnityEngine.UI;
public class SuperMoveScript2 : MonoBehaviour
{
    public GameObject projectile;
    public float timeBetweenSlashes=0.1f;
    public GameObject specialMoveCam;
    public GameObject vcamtriggers;
    public PlayableDirector director;
    public int numProjectiles;
    public float projectileSpeed;
    public Slider sli;
    public float timeToSlash=3.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)){
            DoMove();
        }
    }

    void DoMove(){
        director.Play();
        if(vcamtriggers!=null){
               vcamtriggers.SetActive(false);
        }
        specialMoveCam.SetActive(true);
     
        Vector3 temp=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        temp.z=-10;
        specialMoveCam.transform.position=temp;
                

        
        
        StartCoroutine(projectileAttack());
        
    }

    IEnumerator projectileAttack(){
        /*
        for(int i=0;i<numProjectiles;i++){
            
                GetComponent<Animator>().Play("Base Layer.Attack.attack blend1", 0, 0.37f);
                ShootProjectile();
                yield return new WaitForSeconds(timeBetweenSlashes);



        }
    */
    float t=0;
    while(t<timeToSlash){

         if(Input.GetKeyDown(KeyCode.P)){
              // GetComponent<Animator>().Play("Base Layer.Attack.attack blend1", 0, 0.37f);
                ShootProjectile();
               
               
        } 
        
        t+=Time.deltaTime;
        float z=timeToSlash-t;
       // tmp.text="TIME REMAINING: "+z.ToString("F2");
        sli.value=z;
         yield return null;
    }
        
      
                  //director.Stop();
                //Camera.main.GetComponent<CinemachineBrain>().enabled=true;
                        specialMoveCam.SetActive(false);
                        if(vcamtriggers!=null){
                              vcamtriggers.SetActive(true);
                        }
                              
                                sli.gameObject.SetActive(false);
                                transform.rotation=Quaternion.identity;



    }

    void ShootProjectile(){
           GameObject p= Instantiate(projectile,MovementController.instance.GetFrontOfPlayer(),MovementController.instance.GetRotationOfPlayer());
           p.GetComponent<Rigidbody2D>().velocity=MovementController.instance.GetForwardOfPlayer() * projectileSpeed;


    }

    float round(float x){
        return (float)System.Math.Round(x,2);
    }
}
