using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float speed = 10f;
    public float lifetime = 5f;
    public int damage = 10;
    public LayerMask enemyLayer = -1; // Layer mask for enemies
    
    [Header("Effects")]
    public GameObject hitEffect;
    public GameObject trailEffect;
    
    private Vector3 direction;
    private float currentLifetime;
    private bool hasHit = false;
    
    public void Initialize(Vector3 dir, float projectileSpeed, float projectileLifetime, int projectileDamage)
    {
        direction = dir.normalized;
        speed = projectileSpeed;
        lifetime = projectileLifetime;
        damage = projectileDamage;
        currentLifetime = lifetime;
        
        // Create trail effect if assigned
        if (trailEffect != null)
        {
            GameObject trail = Instantiate(trailEffect, transform);
            trail.transform.localPosition = Vector3.zero;
        }
    }
    
    void Update()
    {
        if (hasHit) return;
        
        // Move projectile
        transform.position += direction * speed * Time.deltaTime;
        
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
            
            // Create hit effect
            if (hitEffect != null)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 2f);
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
        
        // Create destruction effect
        if (hitEffect != null)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        
        Destroy(gameObject);
    }
}
