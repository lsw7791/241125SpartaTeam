using UnityEngine;

public class RangeAI : MonsterAI
{
    [Header("Attack - Range")]
    [SerializeField]
    private GameObject projectilePrefab;              // �߻��� ����ü
    [SerializeField]
    private Transform attackPoint;                   // ����ü �߻� ��ġ

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // playerTransform�� null�̸� ������ �÷��̾ ���ٴ� �ǹ̷� �ʱ� ��ġ�� ���ư�
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

            // ���Ÿ� ����: ����ü �߻�
            ShootProjectile();

            // ���� �� ���� ���·� ����
            currentState = MonsterState.Chasing;

            Debug.Log("�÷��̾ �����մϴ�!");
        }
    }

    private void ShootProjectile()
    {
        if (projectilePrefab != null && attackPoint != null)
        {
            //GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);
            GameObject projectile = GameManager.Instance.spawnManager.projectilePool.SpawnFromPool("TestEnemyRangeAttack");
            projectile.transform.position = attackPoint.position;
            // ����ü�� ������ �ο�
            if (projectile.TryGetComponent<ProjectileCollisionHandler>(out var outProjectile))
            {
                projectile.TryGetComponent<Rigidbody2D>(out var outRigidbody2D);
                outProjectile.Initialize(layerMask, GameManager.Instance.dataManager.creature.GetAttack(monster.id));
                // �÷��̾� �������� ����ü �߻�
                Vector3 direction = (playerTransform.position - attackPoint.position);
                direction.Normalize();
                outRigidbody2D.velocity = direction * GameManager.Instance.dataManager.creature.GetAttackSpeed(monster.id);

                // �߻�ü�� Ÿ���� ���ϵ��� ȸ��
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;  // ���� ���
                outProjectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));  // ȸ�� ����
            }
        }
    }
}