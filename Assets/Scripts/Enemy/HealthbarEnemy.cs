using UnityEngine;
using UnityEngine.UI;

public class HealthbarEnemy : MonoBehaviour
{
    [SerializeField] private Image _healthbarSprite;
    [SerializeField] private float _reduceSpeed = 2;
    private Camera _cam;
    private float _target = 1;

    public void Start()
    {
        _cam = Camera.main;
    }

    public void Update() 
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
        _healthbarSprite.fillAmount = Mathf.MoveTowards(_healthbarSprite.fillAmount, _target, _reduceSpeed * Time.deltaTime);
    }

    
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        if (_healthbarSprite != null)
        {
            _healthbarSprite.fillAmount = currentHealth / maxHealth;
        }
    }
}
