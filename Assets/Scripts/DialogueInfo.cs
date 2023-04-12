using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueInfo", order = 1)]

public class DialogueInfo : ScriptableObject
{
   public List<string>dialogue;
   public Sprite portrait;
   public string name;
   
}
