using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class DarkArea : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float offsetY;
    void Start()
    {
        transform.position = GameController.Instance.GetCameraController().GetBottomPositionOfScreen() - Vector2.up*offsetY;
    }

    private void FixedUpdate()
    {
        if(GameController.Instance.IsInState(GameController.EGameState.GameLoop))
            transform.position += Vector3.up * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.GameOver();
        }
    }
}
