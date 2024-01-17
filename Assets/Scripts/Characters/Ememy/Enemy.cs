using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Move info")]
    [SerializeField] public float movementSpeed = 2f;
    [SerializeField] public float idleTime = 1f;

    [Header("Attack info")]
    [SerializeField] public float lastTimeAttacked;
    [SerializeField] public float attackCheckDistance;
    [SerializeField] public float attackCooldown;
    [SerializeField] public float battleTime;

    #region Component
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected Transform playerCheck;
    #endregion

    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
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

    public virtual RaycastHit2D isPlayerDetected() => Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, attackCheckDistance, whatIsPlayer);

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, new Vector3(playerCheck.position.x + attackCheckDistance * facingDir, playerCheck.position.y));
    }

    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

}
