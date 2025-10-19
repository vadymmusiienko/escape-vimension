using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;

public class Enemy : Entity
{
    [Header("Basic properties")]
    [SerializeField] public float moveSpeed = 3.5f;
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float attackDamage = 10f;
    [SerializeField] public float attackCooldown = 2f;
    [SerializeField] public float attackRange = 2f;
    [SerializeField] public float aggroRange = 10f;
    [Range(0, 360)] public float attackAngle = 90f;
    [SerializeField] private LayerMask playerLayer;

    [Header("Spoils")]
    [SerializeField] private GameObject itemToDropOnDeath;

    [Header("Component")]
    public NavMeshAgent agent;
    public HealthbarEnemy healthBar;

    [Header("AI behaviour")]
    public Transform[] patrolPoints;

    [Header("Hit effect")]
    [SerializeField] protected GameObject hitEffectPrefab;
    [SerializeField] private float flashDuration = 0.15f;
    private Renderer enemyRenderer;
    private Color originalColor;

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

        enemyRenderer = GetComponentInChildren<Renderer>();
        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }

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

    public virtual void TakeDamage(float damage, Vector3 hitPoint)
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

        TriggerHitEffects(hitPoint);
        
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

    private void TriggerHitEffects(Vector3 hitPoint)
    {
        if (hitEffectPrefab != null)
        {
            Instantiate(hitEffectPrefab, hitPoint, Quaternion.identity);
        }

        if (enemyRenderer != null)
        {
            StartCoroutine(FlashEffectCoroutine());
        }
    }

    private IEnumerator FlashEffectCoroutine()
    {
        enemyRenderer.material.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        enemyRenderer.material.color = originalColor;
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
        DropItem();
        Destroy(gameObject, 2f);
    }

    public virtual void Attack()
    {
        if (isDead) return;

        CheckAttackRange();
    }

    protected virtual void CheckAttackRange()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, playerLayer);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Vector3 directionToPlayer = (hit.transform.position - transform.position).normalized;

                float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

                if (angleToPlayer < attackAngle / 2f)
                {
                    PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(attackDamage);
                        Debug.Log($"Deal {attackDamage} to player");
                    }
                    break;
                }
            }
        }
    }

    private void DropItem()
    {
        if (itemToDropOnDeath != null)
        {
            Instantiate(itemToDropOnDeath, transform.position, Quaternion.identity);
            Debug.Log($"{gameObject.name} drops {itemToDropOnDeath.name}");
        }
    }

    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Vector3 forward = transform.forward;
        Vector3 leftBoundary = Quaternion.Euler(0, -attackAngle / 2, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, attackAngle / 2, 0) * forward;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * attackRange);
        Gizmos.DrawLine(transform.position, transform.position +  rightBoundary * attackRange);
    }
}
