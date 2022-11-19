using System;

public class Health
{
    private float _health;
    private bool _isAlive;

    public float CurrentHealth => _health;
    public bool IsAlive => _isAlive;
    public event Action<float> OnDamaged = delegate {  };
    public event Action<float> OnHealed = delegate {  };
    public event Action OnDied = delegate {  }; 

    public Health(float health)
    {
        _health = health;
        _isAlive = true;
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
        {
            return;
        }
        
        _health -= damage;
        OnDamaged?.Invoke(damage);
        
        if (_health <= 0)
        {
            Die();
        }
    }

    public void Heal(float health)
    {
        if (health < 0)
        {
            return;
        }

        _health += health;
        OnHealed?.Invoke(health);
    }

    private void Die()
    {
        _isAlive = false;
        OnDied?.Invoke();
    }
}