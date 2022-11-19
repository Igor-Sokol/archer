using UnityEngine;

[CreateAssetMenu(fileName = "ShootingEnemy", menuName = "LevelObjectConfiguration/ShootingEnemyConfig", order = 1)]
public class ShootingEnemyConfig : EnemyConfig
{
    [SerializeField] private float shootCooldown;
    [SerializeField] private Bullet bullet;
    [SerializeField] private float attackRadius;

    public float ShootCooldown => shootCooldown;
    public Bullet Bullet => bullet;
    public float AttackRadius => attackRadius;
}