using System;
using UnityEngine;

public class MonsterAI : MonoBehaviour
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
        // 몬스터의 초기 위치 저장
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        // 플레이어 초기 설정 (필요한 경우만, 이후 OnTriggerEnter2D에서 설정되도록 할 수 있음)
        playerTransform = GameObject.FindWithTag("Player")?.transform;
    }

    private void FixedUpdate()
    {
        // playerTransform이 null이면 추적할 플레이어가 없다는 의미로 초기 위치로 돌아감
        if (playerTransform == null)
        {
            currentState = MonsterState.Returning;
            ReturnToInitialPosition();
            return;
        }

        // 몬스터가 플레이어와의 거리를 계산
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

            // 공격이 끝나면 상태를 Chasing으로 변경
            currentState = MonsterState.Chasing;

            // 플레이어 공격 (간단한 공격 애니메이션 또는 로직 삽입)
            Debug.Log("플레이어를 공격합니다!");
        }
    }

    private void ReturnToInitialPosition()
    {
        Debug.Log("초기 위치로 이동합니다!");

        // 몬스터가 초기 위치로 돌아가는 행동
        Vector3 direction = (initialPosition - transform.position).normalized;

        // 몬스터가 초기 위치에 거의 도달한 경우, 이동을 멈추고 방향 반전 안함
        if (Vector3.Distance(transform.position, initialPosition) > 0.1f)  // 0.1f는 거의 도달했다고 판단할 수 있는 최소 거리
        {
            transform.Translate(direction * monsterData.creatureMoveSpeed * Time.deltaTime, Space.World);
            spriteRenderer.flipX = direction.x < 0;  // 이동 중에만 방향을 바꿈
        }
    }
    private void ChasePlayer()
    {
        Debug.Log("플레이어를 추적합니다!");

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * monsterData.creatureMoveSpeed * Time.deltaTime, Space.World);

        spriteRenderer.flipX = direction.x <= 0;
    }   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌 시 플레이어 추적 시작
        if (collision.CompareTag("Player"))
        {
            playerTransform = collision.transform;
            currentState = MonsterState.Chasing;
        }
    }
}