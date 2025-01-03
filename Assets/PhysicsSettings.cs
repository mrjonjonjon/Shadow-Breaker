using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "phys", menuName = "ScriptableObjects/physics", order = 1)]
public class PhysicsSettings: ScriptableObject
{
    public static float gravity=-0.1f;//-0.1
    public static bool friction =true;
    public static float frictionAmount=5f;
    public static float maxzvelup=Mathf.Infinity;
    public static float maxzveldown=0.2f;
    public static float deltatime=1f;
    public static float correctionRatio=0.3f;//0.5. need higher ratio for big stacks
    public static float slop=0.1f;//0.01
    //this should be roughly double the maximum stack size for stability
    public static int num_iterations = 1;//6
    // public static float contactThreshold=
    // public enum EntityType{Player, Box};
    


}
