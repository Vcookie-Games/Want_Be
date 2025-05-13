using System;
using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;
using ReuseSystem;
using Unity.VisualScripting;

public class AimPlayer : ChunkArtifact
{
   [SerializeField] private Vector2 offsetCenterBox;
   [SerializeField] private Vector2 sizeBoxCheckPlayer;
   [SerializeField] private int numberOfBulletAimPlayers;
   [SerializeField] private float radius;
   [SerializeField] private float radiusCheckPlayer;
   [SerializeField] private BulletAimPlayer prefab;
   [SerializeField] private float timeSpawnBullet;
   
   private List<BulletAimPlayer> currentBulletAimPlayers;
   private bool isPlayerInside;
   private PlayerController playerController;
   private float currentTimeSpawnBullet;

   public override void OnInit()
   {
      base.OnInit();
      InitBullets();
      playerController = GameController.Instance.GetPlayerController();
      currentTimeSpawnBullet = timeSpawnBullet;
   }

   void InitBullets()
   {
      if (currentBulletAimPlayers != null)
      {
         foreach (var bullet in currentBulletAimPlayers)
         {
            bullet.OnDespawn();
         }
      }
      currentBulletAimPlayers = new List<BulletAimPlayer>();
      for (int i = 0; i < numberOfBulletAimPlayers; i++)
      {
         CreateNewBullet();
      }
   }

   void CreateNewBullet()
   {
      var bullet = ReuseSystem.SimplePool.Instance.Spawn<BulletAimPlayer>(prefab);
      Vector2 bulletPosition;
      float bulletAngle;
      if (currentBulletAimPlayers.Count == 0)
      {
         bulletPosition = (Vector2)transform.position + Vector2.up * radius;
         bulletAngle = 0f;
         
      }
      else
      {
         float angle = 360f / numberOfBulletAimPlayers;
         var lastBullet = currentBulletAimPlayers[^1];
         float angleB = Mathf.Atan2(lastBullet.Tf.position.y - transform.position.y,
            lastBullet.Tf.position.x - transform.position.x) ;
         float angleNewBullet = angleB + angle*Mathf.Deg2Rad;
         bulletPosition = new Vector2(transform.position.x + radius * Mathf.Cos(angleNewBullet),
            transform.position.y + radius * Mathf.Sin(angleNewBullet));
         bulletAngle = angleNewBullet * Mathf.Rad2Deg - 90f;
      }
      bullet.OnInit(transform.position, bulletPosition, bulletAngle);
      currentBulletAimPlayers.Add(bullet);
   }

   private void Update()
   {
      if (isPlayerInside && currentBulletAimPlayers.Count > 0)
      {
         if (currentTimeSpawnBullet > 0)
         {
            currentTimeSpawnBullet -= Time.deltaTime;
         }
         else
         {
            currentTimeSpawnBullet = timeSpawnBullet;
            var bullet = currentBulletAimPlayers[0];
            currentBulletAimPlayers.RemoveAt(0);
            bullet.Fire(playerController.transform.position);
            
         }
      }
   }

   private void FixedUpdate()
   {
      isPlayerInside = Vector2.Distance(playerController.transform.position, transform.position) < radiusCheckPlayer;
   }

   private void OnDrawGizmosSelected()
   {
      Gizmos.DrawWireSphere(transform.position, radiusCheckPlayer);
   }
}
