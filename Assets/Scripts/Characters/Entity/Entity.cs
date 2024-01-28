using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Entity : MonoBehaviour
{
    
    [Header("Attack info")]
    public Transform attackCheck;
    public float attackCheckRadius;

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance = .8f;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance = .11f;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDir;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    #region Component
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX entityFX { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }

    #endregion

    [SerializeField] public float facingDir { get; private set; } = 1;
    public bool facingRight { get; private set; } = true;

    protected virtual void Awake()
    {
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        entityFX = GetComponent<EntityFX>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void Damage()
    {
        entityFX.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback", knockbackDuration);
    }

    protected IEnumerator HitKnockback(float knockbackTime)
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDir.x * -facingDir, knockbackDir.y);
        yield return new WaitForSeconds(knockbackTime);
        rb.velocity = Vector2.zero;
        isKnocked = false;
    }

    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
            spriteRenderer.color = Color.clear;
        else
            spriteRenderer.color = Color.white;
    }

    #region Collision
    public virtual bool isGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y + groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    #endregion

    #region Velocity
    public void SetZeroVelocity()
    {
        if (isKnocked)
            return;

        rb.velocity = Vector2.zero;
    }
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if(isKnocked)
            return;

        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(-1);

    }
    #endregion

    #region Flip
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public virtual void FlipController(float _x)
    {
        if (rb.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion
}
