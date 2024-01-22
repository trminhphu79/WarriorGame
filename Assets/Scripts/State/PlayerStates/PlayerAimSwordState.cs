using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.sword.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("MyCoroutine", .2f);
    }

    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();

        if(Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(player.idleState);

        flipPlayer();
    }

    public void flipPlayer()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (player.transform.position.x > mousePos.x && player.facingDir > 0)
        {
            player.Flip();
        }
        else if (player.transform.position.x < mousePos.x && player.facingDir < 0)
        {
            player.Flip();
        }
       
    }                                           
}
