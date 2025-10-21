using UnityEngine;

public class BossAttackState : EnemyAttackState
{
    private BossCursor boss;

    public BossAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        this.boss = enemy as BossCursor;
    }

    public override void Enter()
    {
        base.Enter();

        if (boss.agent != null)
        {
            boss.agent.isStopped = true;
            boss.agent.updatePosition = false;
            boss.agent.updateRotation = false;
        }

        boss.StartCoroutine(boss.RotationalSweepAttack());
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();

        if (boss.agent != null && boss.agent.enabled)
        {
            boss.agent.isStopped = false;
            boss.agent.updatePosition = true;
            boss.agent.updateRotation = true;
        }
    }
}
