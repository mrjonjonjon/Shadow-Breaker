using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    
    public InputState GetInputState()
    {

        //get raw input
         Vector2 inputVector = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
         bool jumpPress = Input.GetKeyDown(KeyCode.Space);
         bool hookPress = Input.GetKeyDown(KeyCode.I);
         bool attackPress = Input.GetKeyDown(KeyCode.P);
         bool dashPress = Input.GetKeyDown(KeyCode.L);
         bool parryPress = Input.GetKeyDown(KeyCode.N);
         bool rollPress = Input.GetKeyDown(KeyCode.M);

        bool superMovePress = Input.GetKeyDown(KeyCode.X);


         bool attackHold = Input.GetKey(KeyCode.P);

         //preprocess input
            if(inputVector.x > 0){
                inputVector.x=1;
               // _spriteRenderer.flipX=false;
            }else if(inputVector.x<0){
                inputVector.x=-1;
               // _spriteRenderer.flipX=true;
            }else{
                //_spriteRenderer.flipX=true;

            }

            if(inputVector.y>0){
                inputVector.y=1;
            }else if(inputVector.y<0){
                inputVector.y=-1;
            }else{
               
            }

            //return input
            return new InputState(inputVector,jumpPress,hookPress,attackPress,dashPress,parryPress,rollPress,superMovePress);
           
    }
}
