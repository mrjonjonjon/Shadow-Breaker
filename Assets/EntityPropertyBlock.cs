using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]

public class EntityPropertyBlock : MonoBehaviour

{
    [SerializeField] public Renderer rend;

    public MaterialPropertyBlock _propertyBlock;

      [SerializeField] public Renderer rend2;

    public MaterialPropertyBlock _propertyBlock2;


    private void Awake()
    {
        // Make sure to initialize it.
        // Usually it needs to be initialized only once.
        _propertyBlock = new MaterialPropertyBlock(); 
        _propertyBlock2 = new MaterialPropertyBlock(); 
       // SetFloat();

    }


       // #if UNITY_EDITOR
        void Update()
        {
            //if (EditorApplication.isPlayingOrWillChangePlaymode)
            //    return;
            //Awake();
            SetFloat();
        }
        //#endif
      
    

    private void SetFloat()
    {
        //rend=bottom, rend2=top
        //if(MovementController.instance==null){
        //    return;
        //}
        //Physics playerPhysics= MovementController.instance.transform.GetComponent<Physics>();

        rend.GetPropertyBlock(_propertyBlock); 
        // Get previously set values. They will reset otherwise
   
        //Vector4 playerPos = new Vector4(playerPhysics.transform.position.x,playerPhysics.transform.position.y,playerPhysics.zpos,0f);
       // Vector4 pos = new Vector4(transform.position.x,transform.position.y,GetComponent<Physics>().zpos,0f);
        _propertyBlock.SetFloat("_Height",GetComponent<Physics>().height);
        _propertyBlock.SetFloat("_Flat",0f);
         _propertyBlock.SetFloat("_Bottom",transform.Find("sprite/bottom").position.y - GetComponent<Physics>().height/2f);

        rend.SetPropertyBlock(_propertyBlock);



        rend2.GetPropertyBlock(_propertyBlock2); // Get previously set values. They will reset otherwise

         _propertyBlock2.SetFloat("_Flat",1f);

        rend2.SetPropertyBlock(_propertyBlock2);
       
    }

}
