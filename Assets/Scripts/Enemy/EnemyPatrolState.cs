using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    private int patrolPointIndex = 0;
    public EnemyPatrolState(Enemy enemy, EnemyStateMachine stateMachine, string animBool) : base(enemy, stateMachine, animBool) { }

    public override void Enter()
    {
        base.Enter();
        enemy.agent?.SetDestination(enemy.patrolPoints[patrolPointIndex].position);
    }

    public override void Update()
    {
        base.Update();

        if (Vector3.Distance(enemy.transform.position, enemy.playerTarget.position) < enemy.aggroRange)
        {
            stateMachine.ChangeState(enemy.chaseState);
            return;
        }

        if (enemy.agent != null && !enemy.agent.pathPending && enemy.agent.remainingDistance < 0.5f)
        {
            patrolPointIndex = (patrolPointIndex + 1) % enemy.patrolPoints.Length;
            enemy.agent.SetDestination(enemy.patrolPoints[patrolPointIndex].position);
        }
    }
}
