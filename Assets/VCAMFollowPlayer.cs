using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCAMFollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow=MovementController.instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
