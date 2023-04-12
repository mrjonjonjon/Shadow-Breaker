using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{

    public DialogueInfo di;
    public GameObject toTrack;
    int currentIndex=0;
     Vector3 offset;
    // Start is called before the first frame update
    public void Initialize()
    {
        SetText(0);
        offset.x = 5f;
        offset.y = 5f;
        offset.z=0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)){
            AdvanceDialogue();
        }

        if(Vector3.Distance(transform.position,toTrack.transform.position + offset)>0.05f){
            transform.position =  transform.position + (toTrack.transform.position + offset-transform.position)/5f ;
        }
    }

    void SetText(int i){
    
        transform.Find("DialogueTextBox/DialogueTextBoxBG/DialogueText").GetComponent<TextMeshProUGUI>().text=di.dialogue[i];
    }

    public void AdvanceDialogue(){
        if(currentIndex<di.dialogue.Count-1){
            currentIndex++;
            //set text components for new index
            SetText(currentIndex);
        }else{
            MovementController.instance.EnableMovement();
           // transform.Find("DialogueBox").gameObject.SetActive(false);//=true;
            Destroy(gameObject);

        }
        
      


    }

    public void InitiateDialogue( DialogueInfo di){

        //StartCoroutine(PlayDialogue(di));
        //set text components for first dialogue text
       // transform.Find("DialogueBox").gameObject.SetActive(true);//=true;
        //print("acivay");
       // SetText(0);
    }

    void MoveTo(){

    }
}
