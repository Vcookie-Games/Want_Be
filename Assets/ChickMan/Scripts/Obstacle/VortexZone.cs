using UnityEngine;

public class VortexZone : DeadlyObstacle
{

    [Header("Status")]
    [SerializeField] private float scaleSpeed;
    [SerializeField] private float pullForce;
    [SerializeField] private float timeActive;
    [SerializeField] private float minScale;

    private bool isBeingPulled;

    private SpriteRenderer spriteRenderer;

    public VortexZone()
    {
        timeDestroy = 5f;
        pullForce = 100f;
        timeActive = 1f;
        minScale = 0.2f;
        isBeingPulled = false;
        canDestroy = false;
    }
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Update()
    {
        if (isBeingPulled)
        {
            playerMovement.transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (playerMovement.transform.localScale.x <= minScale)
            {
                DisPlayer();
                gameController.GameOver();
            }
        }
    }

    public override void Active()
    {
        StartCoroutine(CountdownTimer(timeActive, () =>
        {
            spriteRenderer.color = Color.blue;
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
            playerMovement.transform.position = Vector2.MoveTowards(playerMovement.transform.position, transform.position, pullForce * Time.fixedDeltaTime);
            isBeingPulled = true;

        }

    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !IObstacle.isPlayerProtect)
        {
            isBeingPulled = false;
            playerMovement.transform.localScale = Vector3.one;
        }
    }
}
