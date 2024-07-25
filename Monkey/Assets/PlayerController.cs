using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    // 각 동물의 스케일을 고정
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

        // Rigidbody2D 회전 잠금 설정
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
        transform.localScale = monkeyScale; // 원숭이의 비율로 변경
        capsuleCollider.size = new Vector2(monkeyScale.x, monkeyScale.y); // 원숭이의 크기에 맞게 조정
        capsuleCollider.direction = CapsuleDirection2D.Vertical; // 방향 설정
        rb.velocity = new Vector2(rb.velocity.x, 0); // 원숭이로 변신 시 수직 속도 초기화
        AdjustToGround(); // 변신 후 바닥에 붙도록 조정
    }

    private void TransformToSnake()
    {
        currentForm = "Snake";
        transform.localScale = snakeScale; // 뱀의 비율로 변경
        capsuleCollider.size = new Vector2(snakeScale.x, snakeScale.y); // 뱀의 크기에 맞게 조정
        capsuleCollider.direction = CapsuleDirection2D.Vertical; // 방향 설정
        rb.velocity = new Vector2(rb.velocity.x, 0); // 뱀으로 변신 시 수직 속도 초기화
        AdjustToGround(); // 변신 후 바닥에 붙도록 조정
    }

    private void TransformToFish()
    {
        currentForm = "Fish";
        transform.localScale = fishScale; // 물고기의 비율로 변경
        capsuleCollider.size = new Vector2(fishScale.x, fishScale.y); // 물고기의 크기에 맞게 조정
        capsuleCollider.direction = CapsuleDirection2D.Horizontal; // 물고기는 수평 방향 캡슐
        rb.velocity = Vector2.zero; // 물고기로 변신 시 속도 초기화
    }

    private void AdjustToGround()
    {
        // 바닥에 붙어 있는 경우에만 위치를 조정
        if (isGrounded)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                // 플레이어의 캡슐 콜라이더 크기만큼 바닥 위로 이동
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
