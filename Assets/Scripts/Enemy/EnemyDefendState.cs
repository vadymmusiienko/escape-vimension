using UnityEngine;

public class EnemyDefendState : EnemyState
{
    private float defenseTimer;
    private EnemyTurtle turtle;
    public EnemyDefendState(Enemy enemy, EnemyStateMachine stateMachine, string animBool) : base(enemy, stateMachine, animBool) 
    { 
        if (enemy is EnemyTurtle)
        {
            this.turtle = (EnemyTurtle)enemy;
        }
    }

    public override void Enter()
    {
        base.Enter();
        defenseTimer = turtle.defenseDuration;
    }

    public override void Update()
    {
        base.Update();

        defenseTimer -= Time.deltaTime;

        if (defenseTimer <= 0)
        {
            stateMachine.ChangeState(turtle.patrolState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
