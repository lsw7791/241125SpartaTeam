using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private Monster monster;

    [Header("Targeting")]
    [SerializeField]
    private float moveSpeed;           // ������ �̵� �ӵ�
    public float detectionRange;     // �÷��̾ ������ �� �ִ� ����
    public float attackRange;       // ���� ����
    [SerializeField]
    private Transform player;               // ������ �÷��̾�
    private Vector3 initialPosition;       // ������ �ʱ� ��ġ
    private SpriteRenderer spriteRenderer;

    [Header("Attack")]
    [SerializeField]
    private float damage;
    public float delayTime = 2f;
    public float curTime = 0f;

    [Header("Condition")]
    [SerializeField]
    private int health;
    private bool isDie = false;

    private void Start()
    {
        // ������ �ʱ� ��ġ ����
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        monster = GetComponent<Monster>();

        Initialize();
    }

    private void Update()
    {
        Dead();

        if(isDie == true)
        {
            return;
        }

        if (player == null)
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
            if(player != null)
            {
                player = null;
                Debug.Log("�߰� ����");
            }
        }
    }

    private void Initialize()
    {
        moveSpeed = monster.Speed;
        damage = monster.Damage;
        detectionRange = monster.DetectionRange;
        attackRange = monster.AttackRange;
        health = monster.Health;
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

    private void Dead()
    {
        if(health  <= 0)
        {
            //CreateManager.Instance.ReturnObjectToPool(gameObject);
            gameObject.SetActive(false);
            isDie = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<TopDownController>(out var outPlayer))
        {
            player = outPlayer.transform;
        }
    }
}