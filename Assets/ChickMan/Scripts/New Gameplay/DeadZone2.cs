using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone2 : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameOverPanel.SetActive(true);
        }
    }

}
