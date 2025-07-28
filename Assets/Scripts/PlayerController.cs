using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Rigidbody2D 컴포넌트 Interpolate는 Inspector에서 설정
    }

    void Update()
    {
        // 키보드 입력을 받음 (WASD or 방향키)
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();  // 대각선 이동 속도 보정

        moveVelocity = moveInput * moveSpeed;
    }

    void FixedUpdate()
    {
        // 물리 프레임에 맞춰 속도 적용
        rb.velocity = moveVelocity;
    }
}
