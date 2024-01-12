using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
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

        if(Input.GetKeyDown(KeyCode.Space)) {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if(xInput != 0 && player.facingDir != xInput)
        {
            stateMachine.ChangeState(player.idleState);
        }

        HandleSpeedWallSide(yInput, rb.velocity.y);

        if (player.isGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    // Check if no enter key down we move slowly and fast if keydow enable
    private void HandleSpeedWallSide(float _yInput, float _y)
    {
        if (_yInput < 0)
            rb.velocity = new Vector2(0, _y);
        else
            rb.velocity = new Vector2(0, _y * .7f);
    }
}
