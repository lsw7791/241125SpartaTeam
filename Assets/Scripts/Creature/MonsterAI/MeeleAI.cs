using UnityEngine;

public class MeeleAI : MonsterAI
{
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

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
        base.AttackPlayer();
        if (curTime >= DataManager.Instance.creature.GetAttackSpeed(monster.id))
        {
            curTime = 0f;

            // 공격이 끝나면 상태를 Chasing으로 변경
            currentState = MonsterState.Chasing;

            // 플레이어 공격 (간단한 공격 애니메이션 또는 로직 삽입)
            Debug.Log("플레이어를 공격합니다!");

        }
    }
}