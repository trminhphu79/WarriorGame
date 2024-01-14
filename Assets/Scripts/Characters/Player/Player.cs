using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    #region States
    public PlayerStateMachine stateMachine {  get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState playerPrimaryAttack { get; private set; }
    #endregion

    #region Properties
    public Vector2[] attackMovement;
    public bool isBusy { get; private set; }
    public float moveSpeed { get; private set; } = 8f;
    public float jumpForce { get; private set; } = 12;

    public float dashDuration { get; private set; } = .6f;
    public float dashSpeed { get; private set; } = 14;
    public float dashDir { get; private set; }
    public float dashCooldown { get; private set; } = 2;
    public float dashTimer { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        playerPrimaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    }

    protected override void Start()
    {
       base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckInputChangeState();
    }
    
    private void CheckInputChangeState()
    {

        if (isWallDetected())
            return;

        dashTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0)
        {
            dashTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if(dashDir != 0)
                stateMachine.ChangeState(dashState);
        }
       
    }

   
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    IEnumerator MyCoroutine(float _time)
    {
        isBusy = true;
        yield return new WaitForSeconds(_time);
        isBusy = false;
    }
 }
