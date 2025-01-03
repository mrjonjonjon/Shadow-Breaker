using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShinyBlockPropertyBlock : MonoBehaviour
{
    private MaterialPropertyBlock propertyBlock;
    public Renderer objectRenderer;

    public Physics phys;

    void Start()
    {
        // Initialize the MaterialPropertyBlock and get the Renderer
        propertyBlock = new MaterialPropertyBlock();
        //objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (objectRenderer == null)
        {
            Debug.LogWarning("No Renderer found on the GameObject.");
            return;
        }
         objectRenderer.GetPropertyBlock(propertyBlock); 

        // Update the _PlayerPos property with the current object's position
        Vector3 position = transform.position;
        propertyBlock.SetVector("_PlayerPos", phys.position);

        // Apply the MaterialPropertyBlock to the Renderer
        objectRenderer.SetPropertyBlock(propertyBlock);
    }
}
