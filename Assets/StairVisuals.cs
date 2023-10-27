using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class StairVisuals : MonoBehaviour
{
    
    BoxCollider2D _collider2D;
float width,depth,z_top,z_bot;

Material _stairsMat;

    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {  _collider2D = GetComponent<Stairs>()._collider2D;
        width = GetComponent<Stairs>().width;
        depth = GetComponent<Stairs>().depth;
        z_top = GetComponent<Stairs>().z_top;
        z_bot = GetComponent<Stairs>().z_bot;
        _stairsMat = GetComponent<Stairs>()._stairsMat;
        //visuals and size synch
_collider2D.size = new Vector2(width,depth);
transform.Find("sprite/stairs").localScale = new Vector3(width,depth,1);
transform.Find("sprite").transform.localPosition = new Vector3(0,z_bot,-z_bot);
_stairsMat.SetFloat("_Height",z_top-z_bot);
//_stairsMat.SetFloat("_Depth",depth);
//float angle = 360f*Mathf.Atan2(z_top-z_bot,width)/(3.1415926535f*2f);
//transform.Find("sprite/stairs").transform.rotation = Quaternion.Euler(0,-angle,angle);//deends on slope

    }
}
