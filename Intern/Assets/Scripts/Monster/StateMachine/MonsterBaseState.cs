using UnityEngine;

public class MonsterBaseState : IState // 모든 상태가 들고있는 
{
    protected MonsterStateMachine stateMachine;

    public MonsterBaseState(MonsterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public void HandleInput()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Update()
    {
    }


}
