using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.XR;

public class PlayerState : IState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    private string animeBoolName;
    protected float xInput;
    protected float yInput;

    protected float stateTimer;
    protected bool triggerCalled;
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animeBoolName)
    {
        stateMachine = _stateMachine;
        player = _player;
        animeBoolName = _animeBoolName;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animeBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update() {
        xInput = UnityEngine.Input.GetAxisRaw("Horizontal");
        yInput = UnityEngine.Input.GetAxisRaw("Vertical");
        stateTimer -= Time.deltaTime;
        player.animator.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit() {
        player.animator.SetBool(animeBoolName, false);
    }

    public virtual void AnimationFinishTrigger() { 
        Debug.Log("Animation Finish Trigger");
        triggerCalled = true;
    }
  
}
