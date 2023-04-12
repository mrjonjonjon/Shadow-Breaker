using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;
using UnityEngine.Events;

public class SuperMoveScript : MonoBehaviour
{
    public float timeBetweenSlashes=0.1f;
    public GameObject specialMoveCam;
    public GameObject vcamtriggers;
    public PlayableDirector director;
    public bool isPlayingSuperMove=false;


public delegate void  StartAction();

public event StartAction onMoveStart;
public void Start(){

  //  onMoveStart +=OnMoveStartFunction;
}


public void OnMoveStart(){
 isPlayingSuperMove=true;
        director.Play();
}

public void OnMoveEnd(){
    isPlayingSuperMove=false;
    director.Stop();
    transform.Find("sprite").localRotation=Quaternion.identity;

}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)){
            DoMove();
        }
    }

    void DoMove(){

       OnMoveStart();
     // isPlayingSuperMove=true;
       // director.Play();
       // specialMoveCam.SetActive(true);
       // vcamtriggers.SetActive(false);
        //transform.Find("Scarf").gameObject.SetActive(true);
        Vector3 temp=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        temp.z=-10;
        //specialMoveCam.transform.position=temp;
                

        //Camera.main.GetComponent<CinemachineBrain>().enabled=false;
        List<Collider2D> results= new List<Collider2D>();
        ContactFilter2D cf=new ContactFilter2D();
        cf.SetLayerMask(LayerMask.GetMask("enemy"));
        //cf.useDepth = true;
        cf.useTriggers = true;
        //cf.SetDepth(0, 10);
        Physics2D.OverlapCircle(transform.position, 500f, cf,results);
        StartCoroutine(chainAttack(results));
        
    }

    IEnumerator chainAttack(List<Collider2D> results){
        print(results.Count);
        foreach(Collider2D col in results){
                transform.position = col.transform.position;
                transform.Find("sprite").localRotation=Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
                // GetComponent<Animator>().SetTrigger("attack");
                GetComponent<Animator>().Play("Base Layer.Attack.attack blend1", 0, 0.37f);
                GetComponent<Animator>().Update(1/60f);
                leaveAfterImage();


                //Destroy(col.gameObject);
                // col.GetComponent<EnemyScript>().TakeDamage(transform.position,col.GetComponent<EnemyScript>().hp);
                //transform.Find("Hitbox").GetComponent<HitBoxScript>().applyHitstop();
                yield return new WaitForSeconds(timeBetweenSlashes);
                
        }

        OnMoveEnd();
                  //director.Stop();
                //Camera.main.GetComponent<CinemachineBrain>().enabled=true;
                        //specialMoveCam.SetActive(false);
                          //      vcamtriggers.SetActive(true);
                           //             transform.Find("Scarf").gameObject.SetActive(false);

                               // transform.rotation=Quaternion.identity;
                                //isPlayingSuperMove=false;



    }

    void leaveAfterImage(){
        GameObject afterImage= new GameObject("afterImage", typeof(SpriteRenderer),typeof(FadeAwayScript));
        afterImage.transform.position=transform.position;
        afterImage.transform.rotation=Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        afterImage.GetComponent<SpriteRenderer>().sprite=transform.Find("sprite").GetComponent<SpriteRenderer>().sprite;
        afterImage.GetComponent<SpriteRenderer>().color=Color.blue;

        afterImage.GetComponent<SpriteRenderer>().sortingLayerName="Player";
        afterImage.GetComponent<FadeAwayScript>().ft=FadeAwayScript.FadeType.time;
    }
}
