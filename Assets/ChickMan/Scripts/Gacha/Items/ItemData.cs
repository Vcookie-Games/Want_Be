using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "New item")]
[System.Serializable]
public class ItemData : ScriptableObject
{
    public string nameItem;
    public string description;
    public int Appeared = 0;
    public int maxAppeared;
    public string id;
    [Range(0f, 100f)] public float rateDrop;
    
    
}
