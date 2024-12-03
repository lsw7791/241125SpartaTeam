using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RangeAI : MonoBehaviour
{
    private MonsterData monsterData;

    [Header("Targeting")]
    [SerializeField]
    private Transform playerTransform;               // 추적할 플레이어
    private Vector3 initialPosition;       // 몬스터의 초기 위치
    private SpriteRenderer spriteRenderer;

    [Header("Attack")]
    [SerializeField]
    private float curTime = 0f;                       // 공격 쿨타임
    [SerializeField]
    private GameObject projectilePrefab;              // 발사할 투사체
    [SerializeField]
    private Transform attackPoint;                   // 투사체 발사 위치

    // 몬스터 상태를 Enum으로 관리
    private enum MonsterState
    {
        Idle,        // 대기
        Chasing,     // 추적 중
        Attacking,   // 공격 중
        Returning    // 초기 위치로 돌아감
    }

    private MonsterState currentState = MonsterState.Idle;  // 현재 상태

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

            // 원거리 공격: 투사체 발사
            ShootProjectile();

            // 공격 후 추적 상태로 변경
            currentState = MonsterState.Chasing;

            Debug.Log("플레이어를 공격합니다!");
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && attackPoint != null)
        {
            // 플레이어 방향으로 투사체 발사
            Vector3 direction = (playerTransform.position - attackPoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);

            // 투사체에 방향을 부여
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