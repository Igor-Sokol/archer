using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Arrow : Bullet
{
    private Rigidbody _rigidbody;
    private bool _isActive;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Fire(Vector3 goal)
    {
        transform.LookAt(goal);
        _isActive = true;
    }

    private void FixedUpdate()
    {
        if (!_isActive) return;
        
        _rigidbody.MovePosition(transform.position + transform.forward * (Time.deltaTime * Speed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer) == EnemyLayerMask && other.TryGetComponent<ITakeDamge>(out ITakeDamge enemy))
        {
            enemy.TakeDamage(Damage);
        }

        _isActive = false;
        Destroy(gameObject);
    }
}