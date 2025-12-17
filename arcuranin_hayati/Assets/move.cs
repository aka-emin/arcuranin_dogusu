using UnityEngine;

public class move : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 12f;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.15f;

    private Rigidbody2D rb;
    private Animator anim;

    private float coyoteCounter;
    private float jumpBufferCounter;

    private bool isGrounded;
    private bool wasGrounded;
    private bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    // Yere temas kontrolü
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
            isGrounded = false;
    }

    void Update()
    {
        bool previousGrounded = wasGrounded;

        // Hareket
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        anim.SetBool("run", Mathf.Abs(moveInput) > 0.1f && isGrounded);

        if (moveInput > 0.01f)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (moveInput < -0.01f)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        // Yere inince jump kilidini aç
        if (!previousGrounded && isGrounded)
        {
            isJumping = false;
        }

        // Coyote time
        if (isGrounded)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;

        // Jump buffer
        if (Input.GetButtonDown("Jump"))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        // ✅ TEK SEFERLİK ZIPLAMA (TRIGGER)
        if (jumpBufferCounter > 0 && coyoteCounter > 0 && isGrounded && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            isJumping = true;          // 🔒 kilit
            anim.SetTrigger("jump");  // 🔥 tek sefer

            jumpBufferCounter = 0;
            coyoteCounter = 0;
        }

        wasGrounded = isGrounded;
    }
}
