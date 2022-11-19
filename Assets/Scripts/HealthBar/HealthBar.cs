using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    private Slider _slider;
    private Transform _target;
    private Camera _camera;
    private Health _health;
    private Vector3 _positionOffset;
    
    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _camera = Camera.main;
    }

    public void Init(Transform target, IHealth health, Vector3 positionOffset)
    {
        _target = target;
        _health = health.CurrentHealth;
        _positionOffset = positionOffset;
        SetMaxHealth(_health.CurrentHealth);

        if (enabled)
        {
            _health.OnDamaged += TakeDamage;
            _health.OnDied += Die;
        }
    }

    private void OnEnable()
    {
        if (_health != null)
        {
            _health.OnDamaged += TakeDamage;
            _health.OnDied += Die;
        }
    }

    private void OnDisable()
    {
        if (_health != null)
        {
            _health.OnDamaged -= TakeDamage;
            _health.OnDied -= Die;
        }
    }

    private void Update()
    {
        transform.position = _camera.WorldToScreenPoint(_target.position + _positionOffset);
    }

    private void TakeDamage(float damage)
    {
        _slider.value -= damage;
    }

    private void SetMaxHealth(float maxHealth)
    {
        _slider.maxValue = maxHealth;
        _slider.value = maxHealth;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
