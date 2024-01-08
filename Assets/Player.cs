using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Move info")]
    [SerializeField]
    private byte moveSpeed = 5;
    [SerializeField]
    private byte jump;
    [SerializeField]
    private float xInput;
    [SerializeField]
    private bool isMoving;

    [Header("Dash info")]
    [SerializeField] private float dashSpeed = 10.5f;
    [SerializeField] private float dashTime = 0.4f;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;
   
    [Header("Attack info")]
    private bool isAttacking;
    private int comboCounter;
    private float counterTimeAttack;
    private float counterTime = 0.5f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Movement();
        CheckInput();
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

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttack();
        }
    }

    private void StartAttack()
    {
        if(!isGrounded) { return;  }

        if (counterTimeAttack < 0)
        {
            comboCounter = 0;
        }
        isAttacking = true;
        counterTimeAttack = counterTime;
    }

    private void Movement()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }

        if(dashTime > 0)
        {
            rb.velocity = new Vector2(facingDir * dashSpeed, 0);
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

    public void AttackOver()
    {
        isAttacking = false;
        comboCounter++;
        if (comboCounter > 2)
        {
            comboCounter = 0;
        }
    }

    private void AnimatorController(Animator animator)
    {
        isMoving = rb.velocity.x != 0;
        
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDashing", dashTime > 0);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetInteger("comboCounter", comboCounter);
    }

  

    private void DashAbility()
    {
        if (dashCooldownTimer < 0 && !isAttacking)
        {
            dashTime = dashDuration;
            dashCooldownTimer = dashCooldown;
        }
    }

    private void ActionTimeController()
    {
    
            dashCooldownTimer -= Time.deltaTime;
            dashTime -= Time.deltaTime;
            counterTimeAttack -= Time.deltaTime;
    }
}
