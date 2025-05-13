using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoangVuCode;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private float speed = 8;
    [SerializeField] private float radius = 3;
    PlayerController playerController;
    CircleCollider2D circleCollider2D;
    void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }
    void Start()
    {
        circleCollider2D.radius = radius;
    }
    void Update()
    {
        
        if (playerController == null) return;
        transform.position = playerController.transform.position;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Coin"))
            {
                hit.transform.position = Vector2.MoveTowards(hit.transform.position, transform.position, speed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
        }

    }
}
