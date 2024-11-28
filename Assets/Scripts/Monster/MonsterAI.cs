using System;
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
    private int damage;
    public float delayTime = 2f;
    public float curTime = 0f;

    public event Action<int> OnAttackComend;

    private void Start()
    {
        // ������ �ʱ� ��ġ ����
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        monster = GetComponent<Monster>();

        Initialize();
    }

    private void Update()
    {
        if(monster.isDie == true)
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

        if (distanceToPlayer <= detectionRange)
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
            //OnAttackComend?.Invoke(damage);
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