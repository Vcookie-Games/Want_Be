using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerCollision : SirenMonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Obstacle"))
        {
            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlaySFX("GameOver");
            GameManager.isGameOver = true;
        }
    }
}
