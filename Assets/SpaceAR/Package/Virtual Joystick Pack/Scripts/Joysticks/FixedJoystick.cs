﻿using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    [Header("Fixed Joystick")]


    Vector2 joystickPosition = Vector2.zero;
    private Camera cam = new Camera();

    public override void OnDrag(PointerEventData eventData)
    {
        if (joystickPosition == Vector2.zero)
        {
            joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        }
        Vector2 direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
        joystickPosition = Vector2.zero;
    }
}