using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using HoangVuCode;
using UnityEngine;

public class Cloud : ChunkArtifact
{
   [SerializeField] private float timeFade;
   [SerializeField] private SpriteRenderer spriteRenderer;
   [SerializeField] private float offset;
   private float height;
   private bool isUserSee;
   private PlayerController player;

   void Start()
   {
      height = GameController.Instance.GetCameraController().Height;
      player = GameController.Instance.GetPlayerController();
   }

   public override void OnInit()
   {
      base.OnInit();
      DOTween.Kill(spriteRenderer);
      isUserSee = false;
      var color = spriteRenderer.color;
      color.a = 1f;
      spriteRenderer.color = color;
   }

   void Update()
   {
      if (!isUserSee)
      {
         CheckUserSee();
         if (isUserSee)
         {
            spriteRenderer.DOFade(0f, timeFade);
         }
         return;
      }
   }
   void CheckUserSee()
   {
      isUserSee = Mathf.Abs(transform.position.y - player.transform.position.y) < height-offset;
   }
}
