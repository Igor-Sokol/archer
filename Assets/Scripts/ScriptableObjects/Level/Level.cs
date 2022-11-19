using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Level/Level", order = 1)]
public class Level : ScriptableObject
{
    [SerializeField] private LevelObjectData[] levelObjects;

    public LevelObjectData[] LevelObjects => levelObjects;
}

[Serializable]
public struct LevelObjectData
{
    public LevelObject levelObject;
    public Vector2 position;
}