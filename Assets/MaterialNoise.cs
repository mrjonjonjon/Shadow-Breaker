using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialNoise : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material.SetFloat("Vector1_620eb0a4fc1a48c79d9daecb584075d4",Random.Range(0.3f,0.6f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
