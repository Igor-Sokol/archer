using UnityEngine;

[RequireComponent(typeof(HealthBarView))]
public abstract class Enemy : MonoBehaviour, ILevelObjectConfigure, IHealth, ITakeDamge
{
    private HealthBarView _healthBarView;
    
    protected Health Health;
    
    public Health CurrentHealth => Health;

    public virtual void Configure(LevelObjectConfiguration config)
    {
        if (config is EnemyConfig cfg)
        {
            Health = new Health(cfg.Health);
            _healthBarView = GetComponent<HealthBarView>();
            _healthBarView.CreateHealthBar(this);
            Health.OnDied += Die;
        }
    }
    
    public void TakeDamage(float damage)
    {
        Health.TakeDamage(damage);
    }

    private void Die()
    {
        Health.OnDied -= Die;
        Destroy(gameObject);
    }
}