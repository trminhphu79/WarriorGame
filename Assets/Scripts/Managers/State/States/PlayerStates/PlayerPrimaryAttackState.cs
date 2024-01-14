using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 1.5f;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
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
        Debug.Log("Exit Attack");
        player.StartCoroutine("MyCoroutine", .15f);
        UpdateEveryThingBeforeExit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            player.ZeroVelocity();
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
        player.SetVelocity(player.attackMovement[comboCounter].x * GetAttackDir(), player.attackMovement[comboCounter].y);
        stateTimer = .1f;
    }

    private void UpdateEveryThingBeforeExit()
    {
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    private float GetAttackDir()
    {
        float attackDir = player.facingDir;
        if (xInput != 0)
        {
            attackDir = xInput;
        }
        return attackDir;
    }
}
