using UnityEngine;

public class EnemyConfig : LevelObjectConfiguration
{
    [SerializeField] private float damage;
    [SerializeField] private float health;

    public float Damage => damage;
    public float Health => health;
}