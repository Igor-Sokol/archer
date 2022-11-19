using UnityEngine;

public class ShootingEnemy : Enemy
{
    private ShootingEnemyConfig _config;
    [SerializeField] private Gun gun;

    public override void Configure(LevelObjectConfiguration config)
    {
        base.Configure(config);
        if (config is ShootingEnemyConfig cfg)
        {
            _config = cfg;
            gun.Init(_config.Bullet, _config.ShootCooldown, _config.Damage, LayerMask.GetMask("Player"));
        }
        else
        {
            Debug.LogError("Incorrect configuration.");
        }
    }

    private void Update()
    {
        if (gun.IsReadyShoot && TryFindPlayer(out Player player))
        {
            gun.Shoot(player.transform.position);
        }
    }

    private bool TryFindPlayer(out Player player)
    {
        var colliders = Physics.OverlapSphere(transform.position, _config.AttackRadius, LayerMask.GetMask("Player"));

        foreach (var item in colliders)
        {
            if (item.TryGetComponent<Player>(out Player plr))
            {
                player = plr;
                return true;
            }
        }

        player = null;
        return false;
    }
}