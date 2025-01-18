using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
//몬스터의 상태머신 
public class MonsterStateMachine : StateMachine
{
    public string CurrentState => currentState?.GetType().Name;
    public Monster Monster { get; }
    public MoveState moveState { get; }
    public AttackingState attackingState { get; }


    public MonsterStateMachine(Monster monster)
    {
        this.Monster = monster;
        moveState = new MoveState(this);
        attackingState = new AttackingState(this);
    }
}