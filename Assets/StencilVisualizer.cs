using UnityEngine;

[ExecuteInEditMode]
public class StencilVisualizer : MonoBehaviour
{
    public Material stencilMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (stencilMaterial != null)
        {
            Graphics.Blit(src, dest, stencilMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
