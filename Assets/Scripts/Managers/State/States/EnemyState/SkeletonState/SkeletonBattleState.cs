using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    protected EnemySkeleton enemy;
    protected Transform player;
    private int moveDir;
    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("ENTER SkeletonBattleState");
        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        CheckMoveDirection();
        enemy.SetVelocity(enemy.movementSpeed * moveDir, rb.velocity.y);
        CheckAttackPlayer();
        Debug.Log("stateTimer: " + stateTimer);
    }

    protected virtual void CheckMoveDirection()
    {
        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;
    }

    private void CheckAttackPlayer()
    {
        if (enemy.isPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if(enemy.isPlayerDetected().distance < enemy.attackCheckDistance)
                if(CanAttack())
                    enemy.stateMachine.ChangeState(enemy.skeletonAttackState);
        } else
        {
            // If timer is less than 0 or player is too far away, go back to idle state
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
                enemy.stateMachine.ChangeState(enemy.skeletonIdleState);
        }
    }

    private bool CanAttack() { 
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        Debug.Log("Can't attack");
        return false;
     }
}
