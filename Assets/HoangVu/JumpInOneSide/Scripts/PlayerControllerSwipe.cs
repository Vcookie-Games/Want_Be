using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class PlayerControllerSwipe : PlayerController
{
   [SerializeField] private float maxTimeSwipe = 1f;
   [SerializeField] private float maxSpeed;
   [SerializeField] private float factor = 0.3f;
   
   private Vector2 initialMouseWorldPos;
   private float currentCountDownTimeSwipe;
   protected override void Start()
   {
    
   }

   protected override void Update()
   {
      if (!GameController.Instance.IsInState(GameController.EGameState.GameLoop)) return;
      if (Input.GetMouseButtonDown(0))
      {
         isMouseDown = true;
         initialMouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         currentCountDownTimeSwipe = 0f;
      }

      if (isMouseDown)
      {
         if (currentCountDownTimeSwipe <= maxTimeSwipe)
         {
            currentCountDownTimeSwipe += Time.deltaTime;
         }
      }

      if (Input.GetMouseButtonUp(0) )
      {
         isMouseDown = false;
         if (currentCountDownTimeSwipe < maxTimeSwipe)
         {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var direction = ((Vector2)mousePos - initialMouseWorldPos);
            var speed = direction.magnitude * factor / currentCountDownTimeSwipe;
            Debug.Log(speed);
            speed = Mathf.Min(speed, maxSpeed);
            direction = direction.normalized;
            Jump(direction, speed);
         }
      }
      
   }

   void Jump(Vector2 direction, float speed)
   {
      
      if (direction.y < 0) direction.y *= -1f;
      rigidbody2D.AddForce(direction * speed, ForceMode2D.Impulse);
      SetState(PlayerState.PlayerJump);
   }

   protected override void FixedUpdate()
   {
      if (!GameController.Instance.IsInState(GameController.EGameState.GameLoop)) return;
      if(IsInState(PlayerState.PlayerJump))
      {
         if (rigidbody2D.velocity.y <= 0)
         {
            SetState(PlayerState.PlayerFall);
         }
      }
      else if (IsInState(PlayerState.PlayerFall))
      {
         if (CheckGround())
         {
            rigidbody2D.velocity = Vector2.zero;
            SetState(PlayerState.PlayerIdle);
            if (currentGroundCheck.collider.transform.position.y < checkGroundPoint.position.y)
            {
               GameController.Instance.OnPlayerTouchGround();
            }
         }
      }
      /*if (isUseConstantGravity && !IsInState(PlayerState.PlayerStop))
      {
         ApplyConstantGravity();
      }
      if (isMouseDown && !isJump)
      {
         HandleUseJetPack();
      }*/
   }
}
