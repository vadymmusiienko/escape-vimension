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

        boss.lastAttackTime = Time.time;
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
        // Don't call base.Update() to prevent double audio playing
        // The boss attack is handled entirely by RotationalSweepAttack() coroutine
        // Only check if player is out of range to transition back to chase
        if (Vector3.Distance(boss.transform.position, boss.playerTarget.position) > boss.attackRange)
        {
            stateMachine.ChangeState(boss.chaseState);
        }
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
