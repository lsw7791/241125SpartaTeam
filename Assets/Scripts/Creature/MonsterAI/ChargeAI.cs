using UnityEngine;

public class ChargeAI : MonsterAI
{
    [Header("Attack - Charge")]
    [SerializeField]
    private float chargeDelay = 1f;        // ���� �� ��� �ð� 1��

    private Vector3 chargeTargetPosition; // ���� ��ǥ ��ġ (���� ���� �� �÷��̾� ��ġ ���)
    private bool isCharging = false;      // ���� ���¸� �����ϴ� ����
    private float chargeStartTime = 0f;   // ���� ���� �ð��� ����

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
        
        if (!isCharging)
        {
            isCharging = true;
            chargeStartTime = Time.time;  // ���� ���� �ð� ���
            chargeTargetPosition = playerTransform.position;  // �÷��̾��� ���� ��ġ ����
            Debug.Log("���� �غ� ��...");
        }

        // ��� �ð��� ���� �Ŀ� ���� ����
        if (Time.time - chargeStartTime >= chargeDelay)
        {
            Vector3 direction = (chargeTargetPosition - transform.position).normalized;
            transform.Translate(direction * monsterData.creatureMoveSpeed * 3 * Time.deltaTime, Space.World); // 3�� ������ ����

            spriteRenderer.flipX = direction.x <= 0;

            // ���� �� ���¸� Chasing���� ����
            currentState = MonsterState.Chasing;

            // �÷��̾�� ���ظ� ������ ���� (���ط��̳� ���� ��ȭ ���� ó���� �� ����)
            Debug.Log("�÷��̾�� ���ظ� �������ϴ�!");
        }
    }
}