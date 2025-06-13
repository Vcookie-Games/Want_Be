using UnityEngine;

public class BlackHole : DeadlyObstacle
{
    [Header("Status")]
    [SerializeField] private float scaleSpeed;
    [SerializeField] private float maxScale;
    [SerializeField] private float pullForce;
    [SerializeField] private float timeActive;

    [Header("Radius")]
    [SerializeField] private RadiusBlackHole radius;


    public BlackHole()
    {
        timeDestroy = 5f;
        scaleSpeed = 2f;
        maxScale = 1f;
        pullForce = 100f;
        timeActive = 1f;
    }
    protected override void Start()
    {
        base.Start();
        radius.Init(pullForce);
        radius.gameObject.SetActive(false);
        Debug.Log("BlackHole Start: gameController is null? " + (gameController == null));
    }
    protected override void Update()
    {
        if (isActive && transform.localScale.x <= maxScale)
        {
            transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
            radius.UpdateScale(transform);
        }
    }
    public override void Active()
    {
        StartCoroutine(CountdownTimer(timeActive, () =>
        {
            radius.gameObject.SetActive(true);
            base.Active();
        }));
    }
    public override void DeActive()
    {
        base.DeActive();
    }

    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isActive)
        {
            DisPlayer();
            gameController.GameOver();
        }
    }

}
