using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBool) : base(enemy, stateMachine, animBool) { }

    public override void Update()
    {
        base.Update();

        if (Time.time >= enemy.lastAttackTime + enemy.attackCooldown)
        {
            enemy.Attack();
            enemy.lastAttackTime = Time.time;
        }
        if (Vector3.Distance(enemy.transform.position, enemy.playerTarget.position) > enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.chaseState);
        }
    }
}
