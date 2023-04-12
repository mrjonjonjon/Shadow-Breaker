using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComboManager : MonoBehaviour
{

    public int comboCount=0;

    int lastComboCount=0;
     [ColorUsage(true, true)]
    public Color level1,level2,level3,level4;
    public Slider cb1,cb2,cb3;//,cb4;
    Material swordMat;
    public GameObject holySwordVFX;
    // Start is called before the first frame update
    public float timer=0f;
    public float comboResetTime=2f;





Color currentColor;
    public Image sworduiImage;
    void Start()
    {
         swordMat=transform.Find("sprite").GetComponent<SpriteRenderer>().material;
               //  InvokeRepeating("CheckForReset", 0f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        cb1.value=((float)(comboCount))/8f;
        cb2.value=(float)comboCount/16f;
        cb3.value=(float)comboCount/24f;
       
      Check();
       if(timer>comboResetTime){
           comboCount=0;

          
          // cb4.value=0f;
           timer=0f;
             swordMat.SetColor("_SwordColor",level1);
             sworduiImage.color=level1;       

       }
       
       
       
       
       
       
        lastComboCount=comboCount;
    }


    public void Check(){
        if(comboCount==lastComboCount){
            timer+=Time.deltaTime;
        }else{
         UpdateSwordColor();
    timer=0f;

            
        }
    }

    public void IncreaseComboCount(int x){
        comboCount+=x;
        timer=0f;
    }

    public void UpdateSwordColor(){

        if(comboCount>=35 && lastComboCount<35){
            swordMat.SetColor("_SwordColor",level4);
            GameObject go=Instantiate(holySwordVFX,transform.position + new Vector3(0,2,0),holySwordVFX.transform.rotation);
           //go.transform.parent=gameObject.transform;
                        go.GetComponent<SpriteRenderer>().color=level4;
sworduiImage.color=level4;       
            
        }else if(comboCount>=20 && lastComboCount<20){
            swordMat.SetColor("_SwordColor",level3);
            GameObject go=Instantiate(holySwordVFX,transform.position + new Vector3(0,2,0),holySwordVFX.transform.rotation);
           // go.transform.parent=gameObject.transform;
            go.GetComponent<SpriteRenderer>().color=level3;
       
       sworduiImage.color=level3;       

        }else if(comboCount>=8 && lastComboCount<8){
            swordMat.SetColor("_SwordColor",level2);
            GameObject go=Instantiate(holySwordVFX,transform.position + new Vector3(0,2,0),holySwordVFX.transform.rotation);
            //go.transform.parent=gameObject.transform;
                        go.GetComponent<SpriteRenderer>().color=level2;
sworduiImage.color=level2;       

         
        }
    }
}
