using UnityEngine;
using FirstGearGames.SmoothCameraShaker;

public class PoisonLeafObstacle : DeadlyObstacle
{
    [SerializeField] private float timeEffect;
    [SerializeField] private int hitCountEffect;
    [SerializeField] private int hitCountDead;
    [SerializeField] private bool isStunned;
    
    [Header("Setting")]
    [SerializeField] private ShakeData shakeData;
    private int currentHitCount;

    public PoisonLeafObstacle()
    {
        timeEffect = 5f;
        hitCountEffect = 2;
        hitCountDead = 3;
        currentHitCount = 0;
        isStunned = false;
        canDestroy = false;
    }
    public override void Active()
    {
        currentHitCount++;
        if(!isStunned)CameraShakerHandler.Shake(shakeData);
        if (currentHitCount >= hitCountEffect)
        {
            isActive = true;
            playerController.AddSpeed(0);
            playerController.ChangeJumpForce(0.5f);
            isStunned = true;
            StartCoroutine(CountdownTimer(timeEffect, () =>
            {
                DeActive();
            }));
        }
        if (currentHitCount >= hitCountDead && isStunned)
        {
            gameController.GameOver();
            DisPlayer();
        }
    }

    public override void DeActive()
    {
        playerController.ResetSpeed();
        playerController.ResetJumpForce();
        isActive = false;
    }
   

}
