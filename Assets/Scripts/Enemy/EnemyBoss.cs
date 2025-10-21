using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BossCursor : Enemy
{
    public GameObject attackHitbox;
    public ParticleSystem sweepPrefab;
    [Header("Boss: Attack parameters")]
    public float startAngle = 45f;
    public float endAngle = -45f;
    public float prepareRotationSpeed = 90f;
    public float sweepRotationSpeed = 180f;
    public float sweepCooldown = 3.0f;
    private HashSet<PlayerHealth> playerHit;

    public new BossAttackState attackState;
    public BossChaseState BosschaseState;

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        chaseState = new BossChaseState(this, stateMachine, "Chase");
        attackState = new BossAttackState(this, stateMachine, "Attack");
        patrolState = new EnemyPatrolState(this, stateMachine, "Patrol");
    }

    protected override void Start()
    {
        base.Start();

        if (agent != null)
        {
            agent.enabled = true;
            agent.isStopped = false;
            agent.updatePosition = true;
            agent.updateRotation = true;
            agent.stoppingDistance = 1.5f;
        }

        stateMachine.Initialize(idleState);
        if (attackHitbox != null) { attackHitbox.SetActive(false); }
    }

    protected override void Update()
    {
        if (isDead) return;
        stateMachine.currentState.Update();
    }

    public IEnumerator RotationalSweepAttack()
    {
        if (playerHit == null)
        {
            playerHit = new HashSet<PlayerHealth>();
        }
        playerHit.Clear();

        if (playerTarget == null)
        {
            stateMachine.ChangeState(chaseState);
            yield break;
        }

        Vector3 directionToPlayer = (playerTarget.position - transform.position).normalized;
        directionToPlayer.y = 0;
        Quaternion facePlayerRotation = Quaternion.LookRotation(directionToPlayer);

        if (directionToPlayer.sqrMagnitude < 0.1f * 0.1f)
        {
            facePlayerRotation = transform.rotation;
        }
        else
        {
            facePlayerRotation = Quaternion.LookRotation(directionToPlayer.normalized);
        }

        Quaternion startRotation = facePlayerRotation * Quaternion.Euler(0, startAngle, 0);
        while (Quaternion.Angle(transform.rotation, startRotation) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, prepareRotationSpeed * Time.deltaTime);
            yield return null;
        }

        if (sweepPrefab != null)
        {
            float centerAngle = (startAngle + endAngle) / 2f;

            Quaternion vfxRotation = facePlayerRotation * Quaternion.Euler(0, centerAngle, 0) * Quaternion.Euler(90, 0, 0);

            Vector3 spawnPosition = transform.position + new Vector3(0, 0.1f, 0);
            ParticleSystem vfxInstance = Instantiate(sweepPrefab, spawnPosition, vfxRotation);

            Destroy(vfxInstance.gameObject, vfxInstance.main.duration);
        }

        if (attackHitbox != null) { attackHitbox.SetActive(true); }
        
        PlayAttackSound();

        Quaternion endRotation = facePlayerRotation * Quaternion.Euler(0, endAngle, 0);
        while (Quaternion.Angle(transform.rotation, endRotation) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, endRotation, sweepRotationSpeed * Time.deltaTime);
            yield return null;
        }

        if (attackHitbox != null) { attackHitbox.SetActive(false); }

        while (Quaternion.Angle(transform.rotation, facePlayerRotation) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, facePlayerRotation, prepareRotationSpeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = facePlayerRotation;

        yield return new WaitForSeconds(sweepCooldown);

        stateMachine.ChangeState(chaseState);
    }

    public void DealDamageToPlayer(PlayerHealth player)
    {
        if (!playerHit.Contains(player))
        {
            player.TakeDamage(attackDamage);
            playerHit.Add(player);
            Debug.Log("Boss hit the player and deal " + attackDamage + " damage!");
        }
    }


    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aggroRange);

#if UNITY_EDITOR
        Handles.color = new Color(1, 0, 0, 0.2f);
        Handles.DrawSolidArc(transform.position, Vector3.up, Quaternion.Euler(0, startAngle, 0) * transform.forward, endAngle - startAngle, attackRange);

        Handles.color = Color.red;
        Handles.DrawWireArc(transform.position, Vector3.up, Quaternion.Euler(0, startAngle, 0) * transform.forward, endAngle - startAngle, attackRange);
#endif
    }
}