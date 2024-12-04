using UnityEngine;

public class RangeAI : MonsterAI
{
    [Header("Attack - Range")]
    [SerializeField]
    private GameObject projectilePrefab;              // �߻��� ����ü
    [SerializeField]
    private Transform attackPoint;                   // ����ü �߻� ��ġ

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
            // �÷��̾� �������� ����ü �߻�
            Vector3 direction = (playerTransform.position - attackPoint.position).normalized;
            GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);

            // ����ü�� ������ �ο�
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * monsterData.creatureAttackSpeed;
            }
        }
    }
}