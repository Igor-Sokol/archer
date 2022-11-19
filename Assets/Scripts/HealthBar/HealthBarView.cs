using UnityEngine;

public class HealthBarView : MonoBehaviour
{
    private HealthBar _healthBar;
    
    [SerializeField] private HealthBar prefab;
    [SerializeField] private Vector3 positionOffset;

    public void CreateHealthBar(IHealth health)
    {
        _healthBar = Instantiate(prefab, Vector3.zero, Quaternion.identity, HealthContainer.Instance.Container);
        _healthBar.Init(transform, health, positionOffset);
    }
}
