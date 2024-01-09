using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
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

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            Debug.Log("dashState: ");
            stateMachine.ChangeState(player.dashState);
        }


        if (Input.GetKeyDown(KeyCode.Space) && player.isGroundDetected())
        {
            Debug.Log("JUMP");
            stateMachine.ChangeState(player.jumpState);
        }
    }
}
