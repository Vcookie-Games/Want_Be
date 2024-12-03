using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class MoveSpike : ChunkArtifact
{
   private bool isTouch;
   private Vector2 direction;
   private bool isMove;
   private float currentTimeMove;
   [SerializeField] private float speed;
   [SerializeField] private float timeWaitForMove;
   [SerializeField] private float timeMove = 5f;
   public override void OnInit()
   {
      base.OnInit();
      isTouch = false;
      isMove = false;
   }

   void FixedUpdate()
   {
      if (isMove && GameController.Instance.IsInState(GameController.EGameState.GameLoop))
      {
         transform.position +=  (Vector3)direction * (speed * Time.deltaTime);
         if (currentTimeMove < 0)
         {
            gameObject.SetActive(false);
         }
         else
         {
            currentTimeMove -= Time.deltaTime;
         }
      }
   }
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (isTouch) return;
      if (other.CompareTag("Player"))
      {
         Move();
      }
   }

   void Move()
   {
      isTouch = true;
      direction = transform.up;
      Invoke(nameof(StartMove), timeWaitForMove);
   }

   void StartMove()
   {
      isMove = true;
      currentTimeMove = timeMove;
   }
}
