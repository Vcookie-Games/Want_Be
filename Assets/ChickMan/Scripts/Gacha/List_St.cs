using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "List_St_available",menuName = "Game Data/List_St_available")]
public class List_St : ScriptableObject
{
   public List<string> idListSta = new List<string>();
   public List<string> idListStb = new List<string>();
    public void AddToSta(string id)
    {
        idListSta.Add(id);
    }
    public void AddToStb(string id)
    {
        idListStb.Add(id);
    }
}
