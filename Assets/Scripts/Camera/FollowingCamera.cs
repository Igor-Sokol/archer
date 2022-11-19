using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool freezeX;
    [SerializeField] private bool freezeZ;

    private void Update()
    {
        transform.position = new Vector3(freezeX ? transform.position.x : target.position.x,
            transform.position.y,
            freezeZ ? transform.position.z : target.position.z) + offset;
    }
}
