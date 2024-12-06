using UnityEngine;

// 몬스터 상태를 Enum으로 관리
public enum MonsterState
{
    Idle,        // 대기
    Chasing,     // 추적 중
    Attacking,   // 공격 중
    Returning   // 초기 위치로 돌아감
}

public class MonsterAI : MonoBehaviour
{
    protected Monster monster;
    protected Rigidbody2D rb;

    [Header("Targeting")]
    [SerializeField]
    protected Transform playerTransform;                 // 추적할 플레이어
    protected Vector3 initialPosition;                   // 몬스터의 초기 위치
    protected SpriteRenderer spriteRenderer;
    protected bool isMove = true;
    [SerializeField]
    protected LayerMask enemyLayer; // 적군 레이어마스크

    [Header("Attack")]
    [SerializeField]
    protected float curTime = 0f;                         // 공격 쿨타임

    protected MonsterState currentState = MonsterState.Idle;  // 현재 상태


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
        // 몬스터가 초기 위치로 돌아가는 행동
        Vector3 direction = (initialPosition - transform.position).normalized;

        // 몬스터가 초기 위치에 거의 도달한 경우, 이동을 멈추고 방향 반전 안함
        if (Vector3.Distance(transform.position, initialPosition) > 0.1f)  // 0.1f는 거의 도달했다고 판단할 수 있는 최소 거리
        {
            transform.Translate(direction * DataManager.Instance.creature.GetMoveSpeed(monster.id) * Time.deltaTime, Space.World);
            spriteRenderer.flipX = direction.x < 0;  // 이동 중에만 방향을 바꿈
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
        if (collision.CompareTag("Weapon")) // 근거리
        {
            monster.TakeDamage(GameManager.Instance.player.stats.Damage); // 데미지 처리
        }
    }

    //private void Attack(Player inPlayer)
    //{
    //    // 몬스터가 플레이어에게 데미지를 줄 때
    //    int damage = DataManager.Instance.creature.GetAttack(monster.id);
    //    Debug.Log($"Monster dealt {damage} damage to {inPlayer.PlayerNickName}");
    //    inPlayer.TakeDamage(damage);  // 데미지 처리
    //}
}
