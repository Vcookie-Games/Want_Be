using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using Unity.Mathematics;
using UnityEngine;

public class BulletAimPlayer : GameUnit
{
    [SerializeField] private float speedRotate;
    [SerializeField] private float speedChase;
    [SerializeField] private float maxTimeToDespawn;
    private bool isMoveToPlayer;
    private Vector2 direction;
    private Vector2 centerPos;
    private float currentTimeToDespawn;
    
    public void OnInit(Vector2 centerPos, Vector2 bulletPosition,float angle)
    {
        isMoveToPlayer = false;
        this.centerPos = centerPos;
        Tf.position = bulletPosition;
        Tf.rotation = Quaternion.Euler(0,0,angle);
    }

    public void Fire(Vector2 target)
    {
        direction = (target - (Vector2)Tf.position).normalized;
        isMoveToPlayer = true;
        currentTimeToDespawn = maxTimeToDespawn;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;
        Tf.rotation = Quaternion.Euler(0f,0f, angle);
    }

    public void OnDespawn()
    {
        Despawn();
    }
    void FixedUpdate()
    {
        if (!GameController.Instance.IsInState(GameController.EGameState.GameLoop)) return;
        if (!isMoveToPlayer)
        {
            Tf.RotateAround(centerPos, Vector3.forward, speedRotate *Time.deltaTime );
        }
        else
        {
            Tf.position += (Vector3)direction * (speedChase * Time.deltaTime);
            if (currentTimeToDespawn > 0)
            {
                currentTimeToDespawn -= Time.deltaTime;
            }
            else
            {
                Despawn();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.Instance.GameOver();
        }
    }
}
