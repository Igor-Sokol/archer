using System;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Level level;

    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;

    [SerializeField] private float startX;
    [SerializeField] private float startY;

    [SerializeField] private Transform spawnContainer;

    private void Start()
    {
        Generate(level);
    }

    public void Generate(Level level)
    {
        for (int i = 0; i < level.LevelObjects.Length; i++)
        {
            LevelObjectData objectData = level.LevelObjects[i];

            if (IsLevelObjectPositionCorrect(objectData.position))
            {
                Debug.LogWarning($"Level Object {i} has incorrect position.");
                continue;
            }
            
            Vector3 spawnPosition = new Vector3(startX + objectData.position.x - 1, 0, startY + objectData.position.y - 1);
            var instance = Instantiate(objectData.levelObject.Prefab, spawnPosition, Quaternion.identity, spawnContainer);

            if (objectData.levelObject.Config != null && instance is ILevelObjectConfigure configurable)
            {
                configurable.Configure(objectData.levelObject.Config);
            }
        }
    }

    private bool IsLevelObjectPositionCorrect(Vector2 position) =>
        (position.x < 1 || position.x > sizeX) || (position.y < 1 || position.y > sizeY);
}