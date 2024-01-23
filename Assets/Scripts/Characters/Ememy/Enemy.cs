using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    public Vector2[] attackMovement;

    [Header("Stun info")]
    [SerializeField] public float stunDuration;
    [SerializeField] public Vector2 stunDirection;
    public bool canStun;
    [SerializeField] public GameObject counterImage;

    [Header("Move info")]
    [SerializeField] public float movementSpeed = 2f;
    [SerializeField] public float idleTime = 1f;

    [Header("Attack info")]
    [SerializeField] public float lastTimeAttacked;
    [SerializeField] public float attackCheckDistance;
    [SerializeField] public float attackCooldown;
    [SerializeField] public float battleTime;

    private float defaultMoveSpeed = 1;
    #region Component
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected Transform playerCheck;
    #endregion

    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        defaultMoveSpeed = movementSpeed;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void FreezeTime(bool _timerFrozen)
    {
        if(_timerFrozen)
        {
            animator.speed = 0;
            movementSpeed = 0;
        }
        else
        {
            movementSpeed = defaultMoveSpeed;
            animator.speed = 1;
        }
    }


    protected virtual IEnumerator FreezeTimerFor(float _duration)
    {
        FreezeTime(true);
        yield return new WaitForSeconds(_duration);
        FreezeTime(false);
    }
    public virtual void OpenCounterAttackWindow()
    {
        canStun = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canStun = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStun()
    {
       if (canStun)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }

    public virtual RaycastHit2D isPlayerDetected() => Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, attackCheckDistance, whatIsPlayer);

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + attackCheckDistance * facingDir, playerCheck.position.y));
    }

    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

}
