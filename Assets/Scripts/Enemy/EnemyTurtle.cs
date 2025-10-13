using UnityEngine;

public class EnemyTurtle : Enemy
{
    [Header("Turtle properties")]
    public float defenseDuration = 3f;
    public bool isDefending { get; private set; }

    public EnemyDefendState defendState;

    protected override void Awake()
    {
        base.Awake();

        defendState = new EnemyDefendState(this, stateMachine, "defend");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(patrolState);
    }

    public override void TakeDamage(float damage)
    {
        if (isDefending)
        {
            Debug.Log("invalid attack, defending!!!!");
            return;
        }

        base.TakeDamage(damage);

        if (!isDead)
        {
            stateMachine.ChangeState(defendState);
        }
    }

    public override void Attack()
    {
        base.Attack();
    }
}
