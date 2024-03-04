using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float speed;
    Rigidbody2D rb;
    public bool isStatic;
    public bool isWalker;
    public bool walksRight;
    public bool isPatrol;
    Animator anim;
    public Transform wallCheck, pitCheck, groundCheck;
    public bool walldetected, pitDetected, isGround;
    public float detectionRadius;
    public LayerMask whatIsGround;


    public Transform pointA, pointB;
    public bool goToA, goToB;


    // Start is called before the first frame update
    void Start()
    {
        goToA = true;
        speed = GetComponent<Enemy>().speed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isStatic) {
            anim.SetBool("Idle", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (isWalker)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            anim.SetBool("Idle", false);
            if (!walksRight)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            }
        }

        if (isPatrol)
        {
            anim.SetBool("Idle", false);
            if (goToA)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
                if (Vector2.Distance(transform.position, pointA.position) < 0.2f)
                {
                    Flip();
                    goToA = false;
                    goToB = true;
                }
            }

            if (goToB)
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);

                if (Vector2.Distance(transform.position, pointB.position) < 0.2f)
                {
                    Flip();
                    goToA = true;
                    goToB = false;
                }
            }
        }
        if (isPatrol)
        {
            anim.SetBool("Idle", false);
            if (goToA)
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
                if (Vector2.Distance(transform.position, pointA.position) < 0.2f)
                {
                    Flip();
                    goToA = false;
                    goToB = true;
                }
            }

            if (goToB)
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);

                if (Vector2.Distance(transform.position, pointB.position) < 0.2f)
                {
                    Flip();
                    goToA = true;
                    goToB = false;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            pitDetected = !Physics2D.OverlapCircle(pitCheck.position, detectionRadius, whatIsGround);
            walldetected = Physics2D.OverlapCircle(wallCheck.position, detectionRadius, whatIsGround);
            isGround = Physics2D.OverlapCircle(groundCheck.position, detectionRadius, whatIsGround);

            if (pitDetected || walldetected && isGround)
            {
                Flip();
            }
        }

    }
    public void Flip()
    {
        walksRight = !walksRight;
        Vector3 originalScale = transform.localScale;
        transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
    }
}
