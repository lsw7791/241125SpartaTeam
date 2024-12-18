using UnityEngine;

public class MeeleAI : MonsterAI
{
    private void Awake()
    {
        GetComponent<Collider2D>();
    }
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
        float distanceToPlayer = Vector2.Distance(_monsterPosition.position, playerTransform.position);

        switch (currentState)
        {
            case MonsterState.Idle:
                break;

            case MonsterState.Chasing:
                if (distanceToPlayer <= GameManager.Instance.DataManager.Creature.GetAttackRange(monster.id))
                {
                    currentState = MonsterState.Attacking;
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
                if (distanceToPlayer > GameManager.Instance.DataManager.Creature.GetDetectionRange(monster.id))
                {
                    ReturnToInitialPosition();
                }
                break;
        }

        if (distanceToPlayer <= GameManager.Instance.DataManager.Creature.GetDetectionRange(monster.id) && currentState != MonsterState.Attacking)
        {
            currentState = MonsterState.Chasing;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<IDamageable>(out var outTarget))
        {
            outTarget.TakeDamage(GameManager.Instance.DataManager.Creature.GetAttack(monster.id));
        }
    }
    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        //if (curTime >= GameManager.Instance.DataManager.Creature.GetAttackSpeed(monster.id))
        //{
        //    curTime = 0f;

        //    // 공격이 끝나면 상태를 Chasing으로 변경
        //    currentState = MonsterState.Chasing;

        //    // 플레이어 공격 (간단한 공격 애니메이션 또는 로직 삽입)

        //}
    }
}