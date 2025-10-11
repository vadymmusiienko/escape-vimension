using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Settings")]
    public float attackCooldown = 1.5f;
    
    private float lastAttackTime = 0f;
    private Animator anim;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
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
    }
}
