using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    // �� ������ �������� ����
    private Vector3 monkeyScale = new Vector3(1f, 1f, 1f);
    private Vector3 snakeScale = new Vector3(2f, 0.1f, 1f);
    private Vector3 fishScale = new Vector3(1f, 0.1f, 1f);

    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    private string currentForm = "Monkey";
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        // Rigidbody2D ȸ�� ��� ����
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        Move();
        HandleTransformation();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");

        if (currentForm == "Monkey" || currentForm == "Snake")
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }

        if (currentForm == "Fish")
        {
            float verticalInput = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(moveInput * moveSpeed, verticalInput * moveSpeed);
        }
    }

    void HandleTransformation()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TransformToMonkey();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TransformToSnake();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TransformToFish();
        }
    }

    private void TransformToMonkey()
    {
        currentForm = "Monkey";
        transform.localScale = monkeyScale; // �������� ������ ����
        capsuleCollider.size = new Vector2(monkeyScale.x, monkeyScale.y); // �������� ũ�⿡ �°� ����
        capsuleCollider.direction = CapsuleDirection2D.Vertical; // ���� ����
        rb.velocity = new Vector2(rb.velocity.x, 0); // �����̷� ���� �� ���� �ӵ� �ʱ�ȭ
        AdjustToGround(); // ���� �� �ٴڿ� �ٵ��� ����
    }

    private void TransformToSnake()
    {
        currentForm = "Snake";
        transform.localScale = snakeScale; // ���� ������ ����
        capsuleCollider.size = new Vector2(snakeScale.x, snakeScale.y); // ���� ũ�⿡ �°� ����
        capsuleCollider.direction = CapsuleDirection2D.Vertical; // ���� ����
        rb.velocity = new Vector2(rb.velocity.x, 0); // ������ ���� �� ���� �ӵ� �ʱ�ȭ
        AdjustToGround(); // ���� �� �ٴڿ� �ٵ��� ����
    }

    private void TransformToFish()
    {
        currentForm = "Fish";
        transform.localScale = fishScale; // ������� ������ ����
        capsuleCollider.size = new Vector2(fishScale.x, fishScale.y); // ������� ũ�⿡ �°� ����
        capsuleCollider.direction = CapsuleDirection2D.Horizontal; // ������ ���� ���� ĸ��
        rb.velocity = Vector2.zero; // ������ ���� �� �ӵ� �ʱ�ȭ
    }

    private void AdjustToGround()
    {
        // �ٴڿ� �پ� �ִ� ��쿡�� ��ġ�� ����
        if (isGrounded)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                // �÷��̾��� ĸ�� �ݶ��̴� ũ�⸸ŭ �ٴ� ���� �̵�
                transform.position = new Vector2(transform.position.x, hit.point.y + capsuleCollider.size.y / 2);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
