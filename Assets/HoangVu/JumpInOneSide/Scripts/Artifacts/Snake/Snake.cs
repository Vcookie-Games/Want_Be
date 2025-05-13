using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform initialPos;
    [SerializeField] private bool isFacingRight;
    private Vector2 moveDirection;
    private bool isRun;

    public void StartRun(EDirection direction)
    {
        isRun = true;
        gameObject.SetActive(true);
        SetRotation(direction);
        if (direction == EDirection.Down)
        {
            transform.position = new Vector3(initialPos.position.x, GameController.Instance.GetCameraController().GetTopPositionOfScreen().y);
            moveDirection = Vector2.down;
        }
        else if (direction == EDirection.Up)
        {
            transform.position = new Vector3(initialPos.position.x,
                GameController.Instance.GetCameraController().GetBottomPositionOfScreen().y);
            moveDirection = Vector2.up;
        }
        else
        {
            transform.position = initialPos.position;
            if (direction == EDirection.Left)
            {
                moveDirection = Vector2.left;
            }
            else
            {
                moveDirection = Vector2.right;
                
            }
        }
        
    }

    void SetRotation(EDirection direction)
    {
        if (direction == EDirection.Up)
        {
            transform.eulerAngles = new Vector3(0f, 0f, -90f);
        }
        else if (direction == EDirection.Down)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 90f);
        }
        else if (direction == EDirection.Left && isFacingRight)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else if (direction == EDirection.Right && !isFacingRight)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void FixedUpdate()
    {
        if (isRun)
        {
            transform.position += speed * Time.deltaTime * (Vector3)moveDirection;
        }
    }

    public void StopRun()
    {
        isRun = false;
        gameObject.SetActive((false));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.GameOver();
        }
    }
}
