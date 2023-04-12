using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FoliagePropertyBlock : MonoBehaviour
{
    
    public float velocityStrength,windStrength,windScale;
    public Vector2 windVelocity;
    public Texture2D displacementMask;
 
    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
 
    void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }
 
    void Update()
    {
        // Get the current value of the material properties in the renderer.
        _renderer.GetPropertyBlock(_propBlock);
        // Assign our new value.
        _propBlock.SetFloat("Vector1_e19a6f73e9824493998ba7bebae9c03c", velocityStrength);//veo\loctystrength
        _propBlock.SetVector("Vector2_2ad9dffd23234809bbb6d55338af2214", windVelocity);
        _propBlock.SetFloat("Vector1_2d61041f8dfd46289cb8aafd27290417", windStrength);
        _propBlock.SetFloat("Vector1_620eb0a4fc1a48c79d9daecb584075d4", windScale);
        _propBlock.SetTexture("_DisplacementMask",displacementMask);

        // Apply the edited values to the renderer.
        _renderer.SetPropertyBlock(_propBlock);
    }
}