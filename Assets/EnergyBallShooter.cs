using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBallShooter : MonoBehaviour
{
        public GameObject EnergyBall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootEnergyBall(Vector2 startPos,Vector2 dir,float velocitymultipler=1f){
        GameObject ball= Instantiate(EnergyBall,startPos,Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().velocity=dir.normalized*velocitymultipler;
    }
}
