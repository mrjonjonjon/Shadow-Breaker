using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPropertyBlock : MonoBehaviour

{
    [SerializeField] private Renderer rend;

    private MaterialPropertyBlock _propertyBlock;

      [SerializeField] private Renderer rend2;

    private MaterialPropertyBlock _propertyBlock2;


    private void Awake()
    {
        // Make sure to initialize it.
        // Usually it needs to be initialized only once.
        _propertyBlock = new MaterialPropertyBlock(); 
        _propertyBlock2 = new MaterialPropertyBlock(); 
        SetFloat();

    }

    private void Update()
    {
        SetFloat();
    }

    private void SetFloat()
    {
        if(MovementController.instance==null){
            return;
        }
        Physics playerPhysics= MovementController.instance.transform.GetComponent<Physics>();

        rend.GetPropertyBlock(_propertyBlock); 
        // Get previously set values. They will reset otherwise
       // _propertyBlock.SetFloat(colorPropertyName, z);

        _propertyBlock.SetFloat("_depth",GetComponent<Physics>().depth);
        _propertyBlock.SetFloat("_width",GetComponent<Physics>().width);
        _propertyBlock.SetFloat("_height",GetComponent<Physics>().height);
        Vector4 playerPos = new Vector4(playerPhysics.transform.position.x,playerPhysics.transform.position.y,playerPhysics.zpos,0f);
        Vector4 pos = new Vector4(transform.position.x,transform.position.y,GetComponent<Physics>().zpos,0f);

        _propertyBlock.SetVector("_playerPos",playerPos);
        _propertyBlock.SetVector("_pos",pos);

        rend.SetPropertyBlock(_propertyBlock);



        rend2.GetPropertyBlock(_propertyBlock2); // Get previously set values. They will reset otherwise
       // _propertyBlock.SetFloat(colorPropertyName, z);

        _propertyBlock2.SetFloat("_depth",GetComponent<Physics>().depth);
        _propertyBlock2.SetFloat("_width",GetComponent<Physics>().width);
        _propertyBlock2.SetFloat("_height",GetComponent<Physics>().height);
       // Vector4 playerPos = new Vector4(playerPhysics.transform.position.x,playerPhysics.transform.position.y,playerPhysics.zpos,0f);
       // Vector4 pos = new Vector4(transform.position.x,transform.position.y,GetComponent<Physics>().zpos,0f);

        _propertyBlock2.SetVector("_playerPos",playerPos);
        _propertyBlock2.SetVector("_pos",pos);

        rend2.SetPropertyBlock(_propertyBlock2);
       
    }

}
