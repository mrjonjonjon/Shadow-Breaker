using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour
{
    public float arrowSpeed=2f;
    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootArrow(){

        GameObject arrowclone=Instantiate(arrow,transform.position,GetComponent<MovementController>().GetRotationOfPlayer() );
        arrowclone.GetComponent<Rigidbody2D>().velocity=arrowSpeed* GetComponent<MovementController>().GetForwardOfPlayer();
    }
}
