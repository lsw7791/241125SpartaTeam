using UnityEngine;

// ���� ���¸� Enum���� ����
public enum MonsterState
{
    Idle,        // ���
    Chasing,     // ���� ��
    Attacking,   // ���� ��
    Returning,    // �ʱ� ��ġ�� ���ư�
}

public class MonsterAI : MonoBehaviour
{
    protected MonsterData monsterData;

    [Header("Targeting")]
    [SerializeField]
    protected Transform playerTransform;                 // ������ �÷��̾�
    protected Vector3 initialPosition;                   // ������ �ʱ� ��ġ
    protected SpriteRenderer spriteRenderer;

    [Header("Attack")]
    [SerializeField]
    protected float curTime = 0f;                         // ���� ��Ÿ��

    protected MonsterState currentState = MonsterState.Idle;  // ���� ����


    private void Start()
    {
        monsterData = GetComponent<MonsterData>();
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindWithTag("Player")?.transform;
        // ���� ������ �����ٰ� ���� �ʾҴ���?
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
            transform.Translate(direction * monsterData.creatureMoveSpeed * Time.deltaTime, Space.World);
            spriteRenderer.flipX = direction.x < 0;  // �̵� �߿��� ������ �ٲ�
        }
    }

    protected void ChasePlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.Translate(direction * monsterData.creatureMoveSpeed * Time.deltaTime, Space.World);

        spriteRenderer.flipX = direction.x <= 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { // �� ��� ���ϰ� ����
        // �÷��̾�� �浹 �� �÷��̾� ���� ����
        if (collision.TryGetComponent<Player>(out var outPlayer))
        {
            playerTransform = collision.transform;
            currentState = MonsterState.Chasing;
            // �÷��̾�� ���� �� ���� ���·� ����
        }
    }

    private void Attack(Player inPlayer)
    {
        // ���Ͱ� �÷��̾�� �������� �� ��
        int damage = monsterData.creatureAttack;
        Debug.Log($"Monster dealt {damage} damage to {inPlayer.PlayerNickName}");
        inPlayer.TakeDamage(damage);  // ������ ó��
    }
}
