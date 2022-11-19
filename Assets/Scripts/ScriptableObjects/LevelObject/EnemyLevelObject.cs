using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "LevelObject/Enemy", order = 1)]
public class EnemyLevelObject : LevelObject
{
    [SerializeField] private Enemy prefab;
    [SerializeField] private EnemyConfig config;
    
    protected override MonoBehaviour GetPrefab() => prefab;
    protected override LevelObjectConfiguration GetConfig() => config;
}