using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera mainCamera;  // ���� ī�޶�
    public Vector3 offset;     // ī�޶�� �÷��̾� ���� ����� �Ÿ�
    public float smoothTime = 0.3f; // ī�޶� �̵� �ε巯�� ������ �����ϴ� �� (�������� �� �ε巴��, Ŀ������ �� ������ �̵�)

    private Vector3 velocity = Vector3.zero;  // ī�޶��� ���� �ӵ�
    void Start()
    {
        GameManager.Instance.player._playerCamera = this;
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        // �⺻ offset�� �����Ͽ� ī�޶�� �÷��̾��� ��� ��ġ�� ����
        if (offset == Vector3.zero)
        {
            offset = new Vector3(0, 0, -10);  // �⺻������ Z�� -10���� ����
        }
    }

    void Update()
    {
        // ��ǥ ��ġ�� �÷��̾��� ��ġ + ������ (Z�� ����)
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, -10);

        // ī�޶� �ε巴�� ��ǥ ��ġ�� �̵�
        mainCamera.transform.position = Vector3.SmoothDamp(
            mainCamera.transform.position,  // ���� ��ġ
            targetPosition,                  // ��ǥ ��ġ
            ref velocity,                    // ī�޶��� ���� �ӵ�
            smoothTime);                     // �̵� �ε巯�� ����
    }
}
