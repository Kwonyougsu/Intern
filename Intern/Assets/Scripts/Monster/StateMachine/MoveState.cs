using UnityEngine;

public class MoveState : MonsterBaseState
{
    public MoveState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        stateMachine.Monster.animator.SetBool("Move", true);
    }

    public override void Update()
    {
        //Vector3 playerPosition = ;
        //stateMachine.Monster.transform.position = Vector3.MoveTowards(stateMachine.Monster.transform.position, Vector3.left, stateMachine.Monster.Speed * Time.deltaTime);

        // X축으로만 이동, Y축과 Z축은 고정
        Vector3 targetPosition = new Vector3(
            stateMachine.Monster.transform.position.x - 1f, // X축으로 이동
            stateMachine.Monster.transform.position.y,     // Y값 고정
            stateMachine.Monster.transform.position.z      // Z값 고정
        );

        stateMachine.Monster.transform.position = Vector3.MoveTowards(
            stateMachine.Monster.transform.position,
            targetPosition,
            stateMachine.Monster.Speed * Time.deltaTime);
    }

    public override void Exit()
    {
        stateMachine.Monster.animator.SetBool("Move", false);
    }

}