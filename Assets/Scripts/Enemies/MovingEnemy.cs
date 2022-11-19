using UnityEngine;
using Random = UnityEngine.Random;

public class MovingEnemy : Enemy
{
    private MovingEnemyConfig _config;
    private Vector3 _moveDirection;
    private bool _readyMoving;
    
    public override void Configure(LevelObjectConfiguration config)
    {
        base.Configure(config);
        if (config is MovingEnemyConfig cfg)
        {
            _config = cfg;
        }
        else
        {
            Debug.LogError("Incorrect configuration.");
        }
    }
    
    private void Awake()
    {
        SetRandomMoveDirection();
    }

    private void Start()
    {
        WaitMoveCooldown();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (_readyMoving)
        {
            transform.position += _moveDirection * (_config.Speed * Time.deltaTime);
        }
    }

    private void WaitMoveCooldown()
    {
        _readyMoving = false;

        Timer.Instance.AddTimer(_config.MoveCooldown, () => { _readyMoving = true; });
    }
    
    private void FacedWall(Collision collision)
    {
        if (!_readyMoving) return;
        
        _readyMoving = false;
        WaitMoveCooldown();
    }

    private void AttackPlayer(ITakeDamge player)
    {
        player.TakeDamage(_config.Damage);
    }
    
    private void OnCollisionEnter(Collision collision) // Don't work at 2+ collision on start TODO
    {
        switch (collision.transform.tag) // Bad TODO
        {
            case "Wall":
                {
                    if (_readyMoving)
                    {
                        FacedWall(collision);
                        ReflectMoveDirection(collision);
                    }
                }
                break;
            case "Player":
                {
                    if (collision.gameObject.TryGetComponent<ITakeDamge>(out ITakeDamge player))
                    {
                        AttackPlayer(player);
                    }
                    ReflectMoveDirection(collision);
                }
                break;
        }
    }

    private void SetRandomMoveDirection()
    {
        Vector2 randomVector = Random.insideUnitCircle.normalized;
        _moveDirection = new Vector3(randomVector.x, 0, randomVector.y);
    }

    private void ReflectMoveDirection(Collision collision)
    {
        _moveDirection = Vector3.Reflect(_moveDirection, collision.GetContact(0).normal);
    }
}