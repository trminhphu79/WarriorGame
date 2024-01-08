using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    [SerializeField]
    private byte moveSpeed = 5;
    [SerializeField]
    private byte jump;
    [SerializeField]
    private float xInput;
    [SerializeField]
    private bool isMoving;

    private int facingDir = 1;
    private bool facingRight = true;

    [Header("Dash info")]
    [SerializeField] private float dashSpeed = 10.5f;
    [SerializeField] private float dashTime = 0.4f;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;



    [Header("Collision Check")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CollisionCheck();
        Movement();
        CheckInput();
        FlipController();
        AnimatorController(animator);
        ActionTimeController();
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer < 0)
        {
            DashAbility();
        }
    }
    private void Movement()
    {
        if(dashTime > 0)
        {
            rb.velocity = new Vector2(xInput * dashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }

    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jump);
    }

    private void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }


    private void AnimatorController(Animator animator)
    {
        isMoving = rb.velocity.x != 0;
        
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDashing", dashTime > 0);
    }

    private void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0); 
    }

    private void FlipController()
    {
        if(rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        } else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void DashAbility()
    {
        if (dashCooldownTimer < 0)
        {
            dashTime = dashDuration;
            dashCooldownTimer = dashCooldown;
        }
    }

    private void ActionTimeController()
    {
    
            dashCooldownTimer -= Time.deltaTime;
            dashTime -= Time.deltaTime;
    }
}
