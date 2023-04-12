using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
 [ExecuteInEditMode]
 public class SetSortingLayer : MonoBehaviour {
     public Renderer MyRenderer;
     public string MySortingLayer;
     public int MySortingOrderInLayer;
     [ColorUsageAttribute(true,true,0.6f,0.35f,0.35f,0f)]
     public Color color;
     
     // Use this for initialization
     void Start () {
         if (MyRenderer == null) {
             MyRenderer = this.GetComponent<Renderer>();
         }
             
 
         SetLayer();
         SetColor();
 
   
    }

    void Update(){
         SetLayer();
         SetColor();
    }
     public void SetLayer() {
         if (MyRenderer == null) {
             MyRenderer = this.GetComponent<Renderer>();
         }
             
         MyRenderer.sortingLayerName = MySortingLayer;
         MyRenderer.sortingOrder = MySortingOrderInLayer;
         
         //Debug.Log(MyRenderer.sortingLayerName + " " + MyRenderer.sortingOrder);
     }
   public void SetColor(){
         if (MyRenderer == null) {
             MyRenderer = this.GetComponent<Renderer>();
         }
             
         MyRenderer.material.color = color;
       
         
         //Debug.Log(MyRenderer.sortingLayerName + " " + MyRenderer.sortingOrder);
     }
 }