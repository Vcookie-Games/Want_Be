using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoJumpCharacter : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;      // Lực nhảy để đạt độ cao mong muốn
    [SerializeField] private float jumpInterval = 0.8f;  // Thời gian giữa các cú nhảy (giây)
    [SerializeField] private float jumpHeight = 3f;      // Độ cao mỗi cú nhảy (units)
    [SerializeField] private float angle = 45f;

    [SerializeField] private float distantJump = 2f; // Khoảng cách nhảy xa (units)
    private float angleRad;

    private Rigidbody2D rb;
    private bool isGrounded = true;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(AutoJump), 0f, jumpInterval);
    }

    void AutoJump()
    {
        if (isGrounded)
        {
            // Tính lực nhảy để đạt đúng độ cao mong muốn
            angleRad = angle * Mathf.Deg2Rad;
            float dir = Input.acceleration.x;
           /* if (Mathf.Abs(dir) > angleRad)
            {
                dir = Input.acceleration.x > 0 ? 1 : -1;
            }
            if (Mathf.Abs(dir) < angleRad)
            {
                dir = 0;
            }*/
            float calculatedJumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * jumpHeight);
            Vector3 jumpDirection = transform.up * Mathf.Cos(dir) + (transform.right * Mathf.Sin(dir));
            rb.velocity = new Vector3(rb.velocity.x, calculatedJumpForce) + jumpDirection * distantJump;
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra va chạm với mặt đất (Ground)
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
