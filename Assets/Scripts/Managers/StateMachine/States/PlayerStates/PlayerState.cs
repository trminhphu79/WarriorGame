using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerState : IState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    private string animeBoolName;
    protected float xInput;

    protected float stateTimer;
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
        Debug.Log("Current State Enter: " + animeBoolName);
    }

    public virtual void Update() {
        xInput = Input.GetAxisRaw("Horizontal");
        stateTimer -= Time.deltaTime;
        player.animator.SetFloat("yVelocity", rb.velocity.y);
        Debug.Log("Current State Update: " + animeBoolName);
    }

    public virtual void Exit() {
        player.animator.SetBool(animeBoolName, false);
        Debug.Log("Current State Exit: " + animeBoolName);
    }
}
