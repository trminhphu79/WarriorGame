using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Counter Attack State");
        stateTimer = player.counterAttackDuration;
        player.animator.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        HandleCounterEnemy();
    }

    // If the player is in the counter attack state, check if there is an enemy in the attack check radius
    // player will be in the counter attack state for 10 seconds or until the player has successfully countered an enemy

    private void HandleCounterEnemy()
    {
        player.SetZeroVelocity();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>() != null)
            {
                if (collider.GetComponent<Enemy>().CanBeStun())
                {
                    stateTimer = 10;
                    player.animator.SetBool("SuccessfulCounterAttack", true);
                }
            }
        }

        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
