using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 10f;
    public float lifetime = 5f;
    public int damage = 10;
    public LayerMask enemyLayer = -1; // Layer mask for enemies
    
    
    private Vector3 direction;
    private float currentLifetime;
    private bool hasHit = false;
    
    public void Initialize(Vector3 dir, float projectileSpeed, float projectileLifetime, int projectileDamage)
    {
        // Ensure direction is not zero
        if (dir.magnitude < 0.001f)
        {
            direction = Vector3.forward; // Default forward direction
        }
        else
        {
            direction = dir.normalized;
        }
        
        speed = projectileSpeed;
        lifetime = projectileLifetime;
        damage = projectileDamage;
        currentLifetime = lifetime;
    }
    
    void Update()
    {
        if (hasHit) return;
        
        // Move projectile with consistent speed
        Vector3 movement = direction * speed * Time.deltaTime;
        transform.position += movement;
        
        // Rotate projectile to face movement direction
        if (direction.magnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
        
        // Check lifetime
        currentLifetime -= Time.deltaTime;
        if (currentLifetime <= 0)
        {
            DestroyProjectile();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        
        // Check if we hit an enemy
        if (IsEnemy(other.gameObject))
        {
            // Deal damage to enemy
            Entity enemy = other.GetComponent<Entity>();
            if (enemy != null)
            {
                // TODO: Add damage system to Entity class
                Debug.Log($"Projectile hit enemy for {damage} damage!");
            }
            
        }
        
        // Destroy projectile on any collision
        DestroyProjectile();
    }
    
    private bool IsEnemy(GameObject obj)
    {
        // Check if the object is on the enemy layer
        return (enemyLayer.value & (1 << obj.layer)) != 0;
    }
    
    private void DestroyProjectile()
    {
        hasHit = true;
        Destroy(gameObject);
    }
}
