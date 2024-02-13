using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed, jumpHeight; 
    public float velX,VelY; 
    private Rigidbody2D rb;

    public Transform groundCheck;
    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        flipCharacter();
    }

    private void FixedUpdate()
    {
        Movement();
        jump();
    }

    public void jump()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    public void Movement() {
        velX = Input.GetAxisRaw("Horizontal");
        VelY = rb.velocity.y;

        rb.velocity = new Vector2(velX * speed, VelY);
    }

    public void flipCharacter()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
