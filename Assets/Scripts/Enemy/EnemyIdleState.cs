using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBool): base(enemy, stateMachine, animBool) { }

    public override void Update()
    {
        base.Update();

        if (Vector3.Distance(enemy.transform.position, enemy.playerTarget.position) < enemy.aggroRange)
        {
            stateMachine.ChangeState(enemy.chaseState);
        }
    }
}
