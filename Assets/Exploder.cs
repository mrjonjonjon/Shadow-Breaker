using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Exploder : MonoBehaviour
{

    public GameObject explosionPrefab;
    public GameObject warningPrefab;
    public float screenshakeIntensity=0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnWarning(Vector2 pos){
        GameObject warning=Instantiate(warningPrefab,pos,Quaternion.identity);
    }
    public void SpawnExplosion(Vector2 pos){
        GameObject explosion=Instantiate(explosionPrefab,pos,Quaternion.identity);
        GetComponent<CinemachineImpulseSource>().GenerateImpulse((Vector3)Random.insideUnitCircle.normalized*screenshakeIntensity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
