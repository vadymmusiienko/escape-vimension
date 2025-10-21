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

        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);
        bool hitEnemy = false;
        
        foreach (Collider hit in hits)
        {
            Enemy enemy = hit.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                Vector3 directionToEnemy = (enemy.transform.position - transform.position).normalized;

                enemy.TakeDamage(attackDamage, hit.ClosestPoint(transform.position));
                hitEnemy = true;
            }
        }
        
        // Play hit enemy sound if we actually hit an enemy
        if (hitEnemy && audioController != null)
        {
            audioController.PlayHitEnemySound();
        }
    }
}