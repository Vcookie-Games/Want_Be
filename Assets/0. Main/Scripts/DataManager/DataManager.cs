using System;
using System.Collections;
using System.Collections.Generic;
using ReuseSystem;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] private DataController[] _controllers;
    private Dictionary<Type, DataController> _typeToControllerMap;
    
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _typeToControllerMap = new Dictionary<Type, DataController>(_controllers.Length);
        foreach (var controller in _controllers)
        {
            _typeToControllerMap[controller.GetType()] = controller;
        }
        LoadData();
    }

    private void Start()
    {
        foreach (var controller in _controllers)
        {
            controller.OnStart();
        }
    }

    void LoadData()
    {
        foreach (var controller in _controllers)
        {
            controller.LoadData();
        }
    }
    
    public T GetDataController<T>() where T : DataController
    {
        try
        {
            return _typeToControllerMap[typeof(T)] as T;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Can't find data controller of type {typeof(T)}: {ex.GetBaseException()}\n{ex.StackTrace}".AddColor(Color.red));
            return null;
        }
    }
}
