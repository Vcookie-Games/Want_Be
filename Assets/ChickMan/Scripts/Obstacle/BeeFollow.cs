using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class BeeFollow : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float randomMoveRange = 2f;
    [SerializeField] private float randomMoveInterval = 0.2f;

    private float speed = 2f;
    private Vector3 randomOffset;
    private float randomMoveTimer;
    public bool haveCollided { get; private set; }

    void Start()
    {
        haveCollided = false;
        randomMoveTimer = 0f;
        randomOffset = Vector3.zero;
    }

    void Update()
    {
        randomMoveTimer -= Time.deltaTime;
        if (randomMoveTimer <= 0f)
        {
            randomOffset = new Vector3(
                Random.Range(-randomMoveRange, randomMoveRange),
                Random.Range(-randomMoveRange, randomMoveRange),
                0f
            );
            randomMoveTimer = randomMoveInterval;
        }
        transform.position += randomOffset * Time.deltaTime;
    }

    public void FollowPlayer(Transform target)
    {
        if (target == null) return;
        speed = Random.Range(minSpeed, maxSpeed);
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            haveCollided = true;
        }
    }
}
