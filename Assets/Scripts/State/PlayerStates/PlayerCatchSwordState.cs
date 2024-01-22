using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    private Transform swordTransform;
    public PlayerCatchSwordState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        swordTransform = player.sword.transform;

        FlipAfterCatchSword();
        rb.velocity = new Vector2(player.swordReturnImpact * -player.facingDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("MyCoroutine", .1f);
    }

    public override void Update()
    {
        base.Update();

        if(triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    private void FlipAfterCatchSword()
    {
        if (player.transform.position.x > swordTransform.position.x && player.facingDir > 0)
            player.Flip();
        else if (player.transform.position.x < swordTransform.position.x && player.facingDir < 0)
            player.Flip();
    }
}
