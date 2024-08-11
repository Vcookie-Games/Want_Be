using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuanUtilities;

public class DeadZone : MonoBehaviour
{
    [SerializeField] protected float deadzoneSpeed = 5;
    private WantBeManager wantBeManager;
    private Player player;
    private float collisionThreshold = 0.1f;
    void Start()
    {
        wantBeManager = FindObjectOfType<WantBeManager>();
    }
    public void Initialize(Player player)
    {
        this.player = player;
    }

    void Update()
    {
        if (player != null && IsColliding())
        {
            wantBeManager.GameOver();
        }
    }

    private bool IsColliding()
    {
        float playerY = player.transform.position.y;
        float deadzoneY = transform.position.y;

        return playerY <= deadzoneY + collisionThreshold;
    }

    public void MoveUp()
    {
        transform.Translate(Vector3.up * deadzoneSpeed * Time.deltaTime);
    }
}
