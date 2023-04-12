using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputState
{
     public InputState(Vector2 iv,bool jp,bool hp,bool ap, bool dp,bool pp,bool rp,bool sm)
    {
       inputVector=iv;
       jumpPress=jp;
       hookPress=hp;
       attackPress=ap;
       dashPress=dp;
       parryPress=pp;
       rollPress=rp;
       superMovePress=sm;
    }
    public Vector2 inputVector;
    public bool jumpPress;
    public bool hookPress;
    public bool attackPress;
    public bool dashPress;
    public bool parryPress;
    public bool rollPress;
    public bool superMovePress;

}
