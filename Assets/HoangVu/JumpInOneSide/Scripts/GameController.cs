using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using ReuseSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HoangVuCode
{
    public class GameController : Singleton<GameController>
    {

        private bool isPlayerAboveScreen;
        [SerializeField] private DetectPlayerToMoveScreen detectPlayerToMoveScreen;
        [Header("Left Right Collider")]
        [SerializeField] private Transform leftColliderTransform;
        [SerializeField] private Transform rightColliderTransform;
        [SerializeField] private float offsetLeftRightCollider;
        
        [Header("Dead Zone")]
        [SerializeField] private Transform deadZoneTransform;
        [SerializeField] private float offsetDeadZone;
        
        [Header("Player")]
        [SerializeField] private PlayerController player;
        
        [Header("Camera")]
        [SerializeField] private CameraController cameraController;
        [SerializeField] private float offsetToCam;
        
        [SerializeField] private Transform moveTriggerTransform;

        
        //only for test
        [Header("Testing")]
        [SerializeField] private GameObject gameOverPanel;

        [SerializeField] private bool isMoveCameraImmediately;
        [SerializeField] private float offsetMoveCamImmediately;

        #region Actions

        public Action<int> OnCoinChange;
        

        #endregion
        private int currentCoin;
        private bool IsUsingCamMoveImmediately
        {
            get => PlayerPrefs.GetInt("IsCamMoveImmediately", 0) == 1;
            set => PlayerPrefs.SetInt("IsCamMoveImmediately", value ? 1 : 0);
        }
        private EGameState currentGameState;
        private float highestPlayerY;
        public enum EGameState
        {
            GameLoop,
            MoveCam,
            GameOver
        }

        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            
           OnInit();
           SetState(EGameState.GameLoop);
        }

        void OnInit()
        {
            currentCoin = 0;
            isMoveCameraImmediately = IsUsingCamMoveImmediately;
            highestPlayerY = player.transform.position.y;
            ChunkGeneration.Instance.InitGenerateChunk(cameraController.GetBottomPositionOfScreen());
            moveTriggerTransform.position = cameraController.GetTriggerMovePosition();
            UpdateBounderPosition();
            UpdateDeadZonePosition();
        }

        public void SetState(EGameState state)
        {
            this.currentGameState = state;
        }

        public void CheckPlayerAboveScreen()
        {
            if (player.transform.position.y > detectPlayerToMoveScreen.transform.position.y)
            {
                SetPlayerAboveScreen(true);
            }
            else
            {
                SetPlayerAboveScreen(false);
            }
        }
        public bool IsInState(EGameState state)
        {
            return currentGameState == state;
        }

        public float GetAspectCompareToNormalScreen()
        {
            return cameraController.Aspect / 0.5625f;
        }

        public PlayerController GetPlayerController()
        {
            return player;
        }

        public CameraController GetCameraController()
        {
            return cameraController;
        }
        public void SetPlayerAboveScreen(bool value)
        {
            isPlayerAboveScreen = value;
        }

        public void OnPlayerTouchGround()
        {
            if (isMoveCameraImmediately)
            {
                MoveCamImmediately();
            }
            else
            {
                highestPlayerY = Math.Max(highestPlayerY, player.transform.position.y);
                ChunkGeneration.Instance.CheckUpdateNextChunk(highestPlayerY);
                MoveCamAbove();
            }
        }

        public void AddCoin()
        {
            currentCoin += 1;
            OnCoinChange?.Invoke(currentCoin);
        }

        void MoveCamImmediately()
        {
            if (player.transform.position.y > highestPlayerY)
            {
                highestPlayerY = player.transform.position.y;
                cameraController.MoveTo(player.transform.position.y - offsetMoveCamImmediately);
            }
        }

        void MoveCamAbove()
        {
            
            if (isPlayerAboveScreen)
            {
                SetState(EGameState.MoveCam);
                isPlayerAboveScreen = false;
                player.StopMovement();
                cameraController.MoveTo(player.transform.position.y - offsetToCam);
            }
        }

        void UpdateAfterCamReachAbove()
        {
            SetState(EGameState.GameLoop);
            player.RefreshMovement();
            //ChunkGeneration.Instance.CheckUpdateNextChunk(highestPlayerY);
            moveTriggerTransform.position = cameraController.GetTriggerMovePosition();
            UpdateBounderPosition();
            UpdateDeadZonePosition();
        }

        void UpdateAfterCamReachImmediately()
        {
            ChunkGeneration.Instance.CheckUpdateNextChunk(highestPlayerY);
            moveTriggerTransform.position = cameraController.GetTriggerMovePosition();
            UpdateBounderPosition();
            UpdateDeadZonePosition();
        }

        void UpdateDeadZonePosition()
        {
            deadZoneTransform.position = cameraController.GetBottomPositionOfScreen() - Vector2.up * offsetDeadZone;
        }
        void UpdateBounderPosition()
        {
            leftColliderTransform.position =
                cameraController.GetLeftMiddlePosition() + Vector2.left * offsetLeftRightCollider;
            rightColliderTransform.position =
                cameraController.GetRightMiddlePosition() + Vector2.right * offsetLeftRightCollider;
        }

        public void OnCamFinishMoveToNextScreen()
        {
            if (isMoveCameraImmediately)
            {
                UpdateAfterCamReachImmediately();
            }
            else
            {
                UpdateAfterCamReachAbove();
            }
        }

        public void GameOver()
        {
            SetState(EGameState.GameOver);
            gameOverPanel.SetActive(true);
            player.StopMovement();
        }
        

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ResetCamMoveImmediately()
        {
            IsUsingCamMoveImmediately = true;
            ReloadScene();
        }

        public void ResetCamMoveNormal()
        {
            IsUsingCamMoveImmediately = false;
            ReloadScene();
        }
    }
}

