using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "phys", menuName = "ScriptableObjects/physics", order = 1)]
public class PhysicsSettings: ScriptableObject
{
    public static float gravity=-0.1f;
    public static bool friction =true;
    public static float frictionAmount=5f;
    public static float maxzvelup=1f;//Mathf.Infinity;
    public static float maxzveldown=0.25f;
    public static float deltatime=1f;
    public static float correctionRatio=0.5f;
    public static float slop=0.01f;
    //this should be roughly double the maximum stack size for stability
    public static int num_iterations = 6;
   // public static float contactThreshold=
       // public enum EntityType{Player, Box};
    


}
