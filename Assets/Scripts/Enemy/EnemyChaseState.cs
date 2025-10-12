using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, string animBool) : base(enemy, stateMachine, animBool) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Chasing!!!");
        // May play some intense music here.
    }

    public override void Update()
    {
        base.Update();

        if (enemy.playerTarget == null)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.playerTarget.position);

        if (distanceToPlayer <= enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            enemy.agent?.SetDestination(enemy.playerTarget.position);
        }
    }
}
