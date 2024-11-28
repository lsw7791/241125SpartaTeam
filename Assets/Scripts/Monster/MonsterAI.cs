using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    [Header("Targeting")]
    public float moveSpeed = 2f;           // ������ �̵� �ӵ�
    public float detectionRange = 1f;     // �÷��̾ ������ �� �ִ� ����
    public float attackRange = 0.5f;       // ���� ����
    public Transform player;               // ������ �÷��̾�
    private Vector3 initialPosition;       // ������ �ʱ� ��ġ
    private SpriteRenderer spriteRenderer;

    [Header("Attack")]
    public float damage = 2f;
    public float delayTime = 2f;
    public float curTime = 0f;

    private void Start()
    {
        // ������ �ʱ� ��ġ ����
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(player == null)
        {
            ReturnToInitialPosition();
            return;
        }

        // ���Ͱ� �÷��̾ �����ϴ� ���� ���� �ִٸ� ����
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            // ���� ������ �������� ���� �ൿ
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // ���� ������ �������� �÷��̾ ����
            ChasePlayer();
            //Debug.Log("��ǥ �߰�");
        }
        else
        {
            // �÷��̾ ���� �ۿ� ������ �ʱ� ��ġ�� ���ư���
            //ReturnToInitialPosition();
            player = null;
        }
    }

    private void ChasePlayer()
    {
        // �÷��̾� �������� �̵�
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        spriteRenderer.flipX = direction.x <= 0;
    }

    private void AttackPlayer()
    {
        curTime += Time.deltaTime;
        if (curTime >= delayTime)
        {
            curTime = 0f;

            // �÷��̾� ���� (������ ���� �ִϸ��̼� �Ǵ� ���� ����)
            Debug.Log("�÷��̾ �����մϴ�!");
            // ��: ���� �ִϸ��̼� ���� �Ǵ� �÷��̾� ü�� ���� ���� �ൿ�� ���⿡ �߰�
        }
    }

    private void ReturnToInitialPosition()
    {
        // ���Ͱ� �ʱ� ��ġ�� ���ư��� �ൿ
        Vector3 direction = (initialPosition - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        spriteRenderer.flipX = direction.x < 0;
        //player = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<TopDownController>(out var outPlayer))
        {
            player = outPlayer.transform;
        }
    }
}