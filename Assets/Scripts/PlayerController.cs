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
        // Rigidbody2D ������Ʈ Interpolate�� Inspector���� ����
    }

    void Update()
    {
        // Ű���� �Է��� ���� (WASD or ����Ű)
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();  // �밢�� �̵� �ӵ� ����

        moveVelocity = moveInput * moveSpeed;
    }

    void FixedUpdate()
    {
        // ���� �����ӿ� ���� �ӵ� ����
        rb.velocity = moveVelocity;
    }
}
