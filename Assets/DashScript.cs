using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DashScript : MonoBehaviour
{
    public bool isDashing=false;
    public Rigidbody2D rb;
    public AudioSource audioSource;
    public AudioClip dashSFX;
    public float dashTime=3f;
    public float dashDistance=3f;
    public Animator anim;

    public UnityEvent OnCompleteDash;
    void Awake()
    {
        audioSource=GetComponent<AudioSource>();
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Dash(Vector2 directionToDash){
         audioSource.PlayOneShot(dashSFX);
    
        print("started dashing");
        float timer=0f;
       

      //  if(directionToDash==Vector2.zero){yield break;}
         anim.SetTrigger("dash");
       
        Vector2 startPos=(Vector2)transform.position;
        Vector2 destination = (Vector2)transform.position + dashDistance*directionToDash;
        
        directionToDash=directionToDash.normalized;
        isDashing=true;
        
        rb.velocity=Vector2.zero;
      
        while((Vector2)transform.position!=destination){

            Vector2 temp=Vector2.Lerp(startPos,destination,timer/dashTime);
            rb.MovePosition(temp);
            timer+=Time.deltaTime;
            yield return null;
        }
       
        isDashing=false;
        
       

        print("done dashing");
        OnCompleteDash.Invoke();

}

}
