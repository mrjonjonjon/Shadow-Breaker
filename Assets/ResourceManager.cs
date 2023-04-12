using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceManager : MonoBehaviour
{
    public Slider HP,Mana,Stamina;
    public float staminaRechargeRate=0.05f;//per second
    public float manaRechargeRate=0.05f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Stamina!=null){
                    Stamina.value+=staminaRechargeRate*Time.deltaTime;

        }
        if(Mana!=null){
                    Mana.value+=manaRechargeRate*Time.deltaTime;

        }
    }

    public void DecreaseStamina(float f){
        if(Stamina!=null){
                    Stamina.value-=f;

        }
    }

    public void DecreaseMana(float f){
        if(Mana!=null){
             Mana.value-=f;
        }
       
    }
}
