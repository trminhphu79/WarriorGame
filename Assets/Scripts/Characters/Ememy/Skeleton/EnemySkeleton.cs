using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    #region State
    public SkeletonIdleState skeletonIdleState { get; private set; }
    public SkeletonMoveState skeletonMoveState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        skeletonIdleState = new SkeletonIdleState(this, stateMachine, "Idle", this);
        skeletonMoveState = new SkeletonMoveState(this, stateMachine, "Move", this);
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
        Debug.Log("SEE PLAYER" + isPlayerDetected());

    }

  
}
