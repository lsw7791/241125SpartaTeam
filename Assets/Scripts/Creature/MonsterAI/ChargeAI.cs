using System.Collections;
using UnityEngine;

public class ChargeAI : MonsterAI
{
    [Header("Attack - Charge")]
    [SerializeField]
    private float chargeDelay = 5f; // 돌진 준비 시간
    [SerializeField]
    private float chargeDuration = 10f; // 돌진 지속 시간

    private Vector3 chargeTargetPosition; // 돌진 목표 위치

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (currentState == MonsterState.Attacking)
        {
            return;
        }

        // playerTransform이 null이면 추적할 플레이어가 없다는 의미로 초기 위치로 돌아감
        if (playerTransform == null && currentState != MonsterState.Attacking)
        {
            currentState = MonsterState.Returning;
            ReturnToInitialPosition();
            return;
        }

        float distanceToPlayer = Vector2.Distance(_monsterPosition.position, playerTransform.position);

        switch (currentState)
        {
            case MonsterState.Idle:
                if (distanceToPlayer <= GameManager.Instance.DataManager.Creature.GetDetectionRange(monster.id))
                {
                    currentState = MonsterState.Chasing;
                }
                break;

            case MonsterState.Chasing:
                if (distanceToPlayer <= GameManager.Instance.DataManager.Creature.GetAttackRange(monster.id))
                {
                    curTime += Time.fixedDeltaTime;
                    // 대기 시간이 지난 후에 돌진 시작
                    if (curTime >= chargeDelay)
                    {
                        currentState = MonsterState.Attacking;
                        chargeTargetPosition = playerTransform.position; // 플레이어의 마지막 위치 저장
                        StartCoroutine(ChargeAttack());
                    }
                }
                else if (distanceToPlayer > GameManager.Instance.DataManager.Creature.GetDetectionRange(monster.id))
                {
                    currentState = MonsterState.Returning;
                }
                else
                {
                    ChasePlayer();
                }
                break;

            case MonsterState.Returning:
                if (distanceToPlayer <= GameManager.Instance.DataManager.Creature.GetDetectionRange(monster.id))
                {
                    currentState = MonsterState.Chasing;
                }
                else
                {
                    ReturnToInitialPosition();
                }
                break;
        }
    }

    private IEnumerator ChargeAttack()
    {
        Debug.Log("돌진 준비 중...");
        isMove = false; // 이동 통제
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f); // 준비 시간 대기

        Debug.Log("돌진 시작!");
        float elapsedTime = 0f;
        Vector3 direction = (chargeTargetPosition - _monsterPosition.position).normalized;

        while (elapsedTime < chargeDuration)
        {
            if(isMove)
            {
                break;
            }

            _monsterPosition.Translate(direction * GameManager.Instance.DataManager.Creature.GetMoveSpeed(monster.id) * 20f * Time.deltaTime, Space.World);
            spriteRenderer.flipX = direction.x < 0;
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }

        Debug.Log("돌진 종료!");
        rb.velocity = Vector2.zero;
        isMove = true; // 이동 재개
        curTime = 0f;
        currentState = MonsterState.Chasing;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isMove && _monsterPosition.position.magnitude != 0)
        {
            // targetLayer에 포함되는 레이어인지 확인
            if (IsLayerMatched(layerMask.value, collision.gameObject.layer))
            {
                StopCoroutine(ChargeAttack());
                Debug.Log("돌진 종료!");
                rb.velocity = Vector2.zero;
                isMove = true; // 이동 재개
                curTime = 0f;
                currentState = MonsterState.Chasing;
            }
        }
    }

    // 레이어가 일치하는지 확인하는 메소드
    private bool IsLayerMatched(LayerMask layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}
