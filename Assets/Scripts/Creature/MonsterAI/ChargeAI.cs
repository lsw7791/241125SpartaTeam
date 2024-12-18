using System.Collections;
using UnityEngine;

public class ChargeAI : MonsterAI
{
    [Header("Attack - Charge")]
    [SerializeField]
    private float chargeDelay = 5f; // ���� �غ� �ð�
    [SerializeField]
    private float chargeDuration = 10f; // ���� ���� �ð�

    private Vector3 chargeTargetPosition; // ���� ��ǥ ��ġ

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (currentState == MonsterState.Attacking)
        {
            return;
        }

        // playerTransform�� null�̸� ������ �÷��̾ ���ٴ� �ǹ̷� �ʱ� ��ġ�� ���ư�
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
                    // ��� �ð��� ���� �Ŀ� ���� ����
                    if (curTime >= chargeDelay)
                    {
                        currentState = MonsterState.Attacking;
                        chargeTargetPosition = playerTransform.position; // �÷��̾��� ������ ��ġ ����
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
        Debug.Log("���� �غ� ��...");
        isMove = false; // �̵� ����
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(2f); // �غ� �ð� ���

        Debug.Log("���� ����!");
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

        Debug.Log("���� ����!");
        rb.velocity = Vector2.zero;
        isMove = true; // �̵� �簳
        curTime = 0f;
        currentState = MonsterState.Chasing;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isMove && _monsterPosition.position.magnitude != 0)
        {
            // targetLayer�� ���ԵǴ� ���̾����� Ȯ��
            if (IsLayerMatched(layerMask.value, collision.gameObject.layer))
            {
                StopCoroutine(ChargeAttack());
                Debug.Log("���� ����!");
                rb.velocity = Vector2.zero;
                isMove = true; // �̵� �簳
                curTime = 0f;
                currentState = MonsterState.Chasing;
            }
        }
    }

    // ���̾ ��ġ�ϴ��� Ȯ���ϴ� �޼ҵ�
    private bool IsLayerMatched(LayerMask layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }
}
