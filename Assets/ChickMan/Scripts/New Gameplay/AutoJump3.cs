using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class AutoJump3 : MonoBehaviour
{
    public List<Transform> destinationLeft;
    public List<Transform> destinationRight;
    public List<Transform> destinationMid;
    public CountdownTimer countdownTimer;
    private BoxCollider2D boxCollider;

    Rigidbody2D playerRb;
    public float jumpDuration = 0.7f;
    public float jumpForce = 10f;
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Check(List<Transform> destination)
    {
        if (destination == null || destination.Count == 0)
        {
            Debug.Log("No destination left");
            return;
        }

        // Loại bỏ các điểm có y thấp hơn vị trí hiện tại (điểm đã vượt qua)
        for (int i = destination.Count - 1; i >= 0; i--)
        {
            if (destination[i].position.y < transform.position.y)
            {
                destination.RemoveAt(i);
                Debug.Log($"Removed destination at index: {i}");
            }
        }

        if (destination.Count == 0)
        {
            Debug.Log("No valid destination left after removal");
            return;
        }

        // Tìm điểm gần nhất có y lớn hơn vị trí hiện tại
        Vector2 targetPos = destination[0].position;
        int targetIndex = 0;

        for (int i = 1; i < destination.Count; i++)
        {
            if (destination[i].position.y < targetPos.y)
            {
                targetPos = destination[i].position;
                targetIndex = i;
            }
        }

        float dis = math.abs(transform.position.y - targetPos.y);
        Debug.Log($"Distance: {dis}");

        if (dis > 2f)
        {
            // Nếu khoảng cách quá xa, nhảy tới targetPos và tắt collider
            jumpForce = 1f; // Giảm lực nhảy để tránh nhảy quá cao
            JumpToTargettarget(targetPos);
            Debug.Log("Jump to target position and disable collider");
            boxCollider.enabled = false;
            return;
        }

        // Nếu khoảng cách hợp lý, nhảy tới targetPos và xóa điểm đó khỏi danh sách
        JumpToTargettarget(targetPos);
        destination.RemoveAt(targetIndex);
    }
    public void ButtonLeft()
    {
        Check(destinationLeft);
    }
    public void ButtonRight()
    {
        Check(destinationRight);
    }
    public void ButtonMid()
    {
        Check(destinationMid);
    }

    public void JumpToTargettarget(Vector2 targetPos)
    {
        Vector2 startPos = playerRb.position;

        float t = jumpDuration;
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        float vx = (targetPos.x - startPos.x) / t;
        float vy = (targetPos.y - startPos.y) / t + 0.5f * gravity * t;

        Vector2 velocity = new Vector2(vx, vy) * jumpForce;
        playerRb.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Time"))
        {
           countdownTimer.AddTime(5f); 
        }
    }
}
