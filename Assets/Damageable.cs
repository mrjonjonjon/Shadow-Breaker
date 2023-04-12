using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Pathfinding;

public class Damageable : MonoBehaviour
{
    public GameObject bloodSplat;
    public int hp=5;
    public float knockbackMultiplier=0f;
    public Material flashMaterial;
    public Animator anim;
     Material oc;
     public ParticleSystem ps;
     public UnityEvent OnDie;
     bool dead=false;
     public AudioClip parriedSFX;
     public AudioSource audioSource;
     public Rigidbody2D rb;
     public bool parrying=false;


     public bool parryable=false;
     public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {

          oc =   sr.material;
         // ps=transform.Find("Particles").GetComponent<ParticleSystem>();
          //anim=GetComponent<Animator>();
        
          audioSource=GetComponent<AudioSource>();
         

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
public void Parried(){
    anim.SetTrigger("parried");
    audioSource.PlayOneShot(parriedSFX);

}
    public void Die(){
        dead=true;
        transform.DetachChildren();
        //spawner is subscribed to ondie

        //rigidbody stuff
       // rb.gravityScale=1;
        //rb.drag=0;
        //rb.constraints=RigidbodyConstraints2D.None;
        //rb.velocity=5*(Random.insideUnitCircle.normalized);
        //rb.angularVelocity=600f;


        OnDie.Invoke();
       
        transform.parent.GetComponent<AIPath>().enabled=false;
        
          SelfDestruct sd = transform.parent.gameObject.AddComponent(typeof(SelfDestruct)) as SelfDestruct;
          anim.enabled=false;
        GetComponent<Collider2D>().enabled=false;
        transform.parent.Find("sprite").GetComponent<SpriteRenderer>().color=Color.black;

          sd.selfdestruct_in=3.0f;
      
    }

    public IEnumerator FlashWhite(float amountOfHitstop){
       
        sr.material=flashMaterial;
        yield return new WaitForSecondsRealtime(amountOfHitstop);
        sr.material=oc;
    }

     //from is contact poitn of damage
    public void TakeDamage(Vector3 from, int damage=1){
        if(dead)return;
       anim.SetTrigger("hurt");
       
        Vector3 temp=(transform.position-from).normalized;
        temp.z=0;
        temp.x+=Random.Range(-0.5f,0.5f);
                temp.y+=Random.Range(-0.5f,0.5f);

        if(hp==1 || Random.Range(0,2)==1){
            Instantiate(bloodSplat,transform.position,Quaternion.FromToRotation(bloodSplat.transform.up, temp));
        }
        
        if(Random.Range(0,3)==0||hp==1){
             ps.Play();
        }

        //rb.AddForce(knockbackMultiplier*(Vector2)temp,ForceMode2D.Impulse);
        
        hp-=damage;
        if(hp<=0){
            Die();
        }
        
    }
}
