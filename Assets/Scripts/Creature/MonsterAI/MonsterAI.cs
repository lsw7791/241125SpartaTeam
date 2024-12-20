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
    protected LayerMask layerMask; // ���� ���̾��ũ
    protected Transform _monsterPosition;

    [Header("Attack")]
    [SerializeField]
    protected float curTime = 0f;                         // ���� ��Ÿ��

    protected MonsterState currentState = MonsterState.Idle;  // ���� ����
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        monster = GetComponentInParent<Monster>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
        playerTransform = GameManager.Instance.Player.gameObject.transform;
        _monsterPosition = monster.transform;

    }

    protected virtual void FixedUpdate()
    {
        if(GameManager.Instance.Player.stats.isDie)
        {
            playerTransform = null;
        }
        if(playerTransform==null)
        {
            playerTransform = GameManager.Instance.Player.gameObject.transform;
        }
    }
    protected virtual void AttackPlayer()
    {
        curTime += Time.deltaTime;
    }
    protected int AttackMinusDef()
    {
        int value = GameManager.Instance.DataManager.Creature.GetAttack(monster.id) - GameManager.Instance.Player.stats.Def;
        if(value < 0)
        return 0;
        else return value;
    }
    protected void ReturnToInitialPosition()
    {   
            // ���Ͱ� �ʱ� ��ġ�� ���ư��� �ൿ
            Vector3 direction = (initialPosition - _monsterPosition.position).normalized;

            // ���Ͱ� �ʱ� ��ġ�� ���� ������ ���, �̵��� ���߰� ���� ���� ����
            if (Vector3.Distance(_monsterPosition.position, initialPosition) > 0.1f)  // 0.1f�� ���� �����ߴٰ� �Ǵ��� �� �ִ� �ּ� �Ÿ�
            {
            _monsterPosition.Translate(direction * GameManager.Instance.DataManager.Creature.GetMoveSpeed(monster.id) * Time.deltaTime, Space.World);
                spriteRenderer.flipX = direction.x < 0;  // �̵� �߿��� ������ �ٲ�
            }
    }

    protected void ChasePlayer()
    {
        if (isMove)
        {
            Vector3 direction = (playerTransform.position - _monsterPosition.position).normalized;
            _monsterPosition.Translate(direction * GameManager.Instance.DataManager.Creature.GetMoveSpeed(monster.id) * Time.deltaTime, Space.World);

            spriteRenderer.flipX = direction.x <= 0;
        }
    }

    //private  void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Weapon")) // �ٰŸ�
    //    {
    //        monster.TakeDamage(GameManager.Instance.Player.stats.Damage+ GameManager.Instance.Player.stats.PhysicalDamage); // ������ ó��
    //    }
    //}

    //private void Attack(Player inPlayer)
    //{
    //    // ���Ͱ� �÷��̾�� �������� �� ��
    //    int damage = DataManager.Instance.creature.GetAttack(monster.id);
    //    Debug.Log($"Monster dealt {damage} damage to {inPlayer.PlayerNickName}");
    //    inPlayer.TakeDamage(damage);  // ������ ó��
    //}
}
