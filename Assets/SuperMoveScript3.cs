using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMoveScript3 : MonoBehaviour
{
int index=0;
    public GameObject enemy;
    public GameObject shadowClone;
    MovementController mc;
    public int numDashes;
    public float timeBetweenDashes=0.1f;
    public float radius=1f;
    
    // Start is called before the first frame update
    void Start()
    {
        mc=GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            //StartCoroutine(ConsecDash());
            SingleDash();
        }
    }

    public IEnumerator ConsecDash(){

       float angle = 2f * Mathf.PI /numDashes;
        Vector2 playerToEnemy=(Vector2)(enemy.transform.position-transform.position).normalized;
        //  mc.StartCoroutine(mc.Dash(playerToEnemy));
        for(int i=0;i<numDashes;i++){  
         
          
             playerToEnemy=(Vector2)(enemy.transform.position-transform.position).normalized;
            Vector2 toAdd= new Vector2(Mathf.Cos(angle*i),Mathf.Sin(angle*i)); 
             GameObject shadow=Instantiate(shadowClone,(Vector2)enemy.transform.position+ toAdd*radius,shadowClone.transform.rotation);
             Vector2 cloneToEnemy= ((Vector2)enemy.transform.position - (Vector2)shadow.transform.position).normalized;
             shadow.GetComponent<DashScript>().StartCoroutine( shadow.GetComponent<DashScript>().Dash(cloneToEnemy));
           
                yield return new WaitUntil(() => !mc.isDashing);
                yield return new WaitForSecondsRealtime(timeBetweenDashes);

        }
       



    }

    public void SingleDash(){
        GetComponent<ResourceManager>().DecreaseMana(0.05f);
         float angle = 2f * Mathf.PI /numDashes;
           Vector2 playerToEnemy=(Vector2)(enemy.transform.position-transform.position).normalized;
            Vector2 toAdd= new Vector2(Mathf.Cos(angle*index),Mathf.Sin(angle*index)); 
             GameObject shadow=Instantiate(shadowClone,(Vector2)enemy.transform.position+ toAdd*radius,shadowClone.transform.rotation);
             Vector2 cloneToEnemy= ((Vector2)enemy.transform.position - (Vector2)shadow.transform.position).normalized;
             shadow.GetComponent<DashScript>().StartCoroutine( shadow.GetComponent<DashScript>().Dash(cloneToEnemy));
             index=(index+1)%numDashes;


    }
}
