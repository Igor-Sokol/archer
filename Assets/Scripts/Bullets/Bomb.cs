using UnityEngine;

public class Bomb : Bullet
{
    private Vector3 _startPosition;
    private Vector3 _goalPosition;
    private bool _isActive;
    private float _timer;

    [SerializeField] private float explodeRadius;
    [SerializeField] private float throwHeight;
    [SerializeField] private float timeToGoal;
    
    private void Awake()
    {
        _startPosition = transform.position;
    }

    public override void Fire(Vector3 goal)
    {
        _goalPosition = goal;
        _isActive = true;
    }
    
    private void Update()
    {
        if (!_isActive) return;

        Move();
    }

    private void Move()
    {
        _timer += Time.deltaTime * Speed;

        float t = _timer / timeToGoal;
        
        Vector2 horizontalPosition = Vector2.Lerp(new Vector2(_startPosition.x, _startPosition.z),
            new Vector2(_goalPosition.x, _goalPosition.z), t);
        float verticalPosition = Mathf.Sin(t * Mathf.PI) * throwHeight;

        transform.position = new Vector3(horizontalPosition.x, verticalPosition, horizontalPosition.y);

        if (t >= 1)
        {
            Explode();
        }
    }

    private void Explode()
    {
        var foundTargets = Physics.OverlapSphere(transform.position, explodeRadius, EnemyLayerMask);

        foreach (var item in foundTargets)
        {
            if (item.TryGetComponent<ITakeDamge>(out ITakeDamge health))
            {
                health.TakeDamage(Damage);
            }
        }
        
        _isActive = false;
        Destroy(gameObject);
    }
}