using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTScript : MonoBehaviour
{
   
public RenderTexture renderTexture;

public void Start(){
     

        // Create a render texture matching the main camera's current dimensions.
        // Surface the render texture as a global variable, available to all shaders.
        Shader.SetGlobalTexture("_shadowTex", renderTexture);

      
}
      

}
