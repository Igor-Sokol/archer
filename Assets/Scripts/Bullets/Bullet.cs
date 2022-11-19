using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected LayerMask EnemyLayerMask;
    protected float Damage;
    
    [SerializeField] protected float Speed;

    public abstract void Fire(Vector3 goal);

    public void Init(float damage, LayerMask layerMask)
    {
        Damage = damage;
        EnemyLayerMask = layerMask;
    }
}