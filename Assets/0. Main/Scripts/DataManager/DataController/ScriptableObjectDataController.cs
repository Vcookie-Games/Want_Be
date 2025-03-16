using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableObjectDataController<T> : DataController<T> where T : ScriptableObject, new()
{
    [SerializeField] private ScriptableObject scriptableObjectData;

    private void SetData()
    {
        _data = scriptableObjectData as T;
    }

    public override void LoadData()
    {
        SetData();
    }
}
