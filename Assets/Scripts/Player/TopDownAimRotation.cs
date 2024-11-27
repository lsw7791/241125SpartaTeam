using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] SpriteRenderer armRenderer;
    [SerializeField] Transform armPivot;
    [SerializeField] SpriteRenderer characterRenderer;

    private TopDownController controller;

    private void Awake()
    {
        controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        // OnLookEvent에서 직접 이벤트를 호출하는 대신 SendMessage로 호출되므로 
        // SendMessage로 호출된 OnAim이 여기서 자동으로 호출됩니다.
    }

    private void OnAim(Vector2 direction)
    {
        RotateArm(direction);
    }

    private void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;  // Abs절댓값

        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}