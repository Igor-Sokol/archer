using UnityEngine;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour
{
    private bool _isReadyShoot;
    private bool _isReloading;
    
    [SerializeField] private Bullet bullet;
    [SerializeField] private float shootCooldownSeconds;
    [SerializeField] private float damage;
    [FormerlySerializedAs("layerMask")] [SerializeField] private LayerMask enemyLayerMask;

    public bool IsReadyShoot => _isReadyShoot;

    public void Init(Bullet bullet, float cooldown, float damage, LayerMask layerMask)
    {
        this.bullet = bullet;
        this.shootCooldownSeconds = cooldown;
        this.damage = damage;
        this.enemyLayerMask = layerMask;
    }
    
    private void Start()
    {
        Reload();
    }

    public void Shoot(Vector3 goal)
    {
        if (_isReadyShoot && !_isReloading)
        {
            Bullet bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            bulletInstance.Init(damage, enemyLayerMask);
        
            bulletInstance.Fire(goal);
            Reload();
        }
    }

    private void Reload()
    {
        if (!_isReloading)
        {
            _isReloading = true;
            _isReadyShoot = false;
            Timer.Instance.AddTimer(shootCooldownSeconds, () => 
            { 
                _isReadyShoot = true;
                _isReloading = false;
            });
        }
    }
}