using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChargeAI : MonoBehaviour
{
    private MonsterData monsterData;

    [Header("Targeting")]
    [SerializeField]
    private Transform playerTransform;               // ������ �÷��̾�
    private Vector3 initialPosition;       // ������ �ʱ� ��ġ
    private SpriteRenderer spriteRenderer;

    [Header("Attack")]
    [SerializeField]
    public float curTime = 0f;
    [SerializeField]
    private float chargeDelay = 1f;        // ���� �� ��� �ð� 1��

    private Vector3 chargeTargetPosition; // ���� ��ǥ ��ġ (���� ���� �� �÷��̾� ��ġ ���)
    private bool isCharging = false;      // ���� ���¸� �����ϴ� ����
    private float chargeStartTime = 0f;   // ���� ���� �ð��� ����

    // ���� ���¸� Enum���� ����
    private enum MonsterState
    {
        Idle,        // ���
        Chasing,     // ���� ��
        Attacking,   // ���� �� (����)
        Returning    // �ʱ� ��ġ�� ���ư�
    }

    private MonsterState currentState = MonsterState.Idle;  // ���� ����

    private void Start()
    {
        monsterData = GetComponent<MonsterData>();
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindWithTag("Player")?.transform;
    }

    private void FixedUpdate()
    {
        if (playerTransform == null)
        {
            currentState = MonsterState.Returning;
            ReturnToInitialPosition();
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        switch (currentState)
        {
            case MonsterState.Idle:
                break;

            case MonsterState.Chasing:
                if (distanceToPlayer <= monsterData.creatureAttackRange)
                {
                    currentState = MonsterState.Attacking;
                }
                else if (distanceToPlayer > monsterData.creatureDetectionRange)
                {
                    currentState = MonsterState.Returning;
                }
                else
                {
                    ChasePlayer();
                }
                break;

            case MonsterState.Attacking:
                ChargePlayer();
                break;

            case MonsterState.Returning:
                if (distanceToPlayer > monsterData.creatureDetectionRange)
                {
                    ReturnToInitialPosition();
                }
                break;
        }

        if (distanceToPlayer <= monsterData.creatureDetectionRange && currentState != MonsterState.Attacking)
        {
            currentState = MonsterState.Chasing;
        }
    }

    private void ChargePlayer()
    {
        if (!isCharging)
        {
            isCharging = true;
            chargeStartTime = Time.time;  // ���� ���� �ð� ���
            chargeTargetPosition = playerTransform.position;  // �÷��̾��� ���� ��ġ ����
            Debug.Log("���� �غ� ��...");
        }

        // ��� �ð��� ���� �Ŀ� ���� ����
        if (Time.time - chargeStartTime >= chargeDelay)
        {
            Vector3 direction = (chargeTargetPosition - transform.position).normalized;
            transform.Translate(direction * monsterData.creatureMoveSpeed * 3 * Time.deltaTime, Space.World); // 3�� ������ ����

            spriteRenderer.flipX = direction.x <= 0;

            // ���� �� ���¸� Chasing���� ����
            currentState = MonsterState.Chasing;

            // �÷��̾�� ���ظ� ������ ���� (���ط��̳� ���� ��ȭ ���� ó���� �� ����)
            Debug.Log("�÷��̾�� ���ظ� �������ϴ�!");
        }
    }

    private void ReturnToInitialPosition()
    {
        Vector3 direction = (initialPosition - transform.position).normalized;

        if (Vector3.Distance(transform.position, initialPosition) > 0.1f)
        {
            transform.Translate(direction * monsterData.creatureMoveSpeed * Time.deltaTime, Space.World);
            spriteRenderer.flipX = direction.x < 0;
        }
    }

    private void ChasePlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * monsterData.creatureMoveSpeed * Time.deltaTime, Space.World);
        spriteRenderer.flipX = direction.x <= 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTransform = collision.transform;
            currentState = MonsterState.Chasing;  // �÷��̾�� ���� �� ���� ���·� ����
        }
    }
}