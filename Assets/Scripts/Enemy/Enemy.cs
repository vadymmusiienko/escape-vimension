using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : Entity
{
    [Header("Basic properties")]
    [SerializeField] public float moveSpeed = 3.5f;
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float attackDamage = 10f;
    [SerializeField] public float attackCooldown = 2f;
    [SerializeField] public float attackRange = 1.5f;
    [SerializeField] public float aggroRange = 10f;

    [Header("Component")]
    public NavMeshAgent agent;
    public HealthbarEnemy healthBar;

    [Header("AI behaviour")]
    public Transform[] patrolPoints;

    public float CurrentHealth {  get; private set; }
    public bool isDead = false;
    public float lastAttackTime;
    
    // Health system events
    public static event Action<Enemy, float, float> OnEnemyHealthChanged; // enemy, currentHealth, maxHealth
    public static event Action<Enemy> OnEnemyDied;

    public Transform playerTarget;

    public EnemyStateMachine stateMachine;
    public EnemyIdleState idleState;
    public EnemyChaseState chaseState;
    public EnemyAttackState attackState;
    public EnemyPatrolState patrolState;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();

        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        chaseState = new EnemyChaseState(this, stateMachine, "Chase");
        attackState = new EnemyAttackState(this, stateMachine, "Attack");
        patrolState = new EnemyPatrolState(this, stateMachine, "Patrol");
    }

    protected override void Start()
    {
        base.Start();
        CurrentHealth = maxHealth;
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerTarget = playerObject.transform;
        }

        if (agent != null)
        {
            agent.speed = moveSpeed;
        }
        
        // Initialize health bar
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(maxHealth, CurrentHealth);
        }
        
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        if (isDead) return;
        stateMachine.currentState.Update();
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDead) return;

        float previousHealth = CurrentHealth;
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
        
        // Update health bar
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(maxHealth, CurrentHealth);
        }
        
        // Trigger health changed event
        OnEnemyHealthChanged?.Invoke(this, CurrentHealth, maxHealth);

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
        {
            anim?.SetTrigger("TakeDamage");
        }
    }
    
    public float GetHealthPercentage()
    {
        return CurrentHealth / maxHealth;
    }

    protected virtual void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} is dead");

        if (agent != null) agent.isStopped = true;
        
        // Trigger death event
        OnEnemyDied?.Invoke(this);

        anim?.SetTrigger("Die");
        Destroy(gameObject, 2f);
    }

    public virtual void Attack()
    {
        if (isDead) return;
    }
}
