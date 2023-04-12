using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SceneController : MonoBehaviour
{
    public Animator SceneTransition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadAScene(string scenename){

        StartCoroutine(LoadSceneCoroutine(scenename));
     
               // SceneManager.LoadScene(scenename);

    }

    IEnumerator LoadSceneCoroutine(string scenename){
 SceneTransition.SetTrigger("exit");
yield return new WaitForSecondsRealtime(1f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenename);

          
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
                   SceneTransition.SetTrigger("enter");

    }
}
