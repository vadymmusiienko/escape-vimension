using UnityEngine;
using UnityEngine.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    [SerializeField] private GameObject[] itemsToDropOnDeath = new GameObject[2];

    [Header("Component")]
    public NavMeshAgent agent;
    public HealthbarEnemy healthBar;

    [Header("AI behaviour")]
    public Transform[] patrolPoints;

    [Header("Hit effect")]
    [SerializeField] protected GameObject hitStarEffectPrefab;
    [SerializeField] private float flashDuration = 0.15f;
    private Renderer enemyRenderer;
    private Color originalColor;

    [Header("Boss Audio")]
    [SerializeField] protected AudioClip attackSoundClip;
    [SerializeField] protected float attackSoundVolume = 0.8f;
    private AudioSource attackAudioSource;

    public float CurrentHealth {  get; private set; }
    public bool isDead = false;
    public float lastAttackTime;
    
    // Events for dialogue triggers
    protected List<EnemyDeathDialogueTrigger> dialogueTriggers = new List<EnemyDeathDialogueTrigger>();
    
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
        
        // Find all EnemyDeathDialogueTrigger components on this GameObject
        dialogueTriggers.AddRange(GetComponents<EnemyDeathDialogueTrigger>());
        
        // Setup audio source for attack sounds
        SetupAttackAudioSource();
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
        if (hitStarEffectPrefab != null)
        {
            Instantiate(hitStarEffectPrefab, hitPoint, Quaternion.identity);
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

        if (agent != null) agent.isStopped = true;
        
        // Always drop item immediately when enemy dies
        DropItem();
        
        // Check if there are any dialogue triggers
        bool hasDialogueTriggers = dialogueTriggers.Count > 0 && dialogueTriggers.Any(t => t != null);
        
        if (hasDialogueTriggers)
        {
            // Make enemy invisible but keep GameObject alive for dialogue
            SetEnemyInvisible();
            
            // Trigger dialogue
            foreach (var trigger in dialogueTriggers)
            {
                if (trigger != null)
                {
                    trigger.TriggerDialogue();
                }
            }
            // Don't destroy - let the dialogue trigger handle it
        }
        else
        {
            // No dialogue triggers, proceed normally
            anim?.SetTrigger("Die");

            // Apply erosion effect
            EnemyErosionController erode = GetComponent<EnemyErosionController>();
            if (erode) erode.TriggerErode();
            
            Destroy(gameObject, 2f);
        }
        
        // Trigger death event
        OnEnemyDied?.Invoke(this);
    }
    
    protected void SetEnemyInvisible()
    {
        /* Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        } */
        
        // Disable all renderers to make enemy invisible via dissolve effect
        EnemyErosionController erode = GetComponent<EnemyErosionController>();
        if (erode)
        {
            erode.TriggerErode();
        }

        
        // Disable colliders so player can't interact with it
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        
        // Disable the agent so it doesn't move
        if (agent != null)
        {
            agent.enabled = false;
        }
        
        // Destroy the healthbar
        if (healthBar != null)
        {
            Destroy(healthBar.gameObject);
        }
    }

    public virtual void Attack()
    {
        if (isDead) return;

        // Play attack sound
        PlayAttackSound();
        
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
                    }
                    break;
                }
            }
        }
    }

    private void DropItem()
    {
        if (itemsToDropOnDeath != null && itemsToDropOnDeath.Length > 0)
        {
            foreach (GameObject item in itemsToDropOnDeath)
            {
                if (item != null)
                {
                    // Add slight random offset to prevent items from overlapping
                    Vector3 randomOffset = new Vector3(
                        UnityEngine.Random.Range(-0.5f, 0.5f),
                        0f,
                        UnityEngine.Random.Range(-0.5f, 0.5f)
                    );
                    Instantiate(item, transform.position + randomOffset, Quaternion.identity);
                }
            }
        }
    }

    private void SetupAttackAudioSource()
    {
        // Create dedicated audio source for boss attack sounds
        attackAudioSource = gameObject.AddComponent<AudioSource>();
        attackAudioSource.clip = attackSoundClip;
        attackAudioSource.loop = false;
        attackAudioSource.volume = attackSoundVolume;
        attackAudioSource.playOnAwake = false;
        attackAudioSource.priority = 16; // Highest priority for boss attack sounds
    }

    protected virtual void PlayAttackSound()
    {
        if (attackSoundClip != null && attackAudioSource != null)
        {
            attackAudioSource.Play();
        }
        else if (attackSoundClip != null && AudioManager.Instance != null)
        {
            // Fallback to AudioManager if dedicated audio source is not available
            AudioManager.Instance.PlaySFX(attackSoundClip);
        }
    }

    // Public methods for external audio control
    public void SetAttackSoundVolume(float volume)
    {
        attackSoundVolume = Mathf.Clamp01(volume);
        if (attackAudioSource != null)
        {
            attackAudioSource.volume = attackSoundVolume;
        }
    }

    public void SetAttackSoundClip(AudioClip newClip)
    {
        attackSoundClip = newClip;
        if (attackAudioSource != null)
        {
            attackAudioSource.clip = attackSoundClip;
        }
    }

    // Item drop management methods
    public void SetDropItem(int index, GameObject item)
    {
        if (itemsToDropOnDeath != null && index >= 0 && index < itemsToDropOnDeath.Length)
        {
            itemsToDropOnDeath[index] = item;
        }
    }

    public GameObject GetDropItem(int index)
    {
        if (itemsToDropOnDeath != null && index >= 0 && index < itemsToDropOnDeath.Length)
        {
            return itemsToDropOnDeath[index];
        }
        return null;
    }

    public int GetDropItemCount()
    {
        if (itemsToDropOnDeath == null) return 0;
        
        int count = 0;
        foreach (GameObject item in itemsToDropOnDeath)
        {
            if (item != null) count++;
        }
        return count;
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
