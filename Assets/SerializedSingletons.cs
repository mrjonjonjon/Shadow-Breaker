using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializedSingletons : MonoBehaviour
{
    public static SerializedSingletons instance;
    public AnimationCurve brightnessCurve;

    void Awake(){
        if(instance==null){
            instance=this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
