using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    public override void ChangeState(IState _newState)
    {
        base.ChangeState(_newState);
    }

    public override void Initialize(IState _startState)
    {
        base.Initialize(_startState);
    }
}
