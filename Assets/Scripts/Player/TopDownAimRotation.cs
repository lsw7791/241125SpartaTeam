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
        // OnLookEvent���� ���� �̺�Ʈ�� ȣ���ϴ� ��� SendMessage�� ȣ��ǹǷ� 
        // SendMessage�� ȣ��� OnAim�� ���⼭ �ڵ����� ȣ��˴ϴ�.
    }

    private void OnAim(Vector2 direction)
    {
        RotateArm(direction);
    }

    private void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;  // Abs����

        armPivot.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}