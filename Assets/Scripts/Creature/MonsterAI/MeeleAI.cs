using UnityEngine;

public class MeeleAI : MonsterAI
{
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

        // ���Ͱ� �÷��̾���� �Ÿ��� ���
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

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

            // ������ ������ ���¸� Chasing���� ����
            currentState = MonsterState.Chasing;

            // �÷��̾� ���� (������ ���� �ִϸ��̼� �Ǵ� ���� ����)

        }
    }
}