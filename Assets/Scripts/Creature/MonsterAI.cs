using System;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private MonsterData monster;

    [Header("Targeting")]
    [SerializeField]
    private Transform player;               // 추적할 플레이어
    private Vector3 initialPosition;       // 몬스터의 초기 위치
    private SpriteRenderer spriteRenderer;

    [Header("Attack")]
    [SerializeField]
    public float delayTime = 2f;
    public float curTime = 0f;

    public event Action<int> OnAttackComend;

    private void Start()
    {
        monster = GetComponent<MonsterData>();
        // 몬스터의 초기 위치 저장
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        // 몬스터가 플레이어를 감지하는 범위 내에 있다면 추적
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= monster.creatureAttackRange)
        {
            // 공격 범위에 들어왔으면 공격 행동
            AttackPlayer();
        }
        else if (distanceToPlayer <= monster.creatureDetectionRange)
        {
            // 감지 범위에 들어왔으면 플레이어를 추적
            ChasePlayer();
            //Debug.Log("목표 발견");
        }
        else
        {
            // 플레이어가 범위 밖에 있으면 초기 위치로 돌아가기
            ReturnToInitialPosition();
            if(player != null)
            {
                player = null;
                Debug.Log("추격 종료");
            }
        }
    }


    private void ChasePlayer()
    {
        // 플레이어 방향으로 이동
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * monster.creatureMoveSpeed * Time.deltaTime, Space.World);

        spriteRenderer.flipX = direction.x <= 0;
    }

    private void AttackPlayer()
    {
        curTime += Time.deltaTime;
        if (curTime >= delayTime)
        {
            curTime = 0f;

            // 플레이어 공격 (간단한 공격 애니메이션 또는 로직 삽입)
            //OnAttackComend?.Invoke(damage);
            Debug.Log("플레이어를 공격합니다!");
            // 예: 공격 애니메이션 실행 또는 플레이어 체력 감소 등의 행동을 여기에 추가
        }
    }

    private void ReturnToInitialPosition()
    {
        // 몬스터가 초기 위치로 돌아가는 행동
        Vector3 direction = (initialPosition - transform.position).normalized;
        transform.Translate(direction * monster.creatureMoveSpeed * Time.deltaTime, Space.World);
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