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
    Animator anim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (isGrounded)
        {
            anim.SetBool("Jump", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }

        flipCharacter();
        Attack();

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
        if (rb.velocity.x != 0) {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);

        }
    }

    public void Attack() {
        if (Input.GetButtonDown("Fire1")) {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }

    }

    public void flipCharacter()
    {
        if (rb.velocity.x > 0.01f) // Si la velocidad es positiva
        {
            transform.localScale = new Vector3(4, 4, 4); // Voltear a la derecha
        }
        else if (rb.velocity.x < -0.01f) // Si la velocidad es negativa
        {
            transform.localScale = new Vector3(-4, 4, 4); // Voltear a la izquierda
        }
    }


}
