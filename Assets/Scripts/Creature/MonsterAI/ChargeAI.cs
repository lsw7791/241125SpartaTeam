using UnityEngine;

public class ChargeAI : MonsterAI
{
    [Header("Attack - Charge")]
    [SerializeField]
    private float chargeDelay = 1f;        // 돌진 전 대기 시간 1초

    private Vector3 chargeTargetPosition; // 돌진 목표 위치 (돌진 시작 시 플레이어 위치 기록)
    private bool isCharging = false;      // 돌진 상태를 추적하는 변수
    private float chargeStartTime = 0f;   // 돌진 시작 시간을 추적

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
                if (distanceToPlayer <= DataManager.Instance.creature.GetAttackRange(monster.id))
                {
                    currentState = MonsterState.Attacking;
                }
                else if (distanceToPlayer > DataManager.Instance.creature.GetDetectionRange(monster.id))
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
                if (distanceToPlayer > DataManager.Instance.creature.GetDetectionRange(monster.id))
                {
                    ReturnToInitialPosition();
                }
                break;
        }

        if (distanceToPlayer <= DataManager.Instance.creature.GetDetectionRange(monster.id) && currentState != MonsterState.Attacking)
        {
            currentState = MonsterState.Chasing;
        }
    }

    protected override void AttackPlayer()
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
            transform.Translate(direction * DataManager.Instance.creature.GetMoveSpeed(monster.id) * 3 * Time.deltaTime, Space.World); // 3배 빠르게 돌진

            spriteRenderer.flipX = direction.x <= 0;

            // 돌진 후 상태를 Chasing으로 변경
            currentState = MonsterState.Chasing;

            // 플레이어에게 피해를 입히는 로직 (피해량이나 상태 변화 등을 처리할 수 있음)
            Debug.Log("플레이어에게 피해를 입혔습니다!");
        }
    }
}