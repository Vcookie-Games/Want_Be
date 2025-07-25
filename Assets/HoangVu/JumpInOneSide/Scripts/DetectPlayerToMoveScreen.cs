using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HoangVuCode
{
    public class DetectPlayerToMoveScreen : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameController.Instance.SetPlayerAboveScreen(false);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (other.transform.position.y < transform.position.y)
                {
                    Debug.Log("set false");
                    GameController.Instance.SetPlayerAboveScreen(false);
                }
                else
                {
                    Debug.Log("set true");
                    GameController.Instance.SetPlayerAboveScreen(true);
                }
            }
        }
    }
}

