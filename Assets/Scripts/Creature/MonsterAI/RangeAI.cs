using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangeAI : MonoBehaviour
{
    private MonsterData monsterData;

    [Header("Targeting")]
    [SerializeField]
    private Transform playerTransform;               // ������ �÷��̾�
    private Vector3 initialPosition;       // ������ �ʱ� ��ġ
    private SpriteRenderer spriteRenderer;

    [Header("Attack")]
    [SerializeField]
    private float curTime = 0f;                       // ���� ��Ÿ��
    [SerializeField]
    private GameObject projectilePrefab;              // �߻��� ����ü
    [SerializeField]
    private Transform attackPoint;                   // ����ü �߻� ��ġ

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

            // ���Ÿ� ����: ����ü �߻�
            ShootProjectile();

            // ���� �� ���� ���·� ����
            currentState = MonsterState.Chasing;

            Debug.Log("�÷��̾ �����մϴ�!");
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && attackPoint != null)
        {
            // �÷��̾� �������� ����ü �߻�
            Vector3 direction = (playerTransform.position - attackPoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);

            // ����ü�� ������ �ο�
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * monsterData.creatureAttackSpeed;
            }
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
            currentState = MonsterState.Chasing;
        }
    }
}