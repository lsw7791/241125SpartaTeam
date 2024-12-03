using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChargeAI : MonoBehaviour
{
    private MonsterData monsterData;

    [Header("Targeting")]
    [SerializeField]
    private Transform playerTransform;               // 추적할 플레이어
    private Vector3 initialPosition;       // 몬스터의 초기 위치
    private SpriteRenderer spriteRenderer;

    [Header("Attack")]
    [SerializeField]
    public float curTime = 0f;
    [SerializeField]
    private float chargeDelay = 1f;        // 돌진 전 대기 시간 1초

    private Vector3 chargeTargetPosition; // 돌진 목표 위치 (돌진 시작 시 플레이어 위치 기록)
    private bool isCharging = false;      // 돌진 상태를 추적하는 변수
    private float chargeStartTime = 0f;   // 돌진 시작 시간을 추적

    // 몬스터 상태를 Enum으로 관리
    private enum MonsterState
    {
        Idle,        // 대기
        Chasing,     // 추적 중
        Attacking,   // 공격 중 (돌진)
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
            chargeStartTime = Time.time;  // 돌진 시작 시간 기록
            chargeTargetPosition = playerTransform.position;  // 플레이어의 현재 위치 저장
            Debug.Log("돌진 준비 중...");
        }

        // 대기 시간이 지난 후에 돌진 시작
        if (Time.time - chargeStartTime >= chargeDelay)
        {
            Vector3 direction = (chargeTargetPosition - transform.position).normalized;
            transform.Translate(direction * monsterData.creatureMoveSpeed * 3 * Time.deltaTime, Space.World); // 3배 빠르게 돌진

            spriteRenderer.flipX = direction.x <= 0;

            // 돌진 후 상태를 Chasing으로 변경
            currentState = MonsterState.Chasing;

            // 플레이어에게 피해를 입히는 로직 (피해량이나 상태 변화 등을 처리할 수 있음)
            Debug.Log("플레이어에게 피해를 입혔습니다!");
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
            currentState = MonsterState.Chasing;  // 플레이어와 접촉 시 추적 상태로 변경
        }
    }
}