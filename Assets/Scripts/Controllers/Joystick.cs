using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 _joystickCenterPoint;
    private float _joystickRadius;

    public Vector2 Axis { get; private set; }
    public float SpeedMultiplier { get; private set; }

    [SerializeField] private RectTransform minLeft;
    [SerializeField] private RectTransform maxRight;
    [SerializeField] private RectTransform stick;

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Vector2 direction = Vector2.ClampMagnitude(eventData.position - _joystickCenterPoint, _joystickRadius);

        stick.position = _joystickCenterPoint + direction;
        Axis = direction.normalized;
        SpeedMultiplier = direction.magnitude / _joystickRadius;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        stick.position = _joystickCenterPoint;
        Axis = Vector2.zero;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        _joystickCenterPoint = (minLeft.position + maxRight.position) / 2f;
        _joystickRadius = (maxRight.position.x - minLeft.position.x) / 2f;
    }
}
