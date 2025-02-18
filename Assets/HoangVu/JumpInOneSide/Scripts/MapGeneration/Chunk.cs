using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : GameUnit
{
   [SerializeField] private Transform topPosition;
   [SerializeField] private List<ChunkArtifact> artifacts;
   public void Init(Vector2 pos, Vector3 scale)
   {
      Tf.localScale = scale;
      Tf.position = pos;
      foreach (var artifact in artifacts)
      {
         artifact.OnInit();
      }
   }

   public Vector2 GetTopPosition()
   {
      return topPosition.position;
   }
}
