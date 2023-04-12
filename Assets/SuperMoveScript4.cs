using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMoveScript4 : MonoBehaviour
{
    public GameObject rapidSlashShadowClone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            GameObject clone=Instantiate(rapidSlashShadowClone,transform.position,rapidSlashShadowClone.transform.rotation);
            clone.GetComponent<Animator>().SetTrigger("attack");
            //clone.transform.rotation=GetComponent<MovementController>().GetRotationOfPlayer();
        }
    }
}
