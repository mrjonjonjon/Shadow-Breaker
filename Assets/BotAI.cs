using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAI : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bulletPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)){
            Shoot(Random.insideUnitCircle);
        }
    }

    public void Shoot(Vector2 dir){
        GameObject bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity=dir.normalized;
    }
}
