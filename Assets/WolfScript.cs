using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfScript : MonoBehaviour
{
    Vector3 playerPos;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        playerPos=MovementController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos=MovementController.instance.transform.position;
        //rb.MovePosition();
        
    }
}
