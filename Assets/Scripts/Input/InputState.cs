using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputState
{
     public InputState(Vector2 iv,bool jp,bool hp,bool ap, bool dp,bool pp,bool rp,bool sm,bool sh)
    {
       inputVector=iv;
       jumpPress=jp;
       hookPress=hp;
       attackPress=ap;
       dashPress=dp;
       parryPress=pp;
       rollPress=rp;
       superMovePress=sm;
       sprintHold=sh;
    }
    public Vector2 inputVector;
    public bool jumpPress;
    public bool hookPress;
    public bool attackPress;
    public bool dashPress;
    public bool parryPress;
    public bool rollPress;
    public bool superMovePress;
    public bool sprintHold;

}
