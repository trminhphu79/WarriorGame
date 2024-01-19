using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected EnemySkeleton enemy;
    protected Transform player;
    public SkeletonGroundedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton _enemy) : base(enemy, stateMachine, animBoolName)
    {
        this.enemy = _enemy;
    }


    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        // If player is detected and distance between player and enemy more than 2 => go to battle state
        if(enemy.isPlayerDetected() || Vector2.Distance(enemy.transform.position , player.transform.position) < 2)
            stateMachine.ChangeState(enemy.skeletonBattleState);
    }

}
