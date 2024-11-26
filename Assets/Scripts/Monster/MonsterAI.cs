using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float moveSpeed = 2f;           // 몬스터의 이동 속도
    public float detectionRange = 10f;     // 플레이어를 감지할 수 있는 범위
    public float attackRange = 1.5f;       // 공격 범위
    public Transform player;               // 추적할 플레이어
    private Vector3 initialPosition;       // 몬스터의 초기 위치

    private void Start()
    {
        // 몬스터의 초기 위치 저장
        initialPosition = transform.position;
    }

    private void Update()
    {
        // 몬스터가 플레이어를 감지하는 범위 내에 있다면 추적
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // 공격 범위에 들어왔으면 공격 행동
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // 감지 범위에 들어왔으면 플레이어를 추적
            ChasePlayer();
        }
        else
        {
            // 플레이어가 범위 밖에 있으면 초기 위치로 돌아가기
            ReturnToInitialPosition();
        }
    }

    private void ChasePlayer()
    {
        // 플레이어 방향으로 이동
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    private void AttackPlayer()
    {
        // 플레이어 공격 (간단한 공격 애니메이션 또는 로직 삽입)
        Debug.Log("플레이어를 공격합니다!");
        // 예: 공격 애니메이션 실행 또는 플레이어 체력 감소 등의 행동을 여기에 추가
    }

    private void ReturnToInitialPosition()
    {
        // 몬스터가 초기 위치로 돌아가는 행동
        Vector3 direction = (initialPosition - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
}