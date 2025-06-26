using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace HoangVuCode
{
    public class CameraController : MonoBehaviour
    {
        private Camera cam;
        [SerializeField] private float timeMove;
        [SerializeField] private Ease easeMove;

        
        public float Width => cam.orthographicSize * cam.aspect * 2;
        public float Aspect => cam.aspect;
        public float Height => cam.orthographicSize * 2;
        private void Awake()
        {
            cam = GetComponent<Camera>();
        }

        public void MoveTo(float yPositionBottom)
        {
            transform.DOMoveY(yPositionBottom + cam.orthographicSize, timeMove).SetEase(easeMove).OnComplete(() =>
            {
                GameController.Instance.OnCamFinishMoveToNextScreen();
            });
        }

        public Vector2 GetTopPositionOfScreen()
        {
            return (Vector2)cam.transform.position + Vector2.up * cam.orthographicSize;
        }

        public Vector2 GetBottomPositionOfScreen()
        {
            return (Vector2)cam.transform.position - Vector2.up * cam.orthographicSize;
        }

        public Vector2 GetTriggerMovePosition()
        {
            return (Vector2)cam.transform.position + Vector2.up * cam.orthographicSize * 1f / 3;
        }
        

        public Vector2 GetLeftMiddlePosition()
        {
            return (Vector2)cam.transform.position + Vector2.left * Width / 2;
        }

        public Vector2 GetRightMiddlePosition()
        {
            return (Vector2)cam.transform.position + Vector2.right * Width / 2;
        }
    } 
}

