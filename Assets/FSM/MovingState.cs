using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HFSM;
public class MovingState : StateMachine
{
    protected override void OnEnter() {
        Debug.Log("Enter MovingState");
    }
    
    protected override void OnUpdate() {
        Debug.Log("Updating MovingState");
    }
    
    protected override void OnExit() {
        Debug.Log("Exit MovingState");
    }

     protected override void HandleTransition() {
        Debug.Log("Exit MovingState");
    }

}

public class IdleState : StateMachine
{
    protected override void OnEnter() {
        Debug.Log("Enter IdleState");
    }
    
    protected override void OnUpdate() {
        Debug.Log("Updating IdleState");
    }
    
    protected override void OnExit() {
        Debug.Log("Exit IdleState");
    }

     protected override void HandleTransition() {
        Debug.Log("Exit IdleState");
    }

}

public class GroundedState : StateMachine
{
    protected override void OnEnter() {
        Debug.Log("Enter Grounded");
    }
    
    protected override void OnUpdate() {
        Debug.Log("Updating Grounded");
    }
    
    protected override void OnExit() {
        Debug.Log("Exit Grounded");
    }

     protected override void HandleTransition() {
        Debug.Log("Exit Grounded");
    }

}
