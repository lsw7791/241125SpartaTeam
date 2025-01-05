using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoll : MonoBehaviour
{
    public float rollDistance = 5f; // ���� �Ÿ�
    public float rollSpeed = 10f; // ������ �ӵ�
    private Collider playerCollider; // �÷��̾��� �ݶ��̴�

    public bool isRolling = false; // ������ ������ ����
    private void Awake()
    {
        playerCollider = GetComponent<Collider>();
    }
    public IEnumerator Roll()
    {
        Debug.Log("Rolling Started");
        // ���콺 ��Ÿ ���� ���
        Vector3 mouseDelta = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
        if (mouseDelta.magnitude < 0.1f) yield break; // ���콺 ��Ÿ�� �ʹ� ���� ��� ������ ����

        isRolling = true;

        // ĳ���� �ݶ��̴� ��Ȱ��ȭ
        if (playerCollider != null)
        {
            playerCollider.enabled = false;
        }

        // ������ ����� �ӵ� ���
        Vector3 rollDirection = mouseDelta.normalized; // ���콺 �̵� ����
        float rollAmount = 0f; // ���� �Ÿ� �ʱ�ȭ
        Vector3 startPosition = transform.position;

        while (rollAmount < rollDistance)
        {
            Debug.Log("Rolling");
            // �̵� ����
            float distanceThisFrame = rollSpeed * Time.deltaTime;
            transform.position += rollDirection * distanceThisFrame;
            rollAmount += distanceThisFrame;

            yield return null; // ���� �����ӱ��� ��ٸ�
        }
        Debug.Log("Rolling1");

        // ������ ������ �ݶ��̴��� �ٽ� Ȱ��ȭ
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }

        isRolling = false; // ������ �Ϸ�
        Debug.Log("Rolling Ended");

    }
}