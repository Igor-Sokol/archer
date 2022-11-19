using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    
    [SerializeField] private float rotateSpeed;
    [FormerlySerializedAs("speed")] [SerializeField] private float moveSpeed;
    [SerializeField] private Joystick joystick;

    public bool IsMoving { get; private set; }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = joystick.Axis.x * joystick.SpeedMultiplier;
        float vertical = joystick.Axis.y * joystick.SpeedMultiplier;

        Vector3 movement = new Vector3(horizontal * moveSpeed, 0, vertical * moveSpeed);
        IsMoving = movement.sqrMagnitude > 0f;
        _characterController.Move(movement * Time.deltaTime);

        if (Vector3.Angle(transform.forward, movement) > 0)
        {
            Vector3 rotation = Vector3.RotateTowards(transform.forward, movement, rotateSpeed, 0);
            transform.rotation = Quaternion.LookRotation(rotation);
        }
    }
}
