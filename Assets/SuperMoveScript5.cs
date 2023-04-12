using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMoveScript5 : MonoBehaviour
{
    public GameObject movementClone;
    public Vector2 offsetFromPlayer;
    public float delay;
    public int numActiveMovementClones=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void DecrementNumActiveMovementClones(){
        numActiveMovementClones--;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)){
            GameObject clone=Instantiate(movementClone,transform.position + (Vector3)offsetFromPlayer,movementClone.transform.rotation);
            clone.transform.GetComponent<MovementCloneScript>().frameDelay=(numActiveMovementClones+1)*10;
            clone.transform.GetComponent<SelfDestruct>().OnSelfDestruct.AddListener(DecrementNumActiveMovementClones);
            numActiveMovementClones++;
        }
    }
}
