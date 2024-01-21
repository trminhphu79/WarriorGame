using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 1.5f;
    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ResetCombo();
    }

    public override void Exit()
    {
        base.Exit();
        UpdateEveryThingBeforeExit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            rb.velocity = new Vector2(0, 0);
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    private void ResetCombo()
    {
        if (comboCounter > 2 || Time.time > lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }

        player.animator.SetInteger("ComboCounter", comboCounter);
        stateTimer = .1f;
    }

    private void UpdateEveryThingBeforeExit()
    {
        comboCounter++;
        lastTimeAttacked = Time.time;
    }
}
