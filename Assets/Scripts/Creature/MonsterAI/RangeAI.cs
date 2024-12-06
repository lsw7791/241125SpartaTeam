using UnityEngine;

public class RangeAI : MonsterAI
{
    [Header("Attack - Range")]
    [SerializeField]
    private GameObject projectilePrefab;              // 발사할 투사체
    [SerializeField]
    private Transform attackPoint;                   // 투사체 발사 위치

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

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        switch (currentState)
        {
            case MonsterState.Idle:
                break;

            case MonsterState.Chasing:
                if (distanceToPlayer <= GameManager.Instance.dataManager.creature.GetAttackRange(monster.id))
                {
                    currentState = MonsterState.Attacking;
                }
                else if (distanceToPlayer > GameManager.Instance.dataManager.creature.GetDetectionRange(monster.id))
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
                if (distanceToPlayer > GameManager.Instance.dataManager.creature.GetDetectionRange(monster.id))
                {
                    ReturnToInitialPosition();
                }
                break;
        }

        if (distanceToPlayer <= GameManager.Instance.dataManager.creature.GetDetectionRange(monster.id) && currentState != MonsterState.Attacking)
        {
            currentState = MonsterState.Chasing;
        }
    }

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        if (curTime >= GameManager.Instance.dataManager.creature.GetAttackSpeed(monster.id))
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
            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);
            //GameManager.Instance.projectilePool.GetProjectile();

            // 투사체에 방향을 부여
            if (projectile.TryGetComponent<Projectile>(out var outProjectile))
            {
                outProjectile.damage = GameManager.Instance.dataManager.creature.GetAttack(monster.id);
                projectile.TryGetComponent<Rigidbody2D>(out var outRigidbody2D);
                outProjectile.Initialize(enemyLayer);
                // 플레이어 방향으로 투사체 발사
                Vector3 direction = (playerTransform.position - attackPoint.position).normalized;
                outRigidbody2D.velocity = direction * GameManager.Instance.dataManager.creature.GetAttackSpeed(monster.id);
            }
        }
    }
}