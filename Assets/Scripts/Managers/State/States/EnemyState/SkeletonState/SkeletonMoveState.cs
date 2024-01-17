using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    public EnemySkeleton enemy;
    public SkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        enemy.SetVelocity(enemy.movementSpeed * enemy.facingDir, enemy.rb.velocity.y);

        if (enemy.isWallDetected() || !enemy.isGroundDetected())
        {
            enemy.ZeroVelocity();
            enemy.Flip();
            stateMachine.ChangeState(enemy.skeletonIdleState);
        }

    }
}
