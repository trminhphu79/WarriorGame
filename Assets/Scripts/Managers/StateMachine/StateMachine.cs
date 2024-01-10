using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : IStateMachine
{
    public IState currentState {  get; private set; }
 
    public void Initialize(IState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }
`
    public void ChangeState(IState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
