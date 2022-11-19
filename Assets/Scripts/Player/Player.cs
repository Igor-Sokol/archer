using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(HealthBarView))]
public class Player : MonoBehaviour, IHealth, ITakeDamge, IHeal
{
    private Health _health;
    private float _playerHeight;
    private HealthBarView _healthBarView;

    [SerializeField] Gun gun;
    [SerializeField] private float startHealth;
    [SerializeField] private float shootingRadius;

    public Health CurrentHealth => _health;
    
    private void Awake()
    {
        _playerHeight = GetComponent<CharacterController>().height;
        _health = new Health(startHealth);

        _healthBarView = GetComponent<HealthBarView>();
        _healthBarView.CreateHealthBar(this);
    }

    private void Update()
    {
        if (gun.IsReadyShoot && TryFindEnemy(out Enemy enemy))
        {
            gun.Shoot(enemy.transform.position);
        }
    }

    private bool TryFindEnemy(out Enemy enemy)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, shootingRadius, LayerMask.GetMask("Enemy"));
        
        if (colliders.Length > 0)
        {
            colliders = colliders.OrderBy(c => (c.transform.position - transform.position).sqrMagnitude).ToArray();

            foreach (var collider in colliders)
            {
                Vector2 normalizedDirection =
                    (new Vector2(collider.transform.position.x, collider.transform.position.z) -
                     new Vector2(transform.position.x, transform.position.z)).normalized;
                Vector3 colliderDirection = new Vector3(normalizedDirection.x, 0, normalizedDirection.y);
                Vector3 raySource = new Vector3(transform.position.x, transform.position.y - (_playerHeight / 2) + 0.1f, transform.position.z);
                
                if (Physics.Raycast(raySource, colliderDirection, out RaycastHit hit, shootingRadius, ~LayerMask.GetMask("Player")))
                {
                    if (hit.collider.TryGetComponent<Enemy>(out Enemy emy))
                    {
                        enemy = emy;
                        return true;
                    }
                }
            }
        }

        enemy = null;
        return false;
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
    }

    public void Heal(float health)
    {
        _health.Heal(health);
    }
}
