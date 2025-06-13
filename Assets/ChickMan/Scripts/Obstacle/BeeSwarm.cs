using System.Collections;
using HoangVuCode;
using UnityEngine;

public class BeeSwarm : DeadlyObstacle
{
    [SerializeField] private float timeActive = 10f;
    [SerializeField] private float timeDelay = 0.5f;
    [SerializeField] private Transform pointPlayer;

    public BeeFollow[] beeFollows;

    private Coroutine updatePointCoroutine;

    protected override void Start()
    {
        base.Start();
        beeFollows = GetComponentsInChildren<BeeFollow>();
    }

    protected override void Update()
    {
        base.Update();
        if (!isActive) return;

        foreach (var bee in beeFollows)
        {
            bee.FollowPlayer(pointPlayer);
            if (bee.haveCollided)
            {
                DeActive();
                gameController.GameOver();
                DisPlayer();
                break;
            }
        }
    }

    public override void Active()
    {
        isActive = true;
        if (updatePointCoroutine != null) StopCoroutine(updatePointCoroutine);
        updatePointCoroutine = StartCoroutine(UpdatePointPlayerRoutine());
        StartCoroutine(CountdownTimer(timeActive, DeActive));
    }

    private IEnumerator UpdatePointPlayerRoutine()
    {
        while (isActive)
        {
            if (playerController != null && pointPlayer != null)
            {
                pointPlayer.position = Vector3.Lerp(
                    pointPlayer.position,
                    playerController.transform.position,
                    0.5f
                );
            }
            yield return new WaitForSeconds(timeDelay);
        }
    }

    public override void DeActive()
    {
        isActive = false;
        if (updatePointCoroutine != null) StopCoroutine(updatePointCoroutine);
        base.DeActive();
    }

    public void SetPlayerController(PlayerController controller)
    {
        if (controller == null) return;
        playerController = controller;
        if (pointPlayer == null)
        {
            var pointObj = new GameObject("PointPlayer");
            pointPlayer = pointObj.transform;
            pointPlayer.position = playerController.transform.position;
        }
    }
}
