using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoJumpAndMove : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpInterval = 0.8f;  // Thời gian giữa các cú nhảy (giây)
    [SerializeField] private float jumpHeight = 3f;      // Độ cao mỗi cú nhảy (units)
    [SerializeField] private float angle = 45f;

    private float dirMove = 1f; // 1 = phải, -1 = trái, 0 = nhảy thẳng lên
    private float dir = 0f;
    private bool isJumping = false;

    private Rigidbody2D rb;
    private bool isGrounded = true;
    float leftLimit = 1.6f;
    float rightLimit =  -1.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {

        if (!isJumping)
        {
            rb.velocity = new Vector2(dirMove * moveSpeed, rb.velocity.y);

        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }

        if (isGrounded && !isJumping)
        {
            if (dirMove > 0 && transform.position.x >= rightLimit && transform.position.x <= -0.5f)
                dirMove *= -1f;
            else if (dirMove < 0 && transform.position.x <= leftLimit&& transform.position.x >= 0.5f)
                dirMove *= -1f;
        }
    }

    void AutoJump()
    {
        if (isGrounded)
        {
            // Tính lực nhảy để đạt đúng độ cao mong muốn
            float calculatedJumpForce = Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y) * jumpHeight);

            float jumpAngleDeg = 90f; // Default: nhảy lên thẳng

            if (dir != 0)
            {
                // Nếu dir = 1 (phải): góc = 90 - angle
                // Nếu dir = -1 (trái): góc = 90 + angle
                jumpAngleDeg = 90f - dir * angle;
            }
            float jumpAngleRad = jumpAngleDeg * Mathf.Deg2Rad;

            // Tạo vector hướng theo góc (từ phương ngang)
            Vector2 jumpDir = new Vector2(Mathf.Cos(jumpAngleRad), Mathf.Sin(jumpAngleRad)).normalized;

            // Gán vận tốc Y mới (X giữ nguyên ở FixedUpdate)
            rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vận tốc Y trước khi nhảy
            rb.velocity += jumpDir * calculatedJumpForce;
            isJumping = true;
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Chạm đất thì nhảy tiếp
        if (collision.contacts[0].normal.y > 0.5f)
        {
            if (isJumping)
            {
                // Nếu đang nhảy, reset trạng thái
                isJumping = false;
            }
            isGrounded = true;
        }
        // Chạm tường thì đổi hướng (tag hoặc Layer tường là "Wall")
        if (collision.gameObject.CompareTag("Wall"))
        {
            dirMove *= -1f; // Đổi hướng
            // Nếu cần flipX thì lật sprite ở đây
        }
    }

    // Nếu bạn muốn điều khiển hướng ngoài tự động:
    public void JumpRight()
    {
        dir = 1;
        AutoJump();
    }
    public void JumpLeft()
    {
        dir = -1;
        AutoJump();
    }
    public void ButtonExit()
    {
        dir = 0;
    }
}
