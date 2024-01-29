using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackHoleState : PlayerState
{
    private float flyTime = 0.4f;
    private bool skillUsed;
    private float defaultGravity;
    public PlayerBlackHoleState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName) : base(_player, _stateMachine, _animeBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        defaultGravity = rb.gravityScale;
        skillUsed = false;
        stateTimer = flyTime;
        rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = defaultGravity;
        player.MakeTransparent(false);
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer >= 0)
            rb.velocity = new Vector2(0, 10);
        
        if(stateTimer < 0){
            rb.velocity = new Vector2(0, -.1f);

            if(!skillUsed)
            {
                if(player.skill.blackHole.CanUseSkill())
                    skillUsed = true;
                Debug.Log("Skill used: " + skillUsed);
            }
        }

        if (player.skill.blackHole.SkillCompleted())
            stateMachine.ChangeState(player.airState);
    }
}
