using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    public SkillManager skill;
    #region States
    public PlayerStateMachine stateMachine {  get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }
    public PlayerAimSwordState aimSword { get; private set; }
    public PlayerCatchSwordState catchSword { get; private set; }
    public PlayerBlackHoleState blackHoleState { get; private set; }

    #endregion
    [Header("Attack info")]
    public Vector2[] attackMovement;
    public float counterAttackDuration { get; set; } = .2f;
    
    public float moveSpeed { get; private set; } = 8f;
    public float jumpForce { get; private set; } = 12;
    public float swordReturnImpact { get; set; } = 5;

    public float dashDuration { get; private set; } = .6f;
    public float dashSpeed { get; private set; } = 14;
    public float dashDir { get; private set; }
    public bool isBusy { get; private set; }

    public GameObject sword { get; private set; }


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

        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

        aimSword = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSword = new PlayerCatchSwordState(this, stateMachine, "CatchSword");

        blackHoleState = new PlayerBlackHoleState(this, stateMachine, "Jump");

    }

    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;

        stateMachine.Initialize(idleState);
        InputEventHandler inputEventHandler = GetComponent<InputEventHandler>();
        inputEventHandler.SpaceEvent += On_SpaceEvent;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckInputChangeState();

        if(Input.GetKeyDown(KeyCode.F))
        {
            skill.crystalSkill.UseSkill();
        }
    }
    
    private void CheckInputChangeState()
    {

        if (isWallDetected())
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {
            dashDir = Input.GetAxisRaw("Horizontal");
            if(dashDir != 0)
                stateMachine.ChangeState(dashState);
        }
       
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public void AssigneNewSword(GameObject _sword)
    {
        sword = _sword;
    }

    public void CatchTheSword()
    {
        stateMachine.ChangeState(catchSword);
        Destroy(sword);
    }
    IEnumerator MyCoroutine(float _time)
    {
        isBusy = true;
        yield return new WaitForSeconds(_time);
        isBusy = false;
    }

    public void On_SpaceEvent(object sender, InputEventHandler.SpaceEventArgs e)
    {
    }
}
