using UnityEngine;

[CreateAssetMenu(fileName = "MovingEnemy", menuName = "LevelObjectConfiguration/MovingEnemyConfig", order = 1)]
public class MovingEnemyConfig : EnemyConfig
{
    [SerializeField] private float speed;
    [SerializeField] private float moveCooldown;

    public float Speed => speed;
    public float MoveCooldown => moveCooldown;
}