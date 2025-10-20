using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BossCursor : Enemy
{
    public GameObject attackHitbox;
    [Header("Boss: Attack parameters")]
    public float startAngle = 45f;
    public float endAngle = -45f;
    public float prepareRotationSpeed = 90f;
    public float sweepRotationSpeed = 180f;
    public float sweepCooldown = 2.0f;
    private bool isBusy = false;
    private Quaternion initialRotation;
    private HashSet<PlayerHealth> playerHit;

    protected override void Start()
    {
        base.Start();
        initialRotation = transform.rotation;

        if (agent != null)
        {
            agent.enabled = false;
        }
        if (attackHitbox != null) { attackHitbox.SetActive(false); }
    }

    protected override void Update()
    {
        if (isDead || isBusy) return;

        if (playerTarget != null && Vector3.Distance(transform.position, playerTarget.position) <= aggroRange || Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(RotationalSweepAttack());
        }
    }

    private IEnumerator RotationalSweepAttack()
    {
        isBusy = true;
        if (playerHit == null)
        {
            playerHit = new HashSet<PlayerHealth>();
        }
        playerHit.Clear();

        Quaternion startRotation = initialRotation * Quaternion.Euler(0, startAngle, 0);
        while (Quaternion.Angle(transform.rotation, startRotation) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, startRotation, prepareRotationSpeed * Time.deltaTime);
            yield return null;
        }
        if (attackHitbox != null) { attackHitbox.SetActive(true); }

        Quaternion endRotation = initialRotation * Quaternion.Euler(0, endAngle, 0);
        while (Quaternion.Angle(transform.rotation, endRotation) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, endRotation, sweepRotationSpeed * Time.deltaTime);
            yield return null;
        }

        if (attackHitbox != null) { attackHitbox.SetActive(false); }

        Quaternion initialRot = initialRotation;
        while (Quaternion.Angle(transform.rotation, initialRotation) > 1f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, prepareRotationSpeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = initialRotation;

        yield return new WaitForSeconds(sweepCooldown);

        isBusy = false;
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