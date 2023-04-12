using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DialogueBoxPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitiateDialogue(DialogueInfo di){
print("pivsmfv");
Vector3 spawnPos= transform.position;
spawnPos.z=0;
        GameObject dialogueBox=Instantiate(DialogueBoxPrefab,spawnPos,Quaternion.identity);
        DialogueController dc = dialogueBox.GetComponent<DialogueController>();
        dc.toTrack=MovementController.instance.transform.gameObject;
        dc.di=di;
        dc.Initialize();
    }
    public void x(){}
}
