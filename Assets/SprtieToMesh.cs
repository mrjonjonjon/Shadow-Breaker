using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[ExecuteInEditMode]
public class SprtieToMesh : MonoBehaviour
{

    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshFilter>().mesh=SpriteToMesh(sprite);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Mesh SpriteToMesh(Sprite sprite)
 {
         Mesh mesh = new Mesh();
         mesh.vertices = Array.ConvertAll(sprite.vertices, i => (Vector3)i);
         mesh.uv = sprite.uv;
         mesh.triangles = Array.ConvertAll(sprite.triangles, i => (int)i);
 
         return mesh;
 }
}
