using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReScanner : MonoBehaviour
{

    public float rescanTime;
    // Start is called before the first frame update
    void Start()
    {
                InvokeRepeating("RescanGraph",0f,rescanTime);

    }

    void RescanGraph(){
        AstarPath.active.Scan();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
