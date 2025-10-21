using UnityEngine;

public class BossChaseState : EnemyChaseState
{
    private BossCursor boss;

    public BossChaseState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.boss = enemy as BossCursor;
    }

    public override void Update()
    {

        if (boss.playerTarget == null)
        {
            stateMachine.ChangeState(boss.idleState);
            return;
        }

        float distance = Vector3.Distance(boss.playerTarget.position, boss.transform.position);

        if (distance < boss.attackRange)
        {
            boss.agent.isStopped = true;

            if (Time.time > boss.lastAttackTime + boss.sweepCooldown)
            {
                stateMachine.ChangeState(boss.attackState);
            }
        }
        else if (distance < boss.aggroRange)
        {
            boss.agent.isStopped = false;
            boss.agent.SetDestination(boss.playerTarget.position);
        }
        else
        {
            stateMachine.ChangeState(boss.idleState);
        }
    }
}
