using UnityEngine;

public class RangeAI : MonsterAI
{
    [Header("Attack - Range")]
    [SerializeField]
    private GameObject projectilePrefab;              // 발사할 투사체
    [SerializeField]
    private Transform attackPoint;                   // 투사체 발사 위치

    private void Awake()
    {
        attackPoint = this.transform;
        projectilePrefab = Resources.Load<GameObject>("Prefabs/AttackObjects/TestEnemyRangeAttack");
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

            case MonsterState.Attacking:
                AttackPlayer();
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

    protected override void AttackPlayer()
    {
        base.AttackPlayer();
        if (curTime >= GameManager.Instance.DataManager.Creature.GetAttackSpeed(monster.id))
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
            //GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);
            GameObject projectile = GameManager.Instance.SpawnManager.projectilePool.SpawnFromPool("TestEnemyRangeAttack");
            projectile.transform.position = attackPoint.position;
            // 투사체에 방향을 부여
            if (projectile.TryGetComponent<ProjectileCollisionHandler>(out var outProjectile))
            {
                projectile.TryGetComponent<Rigidbody2D>(out var outRigidbody2D);
                outProjectile.Initialize(_layerMask, _obstacleLayer, AttackMinusDef());
                // 플레이어 방향으로 투사체 발사
                Vector3 direction = (playerTransform.position - attackPoint.position);
                direction.Normalize();
                outRigidbody2D.velocity = direction * GameManager.Instance.DataManager.Creature.GetAttackSpeed(monster.id);

                // 발사체가 타겟을 향하도록 회전
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // 각도 계산
                outProjectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));  // 회전 적용
            }
        }
    }
}