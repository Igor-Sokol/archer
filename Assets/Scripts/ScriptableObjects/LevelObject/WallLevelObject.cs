using UnityEngine;

[CreateAssetMenu(fileName = "Wall", menuName = "LevelObject/Wall", order = 1)]
public class WallLevelObject : LevelObject
{
    [SerializeField] private Wall wall;

    protected override MonoBehaviour GetPrefab() => wall;
    protected override LevelObjectConfiguration GetConfig() => null;
}