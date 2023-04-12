using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("slash")){
           print("HIT");
       RaycastHit2D hit = Physics2D.Raycast(transform.position, rb.velocity);
         
            rb.velocity*=-1;//=2*(Vector2) Vector3.Reflect((Vector3)rb.velocity, (Vector3)(col.transform.position-transform.position).normalized);
        }
    }
}
