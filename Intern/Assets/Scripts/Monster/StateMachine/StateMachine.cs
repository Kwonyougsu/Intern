using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//기본적인 상태 
public interface IState
{
    public void Enter();
    public void Exit();
    public void Update();
    public void PhysicsUpdate();
}

public class StateMachine
{
    protected IState currentState;

    public void ChangedState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }
    public void Update()
    {
        currentState?.Update();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}
