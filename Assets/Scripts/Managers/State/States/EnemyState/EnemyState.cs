using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : IState
{
    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;
    protected Rigidbody2D rb;
    private string animBoolName;


    protected bool triggerCalled;
    protected float stateTimer;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        enemyBase = _enemyBase;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

   
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        enemyBase.animator.SetBool(animBoolName, true);
        rb = enemyBase.rb;
    }

    public virtual void Exit()
    {
        enemyBase.animator.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
