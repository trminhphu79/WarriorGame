using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
        Debug.Log("Dash" + player.transform);
        player.skill.CreateClone(player.transform);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();  
        if (xInput == player.facingDir && player.isWallDetected())
        {
            stateMachine.ChangeState(player.idleState);
            return;
        }
        player.SetVelocity(player.dashSpeed * player.dashDir, 0); 
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
