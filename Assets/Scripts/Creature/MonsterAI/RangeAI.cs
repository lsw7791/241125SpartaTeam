using UnityEngine;

public class RangeAI : MonsterAI
{
    [Header("Attack - Range")]
    [SerializeField]
    private GameObject projectilePrefab;              // 발사할 투사체
    [SerializeField]
    private Transform attackPoint;                   // 투사체 발사 위치

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

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
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
}