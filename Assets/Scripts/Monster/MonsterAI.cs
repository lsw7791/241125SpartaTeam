using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float moveSpeed = 2f;           // ������ �̵� �ӵ�
    public float detectionRange = 10f;     // �÷��̾ ������ �� �ִ� ����
    public float attackRange = 1.5f;       // ���� ����
    public Transform player;               // ������ �÷��̾�
    private Vector3 initialPosition;       // ������ �ʱ� ��ġ

    private void Start()
    {
        // ������ �ʱ� ��ġ ����
        initialPosition = transform.position;
    }

    private void Update()
    {
        // ���Ͱ� �÷��̾ �����ϴ� ���� ���� �ִٸ� ����
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // ���� ������ �������� ���� �ൿ
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // ���� ������ �������� �÷��̾ ����
            ChasePlayer();
        }
        else
        {
            // �÷��̾ ���� �ۿ� ������ �ʱ� ��ġ�� ���ư���
            ReturnToInitialPosition();
        }
    }

    private void ChasePlayer()
    {
        // �÷��̾� �������� �̵�
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    private void AttackPlayer()
    {
        // �÷��̾� ���� (������ ���� �ִϸ��̼� �Ǵ� ���� ����)
        Debug.Log("�÷��̾ �����մϴ�!");
        // ��: ���� �ִϸ��̼� ���� �Ǵ� �÷��̾� ü�� ���� ���� �ൿ�� ���⿡ �߰�
    }

    private void ReturnToInitialPosition()
    {
        // ���Ͱ� �ʱ� ��ġ�� ���ư��� �ൿ
        Vector3 direction = (initialPosition - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
}