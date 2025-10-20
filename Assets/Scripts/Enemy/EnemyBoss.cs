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

        if (sweepPrefab != null)
        {
            // 1. 计算斩击弧线的中心角度
            float centerAngle = (startAngle + endAngle) / 2f;

            // 2. 结合Boss的初始朝向，计算出特效最终应该朝向哪个角度
            Quaternion vfxRotation = initialRotation * Quaternion.Euler(0, centerAngle, 0) * Quaternion.Euler(90, 0, 0);

            // 3. 在Boss的轴心点生成特效，并赋予它我们计算好的、固定的旋转
            //    我们稍微抬高一点位置(Y+0.1f)，防止它生成在地面以下
            Vector3 spawnPosition = transform.position + new Vector3(0, 0.1f, 0);
            ParticleSystem vfxInstance = Instantiate(sweepPrefab, spawnPosition, vfxRotation);

            // 4. 播放完毕后销毁
            Destroy(vfxInstance.gameObject, vfxInstance.main.duration);
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