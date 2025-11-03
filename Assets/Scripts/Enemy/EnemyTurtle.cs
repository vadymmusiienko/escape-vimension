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

        defendState = new EnemyDefendState(this, stateMachine, "Defend");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(patrolState);
    }

    public override void TakeDamage(float damage, Vector3 hitPoint)
    {
        if (isDefending)
        {
            return;
        }

        base.TakeDamage(damage, hitPoint);

        if (hitStarEffectPrefab != null)
        {
            Instantiate(hitStarEffectPrefab, hitPoint, Quaternion.identity);
        }

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
