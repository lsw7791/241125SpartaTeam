using UnityEngine;

// ���� ���¸� Enum���� ����
public enum MonsterState
{
    Idle,        // ���
    Chasing,     // ���� ��
    Attacking,   // ���� ��
    Returning   // �ʱ� ��ġ�� ���ư�
}

public class MonsterAI : MonoBehaviour
{
    protected Monster monster;
    protected Rigidbody2D rb;

    [Header("Targeting")]
    [SerializeField]
    protected Transform playerTransform;                 // ������ �÷��̾�
    protected Vector3 initialPosition;                   // ������ �ʱ� ��ġ
    protected SpriteRenderer spriteRenderer;
    protected bool isMove = true;
    [SerializeField]
    protected LayerMask enemyLayer; // ���� ���̾��ũ

    [Header("Attack")]
    [SerializeField]
    protected float curTime = 0f;                         // ���� ��Ÿ��

    protected MonsterState currentState = MonsterState.Idle;  // ���� ����


    private void Start()
    {rb = GetComponent<Rigidbody2D>();
        monster = GetComponentInParent<Monster>();
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameManager.Instance.player?.transform;
    }

    protected virtual void FixedUpdate()
    {
        if(GameManager.Instance.player._playerAnimationController.isDeath)
        {
            playerTransform = null;
        }
    }

    protected virtual void AttackPlayer()
    {
        curTime += Time.deltaTime;
    }

    protected void ReturnToInitialPosition()
    {
        // ���Ͱ� �ʱ� ��ġ�� ���ư��� �ൿ
        Vector3 direction = (initialPosition - transform.position).normalized;

        // ���Ͱ� �ʱ� ��ġ�� ���� ������ ���, �̵��� ���߰� ���� ���� ����
        if (Vector3.Distance(transform.position, initialPosition) > 0.1f)  // 0.1f�� ���� �����ߴٰ� �Ǵ��� �� �ִ� �ּ� �Ÿ�
        {
            transform.Translate(direction * DataManager.Instance.creature.GetMoveSpeed(monster.id) * Time.deltaTime, Space.World);
            spriteRenderer.flipX = direction.x < 0;  // �̵� �߿��� ������ �ٲ�
        }
    }

    protected void ChasePlayer()
    {
        if (isMove)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * DataManager.Instance.creature.GetMoveSpeed(monster.id) * Time.deltaTime, Space.World);

            spriteRenderer.flipX = direction.x <= 0;
        }
    }

    private  void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon")) // �ٰŸ�
        {
            monster.TakeDamage(GameManager.Instance.player.stats.Damage); // ������ ó��
        }
    }

    //private void Attack(Player inPlayer)
    //{
    //    // ���Ͱ� �÷��̾�� �������� �� ��
    //    int damage = DataManager.Instance.creature.GetAttack(monster.id);
    //    Debug.Log($"Monster dealt {damage} damage to {inPlayer.PlayerNickName}");
    //    inPlayer.TakeDamage(damage);  // ������ ó��
    //}
}
