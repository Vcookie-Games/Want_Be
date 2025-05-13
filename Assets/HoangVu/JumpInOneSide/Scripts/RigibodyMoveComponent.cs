using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigibodyMoveComponent : MonoBehaviour
{
   private Rigidbody2D _rigidbody2D;

   void Awake()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
   }

   public void AddForce(Vector2 direction, float strength)
   {
      _rigidbody2D.AddForce(direction*strength,ForceMode2D.Impulse);
   }
}
