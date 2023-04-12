using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VcamScript : MonoBehaviour
{

    public CinemachineVirtualCamera vcam; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  void nTriggerEnter2D(Collider2D other){
     // if(GetComponent<Collider2D>().bounds.Contains(other.bounds.min) && GetComponent<Collider2D>().bounds.Contains(other.bounds.max) && other.CompareTag("Player")){
      if(Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera!=null){
                   Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);

      }
            vcam.gameObject.SetActive(true);
     //   }
  }
    void nTriggerExit2D(Collider2D other){
      //  if(GetComponent<Collider2D>().bounds.Contains(other.bounds.min) && GetComponent<Collider2D>().bounds.Contains(other.bounds.max) && other.CompareTag("Player")){
           //Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);
            vcam.gameObject.SetActive(false);
     //   }
    }

    void OnTriggerStay2D(Collider2D other){
 
    if (GetComponent<Collider2D>().bounds.Contains(other.bounds.min) && GetComponent<Collider2D>().bounds.Contains(other.bounds.max) && other.CompareTag("Player"))
    {
      //  print("x");

      if(Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera!=null){
                Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.SetActive(false);

      }
        vcam.gameObject.SetActive(true);
    }
}
}
