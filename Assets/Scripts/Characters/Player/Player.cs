using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region States
    public PlayerStateMachine stateMachine {  get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    #endregion

    #region Properties
    public float moveSpeed { get; private set; } = 7f;
    public float jumpForce { get; private set; } = 12;
    public float facingDir { get; private set; } = 1;
    public bool facingRight { get; private set; }  = true;

    public float dashDuration { get; private set; } = .6f;
    public float dashSpeed { get; private set; } = 14;
    public float dashDir { get; private set; }
    public float dashCooldown { get; private set; } = 2;
    public float dashTimer { get; private set; }

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = .8f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = .7f;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;

    #endregion

    #region Component
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        stateMachine.currentState.Update();
        CheckInputChangeState();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(-1);

    }

    private void CheckInputChangeState()
    {
        dashTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0)
        {
            dashTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if(dashDir != 0)
                stateMachine.ChangeState(dashState);
        }
       
    }

    public bool isGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    // public bool isGroundDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance * facingDir, whatIsGround)
    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y + groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if(rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        } else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

}
