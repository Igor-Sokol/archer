using UnityEngine;

public abstract class LevelObject : ScriptableObject
{
    public MonoBehaviour Prefab => GetPrefab();

    public LevelObjectConfiguration Config => GetConfig();
    
    protected abstract MonoBehaviour GetPrefab();
    protected abstract LevelObjectConfiguration GetConfig();
}