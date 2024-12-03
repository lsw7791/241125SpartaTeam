using System;
using UnityEngine;

public class MonsterAI : MonoBehaviour
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

    // ���� ���¸� Enum���� ����
    private enum MonsterState
    {
        Idle,        // ���
        Chasing,     // ���� ��
        Attacking,   // ���� ��
        Returning    // �ʱ� ��ġ�� ���ư�
    }

    private MonsterState currentState = MonsterState.Idle;  // ���� ����

    private void Start()
    {
        monsterData = GetComponent<MonsterData>();
        // ������ �ʱ� ��ġ ����
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        // �÷��̾� �ʱ� ���� (�ʿ��� ��츸, ���� OnTriggerEnter2D���� �����ǵ��� �� �� ����)
        playerTransform = GameObject.FindWithTag("Player")?.transform;
    }

    private void FixedUpdate()
    {
        // playerTransform�� null�̸� ������ �÷��̾ ���ٴ� �ǹ̷� �ʱ� ��ġ�� ���ư�
        if (playerTransform == null)
        {
            currentState = MonsterState.Returning;
            ReturnToInitialPosition();
            return;
        }

        // ���Ͱ� �÷��̾���� �Ÿ��� ���
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
                AttackPlayer();
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

    private void AttackPlayer()
    {
        curTime += Time.deltaTime;
        if (curTime >= monsterData.creatureAttackSpeed)
        {
            curTime = 0f;

            // ������ ������ ���¸� Chasing���� ����
            currentState = MonsterState.Chasing;

            // �÷��̾� ���� (������ ���� �ִϸ��̼� �Ǵ� ���� ����)
            Debug.Log("�÷��̾ �����մϴ�!");
        }
    }

    private void ReturnToInitialPosition()
    {
        Debug.Log("�ʱ� ��ġ�� �̵��մϴ�!");

        // ���Ͱ� �ʱ� ��ġ�� ���ư��� �ൿ
        Vector3 direction = (initialPosition - transform.position).normalized;

        // ���Ͱ� �ʱ� ��ġ�� ���� ������ ���, �̵��� ���߰� ���� ���� ����
        if (Vector3.Distance(transform.position, initialPosition) > 0.1f)  // 0.1f�� ���� �����ߴٰ� �Ǵ��� �� �ִ� �ּ� �Ÿ�
        {
            transform.Translate(direction * monsterData.creatureMoveSpeed * Time.deltaTime, Space.World);
            spriteRenderer.flipX = direction.x < 0;  // �̵� �߿��� ������ �ٲ�
        }
    }
    private void ChasePlayer()
    {
        Debug.Log("�÷��̾ �����մϴ�!");

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * monsterData.creatureMoveSpeed * Time.deltaTime, Space.World);

        spriteRenderer.flipX = direction.x <= 0;
    }   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �浹 �� �÷��̾� ���� ����
        if (collision.CompareTag("Player"))
        {
            playerTransform = collision.transform;
            currentState = MonsterState.Chasing;
        }
    }
}