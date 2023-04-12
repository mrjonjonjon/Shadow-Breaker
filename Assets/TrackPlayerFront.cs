using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayerFront : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = MovementController.instance.GetFrontOfPlayer();
        transform.rotation=MovementController.instance.GetRotationOfPlayer();
    }
}
