using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

namespace HoangVuCode
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed;
        //Jump section
        [SerializeField] private float jumpForce;
        [SerializeField] private float gravityScale;
        
        [SerializeField] private Transform checkGroundPoint;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private Vector2 size;
        [SerializeField] private float gravity;
        [SerializeField] private float minVelocityY;
        [SerializeField] private bool isUseConstantGravity;
        [SerializeField] private float timeForHoldToUseJetPack;
        [SerializeField] private float jetpackForce;
        [SerializeField] private float maxJetPackForce;
        [SerializeField] private float maxJetPackFuel;
        [SerializeField] private Image fuelStatus;
        [SerializeField] private GameObject smokeTrailGameObject;
        
        private float currentJetPackFuel;
        private bool isJump;
        private bool isMouseDown;
        private float currentTimeToUseJetPack;
        private Rigidbody2D rigidbody2D;
        private RaycastHit2D currentGroundCheck;
        

        public enum PlayerState
        {
            PlayerMoveLeftRight,
            PlayerJump,
            PlayerFall,
            PlayerStop,
            PlayerJetPack
        }
    
        private float currentDirectionX;
        private PlayerState currentState;
        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
    
        void Start()
        {
            currentJetPackFuel = maxJetPackFuel;
            transform.localScale = GameController.Instance.GetAspectCompareToNormalScreen() * Vector3.one;
           SetDirection(1f);
           SetState(PlayerState.PlayerMoveLeftRight);
        }
        private void Update()
        {
            if (!GameController.Instance.IsInState(GameController.EGameState.GameLoop)) return;
            if (Input.GetMouseButtonDown(0))
            {
                isMouseDown = true;
                currentTimeToUseJetPack = timeForHoldToUseJetPack;
                if (IsInState(PlayerState.PlayerMoveLeftRight)
                    )
                {
                    isJump = true;
                    Jump();
                }
                else
                {
                    isJump = false;
                }
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                isMouseDown = false;
            }
            AutoGenerateFuel();
            
        }
        
        private void FixedUpdate()
        {
            if (!GameController.Instance.IsInState(GameController.EGameState.GameLoop)) return;
            if(!IsInState(PlayerState.PlayerStop))
                SetDirection(currentDirectionX);
            if (IsInState(PlayerState.PlayerMoveLeftRight))
            {
                if (!CheckGround())
                {
                    SetState(PlayerState.PlayerFall);
                }
            }
            else if(IsInState(PlayerState.PlayerJump))
            {
                if (rigidbody2D.velocity.y <= 0)
                {
                    SetState(PlayerState.PlayerFall);
                }
            }
            else if (IsInState(PlayerState.PlayerJetPack))
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
                    SetState(PlayerState.PlayerMoveLeftRight);
                    if (currentGroundCheck.collider.transform.position.y < checkGroundPoint.position.y)
                    {
                        GameController.Instance.OnPlayerTouchGround();
                    }
                }
            }
            if (isUseConstantGravity && !IsInState(PlayerState.PlayerStop))
            {
                ApplyConstantGravity();
            }
            if (isMouseDown && !isJump)
            {
                HandleUseJetPack();
            }
        }

        void AutoGenerateFuel()
        {
            if (!IsInState(PlayerState.PlayerJetPack) && !IsInState(PlayerState.PlayerFall))
            {
                if (currentJetPackFuel < maxJetPackFuel)
                {
                    currentJetPackFuel += Time.deltaTime;
                    UpdateFuelStatus();
                }
            }
        }
        void HandleUseJetPack()
        {
            if (IsInState(PlayerState.PlayerMoveLeftRight) || IsInState(PlayerState.PlayerStop)) return;
            if (currentTimeToUseJetPack < 0)
            {
               
                if (currentJetPackFuel > 0)
                {
                    if (!IsInState(PlayerState.PlayerJetPack))
                    {
                        SetState(PlayerState.PlayerJetPack);
                    }
                    UseJetPack();
                }
            }
            else
            {
                currentTimeToUseJetPack -= Time.fixedDeltaTime;
            }
        }

        void UpdateFuelStatus()
        {
            fuelStatus.fillAmount = currentJetPackFuel / maxJetPackFuel;
        }
        void UseJetPack()
        {
            if (rigidbody2D.velocity.y < 0)
            {
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            }
            if(rigidbody2D.velocity.y < maxJetPackForce)
                rigidbody2D.AddForce(Vector2.up * jetpackForce, ForceMode2D.Force);
            currentJetPackFuel -= Time.deltaTime;
            UpdateFuelStatus();
        }

        void ApplyConstantGravity()
        {
            float currentVelocityY = rigidbody2D.velocity.y;
            if ( currentVelocityY > minVelocityY)
            {
                rigidbody2D.velocity =
                    new Vector2(rigidbody2D.velocity.x, currentVelocityY - gravity* Time.deltaTime);
            }
           
        }

        void SetState(PlayerState state)
        {
            currentState = state;
            if (state == PlayerState.PlayerMoveLeftRight)
            {
                SetDirection(currentDirectionX);
            }

            smokeTrailGameObject.SetActive(state == PlayerState.PlayerJetPack);
        }

        bool IsInState(PlayerState state)
        {
            return currentState == state;
        }
        public void StopMovement()
        {
            SetState(PlayerState.PlayerStop);
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.gravityScale = 0f;
        }

        public void RefreshMovement()
        {
            SetState(PlayerState.PlayerMoveLeftRight);
            if (!isUseConstantGravity)
            {
                rigidbody2D.gravityScale = gravityScale;
            }
        }
    
      

        bool CheckGround()
        {
            currentGroundCheck = Physics2D.BoxCast(checkGroundPoint.position, size, 0f,
                Vector2.down, 0f, groundLayerMask);
            return currentGroundCheck.collider is not null;
        }

        void Jump()
        {
            rigidbody2D.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
            SetState(PlayerState.PlayerJump);
        }

        public void AddJumpForce(float force)
        {
            rigidbody2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            SetState(PlayerState.PlayerJump);
        }

        public void AddJumpForceContinuous(float force)
        {
            rigidbody2D.AddForce(Vector2.up*force*Time.deltaTime, ForceMode2D.Impulse);
            if (!IsInState(PlayerState.PlayerJump))
            {
                SetState(PlayerState.PlayerJump);
            }
        }
        
        void SetDirection(float x)
        {
            currentDirectionX = x;
            rigidbody2D.velocity = new Vector2(x*speed, rigidbody2D.velocity.y);
        }
    
        void ReverseMovement()
        {
            //Debug.Log("reverse");
            currentDirectionX = currentDirectionX * -1f;
            SetDirection(currentDirectionX);
        }
    
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                ReverseMovement();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            
            Gizmos.DrawWireCube(checkGroundPoint.position, size );
        }
    }
}

