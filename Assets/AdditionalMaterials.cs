using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class AdditionalMaterials : MonoBehaviour
{
    //public List<Material> extraMats= new List<Material>();
    public Material[] newmats=new Material[2];
    // Start is called before the first frame update
    void Start()
    {
      
      if(GetComponent<ParticleSystem>()!=null){
          GetComponent<ParticleSystem>().GetComponent<Renderer>().materials=newmats;
          return;
      }
        GetComponent<Renderer>().materials=newmats;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
