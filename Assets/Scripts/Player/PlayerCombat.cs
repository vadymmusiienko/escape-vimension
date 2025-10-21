using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackCooldown = 1.5f;
    [SerializeField] private float attackDamage = 25f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] [Range(0, 360)] private float attackAngle = 90f;
    [SerializeField] private LayerMask enemyLayer;
    
    private float lastAttackTime = 0f;
    private Animator anim;
    private PlayerAudioController audioController;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        audioController = GetComponent<PlayerAudioController>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && CanAttack())
        {
            PerformAttack();
        }
    }
    
    private bool CanAttack()
    {
        return Time.time - lastAttackTime >= attackCooldown;
    }
    
    private void PerformAttack()
    {
        anim.SetTrigger("Attack");
        lastAttackTime = Time.time;
        
        // Play attack swing sound on every attack attempt
        if (audioController != null)
        {
            audioController.PlayAttackSwingSound();
        }

        Debug.Log("--- 玩家发动攻击 ---");

        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        Debug.Log("OverlapSphere 在 " + enemyLayer.ToString() + " 层上找到了 " + hits.Length + " 个碰撞体。");
        bool hitEnemy = false;
        
        foreach (Collider hit in hits)
        {
            Debug.Log("检测到碰撞体：" + hit.gameObject.name);
            Enemy enemy = hit.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("成功在 " + enemy.gameObject.name + " 上找到了 Enemy 脚本！");
                Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;

                //if (Vector3.Angle(transform.forward, directionToEnemy) < attackAngle / 2f)
                //{
                enemy.TakeDamage(attackDamage, hit.ClosestPoint(transform.position));
                hitEnemy = true;
                //}
            }
        }
        
        // Play hit enemy sound if we actually hit an enemy
        if (hitEnemy && audioController != null)
        {
            audioController.PlayHitEnemySound();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Vector3 forward = transform.forward;
        Vector3 leftBoundary = Quaternion.Euler(0, -attackAngle / 2, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, attackAngle / 2, 0) * forward;

        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * attackRange);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * attackRange);
    }
}
