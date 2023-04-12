using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SelfDestruct : MonoBehaviour {
	public float selfdestruct_in = 4; // Setting this to 0 means no selfdestruct.
	public UnityEvent OnSelfDestruct;
	IEnumerator Start () {
		
		if ( selfdestruct_in != 0){ 
					yield return new WaitForSecondsRealtime(selfdestruct_in);
					Time.timeScale=1.0f;
OnSelfDestruct.Invoke();
					Destroy(gameObject);
		}
	}

	
}
