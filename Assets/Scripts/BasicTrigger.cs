using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BasicTrigger : MonoBehaviour
{

    public UnityEvent OnEnter;
    public bool OneTimeUse=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col){

        if(col.CompareTag("Player")){
            OnEnter.Invoke();
            if(OneTimeUse){
                 Destroy(gameObject);
            }
           
        }
    }
}
