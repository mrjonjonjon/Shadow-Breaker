using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyScript : MonoBehaviour
{
    public GameObject bloodSplat;
    public int hp=5;
    public float knockbackMultiplier=0f;
    public Material flashMaterial;
     Material oc;
     ParticleSystem ps;
     public UnityEvent OnDie;
     bool dead=false;

     public bool parryable=false;
    // Start is called before the first frame update
    void Start()
    {
          oc =   GetComponent<SpriteRenderer>().material;
          ps=transform.Find("Particles").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die(){
        dead=true;
        transform.DetachChildren();
      
        OnDie.Invoke();
        Destroy(gameObject);
    }
    public IEnumerator FlashWhite(float amountOfHitstop){
       
        GetComponent<SpriteRenderer>().material=flashMaterial;
        yield return new WaitForSecondsRealtime(amountOfHitstop);
        GetComponent<SpriteRenderer>().material=oc;
    }
//from is contact poitn of damage
    public void TakeDamage(Vector3 from, int damage=1){
        if(dead)return;
        GetComponent<Animator>().SetTrigger("hurt");
       
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
        GetComponent<Rigidbody2D>().AddForce(knockbackMultiplier*(Vector2)temp,ForceMode2D.Impulse);
        hp-=damage;
        if(hp<=0){
            Die();
        }
        
    }
}
