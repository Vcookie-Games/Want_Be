using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : GameUnit
{
   [SerializeField] private Transform topPosition;
   public void Init(Vector2 pos)
   {
      transform.position = pos;
   }

   public Vector2 GetTopPosition()
   {
      return topPosition.position;
   }
}
