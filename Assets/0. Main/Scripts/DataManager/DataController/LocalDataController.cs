using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class LocalDataController<TDataModel> : DataController<TDataModel> where TDataModel : class, new()
{
    [SerializeField] protected string _filePath;

    protected string _fullPath;

    public string GetFullPath()
    {
        if (string.IsNullOrEmpty(_fullPath))
        {
            _fullPath = $"{Application.persistentDataPath}/{_filePath}.dat";
        }

        return _fullPath;
    }

    public override void LoadData()
    {
        var filePath = GetFullPath();

        TDataModel result = null;

        if (File.Exists(filePath))
        {
            try
            {
                var savedData = File.ReadAllText(filePath);
                result = JsonUtility.FromJson<TDataModel>(savedData);
                Debug.Log($"LoadData complete {filePath}\n{savedData}".AddColor(Color.green));
            }
            catch (Exception ex)
            {
                Debug.LogError($"Load data -- {filePath} -- is error: {ex.GetBaseException()}\n{ex.StackTrace}".AddColor(Color.red));
            }
        }

        if (result == null)
        {
            result = new TDataModel();
        }

        _data = result;
        
    }


    public void SaveData()
    {
        var filePath = GetFullPath();

        try
        {
            var saveData = JsonUtility.ToJson(_data);
            File.WriteAllText(filePath, saveData);
            
            Debug.Log($"SaveData complete {filePath}\n{saveData}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Save data -- {this.GetType()} -- is error: {ex.GetBaseException()}\n{ex.StackTrace}");
        }
    }

    public void ClearData()
    {
        var filePath = GetFullPath();
        try
        {
            File.Delete(filePath);
            _data = new TDataModel();
            Debug.Log($"Delete Data complete {filePath}\n");
        }
        catch (Exception ex)
        {
            Debug.LogError($"Delete Data -- {this.GetType()} -- is error: {ex.GetBaseException()}\n{ex.StackTrace}");
        }

        
    }

}
