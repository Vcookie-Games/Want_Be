using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataController : ScriptableObject
{
   public abstract void LoadData();
   public abstract void InitData();
   public abstract void OnStart();
}

public abstract class DataController<TDataModel> : DataController where TDataModel : class, new()
{
   protected TDataModel _data;

   public override void InitData()
   {
      _data = new TDataModel();
   }
}
