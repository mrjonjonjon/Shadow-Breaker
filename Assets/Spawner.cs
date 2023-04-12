using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public BoxCollider2D spawnArea;
    public UnityEvent OnClear;
    public PlayableDirector EndRoom;
    
    public bool started=false;
    public bool done=false;
    public int numRemaining=0;
    // Start is called before the first frame update
    void Start()
    {

       // OnClear.AddListener(PlayEndEncounterTimeline);
    }

    void PlayEndEncounterTimeline(){
        EndRoom.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(started && !done && numRemaining==0){
            print("CLEARED ROOM");
            OnClear.Invoke();
            done=true;
            
        }
    }

    public void Spawn(int numToSpawn){
       
         Vector2 colliderPos = (Vector2)spawnArea.transform.position + spawnArea.offset;

         for(int i=0;i<numToSpawn;i++){
            float randomPosX = Random.Range(colliderPos.x - spawnArea.size.x / 2, colliderPos.x + spawnArea.size.x / 2);
            float randomPosY = Random.Range(colliderPos.y - spawnArea.size.y / 2, colliderPos.y + spawnArea.size.y / 2);
            GameObject obj= Instantiate(objectToSpawn,new Vector3(randomPosX,randomPosY,0),Quaternion.identity);

            if(obj.transform.GetComponent<Damageable>()!=null){
              obj.transform.GetComponent<Damageable>().OnDie.AddListener(DecrementNumRemaining);
              numRemaining++; 
             }
      

        started=true;
         }
        


    }

    public void DecrementNumRemaining(){
        numRemaining-=1;
    }
}
