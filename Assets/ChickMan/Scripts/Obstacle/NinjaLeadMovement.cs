using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaLeadMovement : DeadlyObstacle
{
    [SerializeField] private float speed = 2f;
    private float currentSpeed;
    public bool moveRight = true;

    NinjaLeadMovement()
    {
        isActive = false;
        timeDestroy = 5f;
        canDestroy = true;
    }
    protected override void Start()
    {
        base.Start();
        Destroy(gameObject, timeDestroy);
        currentSpeed = speed;
    }
    protected override void Update()
    {
        float direction = moveRight ? 1f : -1f;
        transform.Translate(Vector3.right * direction * currentSpeed * Time.deltaTime);
    }
    public override void Active()
    {

        base.Active();

    }
    public override void DeActive()
    {
        DisPlayer();
        gameController.GameOver();
    }

    //Chưa dùng
    // public void AddSpeed(float Speed)
    // {
    //     currentSpeed *= Speed;
    // }
    // public void ResetSpeed()
    // {
    //     currentSpeed = speed;
    // }


}
