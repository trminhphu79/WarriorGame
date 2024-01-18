using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    #region State
    public SkeletonIdleState skeletonIdleState { get; private set; }
    public SkeletonMoveState skeletonMoveState { get; private set; }
    public SkeletonBattleState skeletonBattleState { get; private set; }
    public SkeletonAttackState  skeletonAttackState { get; private set; }
    public SkeletonStunState skeletonStunState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        skeletonIdleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        skeletonMoveState = new SkeletonMoveState(this, stateMachine, "Move", this);
        skeletonBattleState = new SkeletonBattleState(this, stateMachine, "Move", this);
        skeletonAttackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        skeletonStunState = new SkeletonStunState(this, stateMachine, "Stun", this);
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(skeletonIdleState);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.U))
            stateMachine.ChangeState(skeletonStunState);
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public override bool CanBeStun()
    {
        if (base.CanBeStun())
        {
            stateMachine.ChangeState(skeletonStunState);
            return true;
        }
        return false;
    }
}
