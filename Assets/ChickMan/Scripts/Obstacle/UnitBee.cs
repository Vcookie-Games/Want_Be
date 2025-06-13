using System.Collections;
using UnityEngine;

public class UnitBee : TrapObstacle
{
    private SpriteRenderer spriteRenderer;

    
    [SerializeField] private float moveDistance = 2f;    
    [SerializeField] private float moveSpeed = 2f;       
    [SerializeField] private float verticalAmplitude = 0.3f;
    [SerializeField] private float verticalFrequency = 2f;   

    private Vector3 startPosition;
    private float direction = 1f; 

    UnitBee()
    {
        maxHitCount = 0;
        timeStun = 5f;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        direction = Random.value > 0.5f ? 1f : -1f; 
    }

    void Update()
    {
        NaturalMove();
    }

    // Hàm di chuyển tự nhiên
    private void NaturalMove()
    {
        float offsetX = Mathf.PingPong(Time.time * moveSpeed, moveDistance) - moveDistance / 2f;
        offsetX *= direction;

        float offsetY = Mathf.Sin(Time.time * verticalFrequency) * verticalAmplitude;

        transform.position = startPosition + new Vector3(offsetX, offsetY, 0f);
    }

    public override void Active()
    {
        base.Active();
        moveSpeed = 0;
        spriteRenderer.enabled = false;
    }

    public override void DeActive()
    {
        base.DeActive();
        Destroy(this.gameObject);
    }
}
