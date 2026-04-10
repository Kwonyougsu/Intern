using UnityEngine;

public enum MonsterState { Move, Attacking }

public class MonsterStateMachine
{
    private Monster monster;
    private MonsterState? currentState = null;

    public MonsterStateMachine(Monster monster)
    {
        this.monster = monster;
    }

    public void ChangeState(MonsterState newState)
    {
        if (currentState.HasValue)
            Exit(currentState.Value);
        currentState = newState;
        Enter(currentState.Value);
    }

    private void Enter(MonsterState state)
    {
        switch (state)
        {
            case MonsterState.Move:
                monster.animator.SetBool("Move", true);
                break;
            case MonsterState.Attacking:
                break;
        }
    }

    public void Update()
    {
        if (!currentState.HasValue) return;
        switch (currentState.Value)
        {
            case MonsterState.Move:
                Vector3 targetPosition = new Vector3(
                    monster.transform.position.x - 1f,
                    monster.transform.position.y,
                    monster.transform.position.z
                );
                monster.transform.position = Vector3.MoveTowards(
                    monster.transform.position,
                    targetPosition,
                    monster.speed * Time.deltaTime);
                break;
            case MonsterState.Attacking:
                break;
        }
    }

    private void Exit(MonsterState state)
    {
        switch (state)
        {
            case MonsterState.Move:
                monster.animator.SetBool("Move", false);
                break;
            case MonsterState.Attacking:
                break;
        }
    }
}
