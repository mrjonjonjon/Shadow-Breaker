using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public Vector2 startVelocity;
    public AnimationCurve ac;
    float t;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        startVelocity=rb.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        t+=Time.deltaTime;
        rb.velocity=startVelocity* ac.Evaluate(t);
    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("wall")){
             transform.DetachChildren();

            Destroy(gameObject);
        }
    }
}
