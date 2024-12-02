using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private MonsterData monster;
    private void Awake()
    {
        monster = GetComponent<MonsterData>();
    }
    private void Start()
    {
        //GameManager.Instance.플레이어.공격이벤트 += HitDamage;
    }

    private void OnEnable()
    {
        monster = GetComponentInParent<MonsterData>();
    }

    public void HitDamage(int damage)
    {
        monster.creatureHealth -= damage;

        if (monster.creatureHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        //CreateManager.Instance.ReturnObjectToPool(gameObject);
        //monster.gameObject.SetActive(false);
        monster.isDie = true;
    }

    // TODO :: TestTrigger // 이 트리거는 지우고 플레이어 측에서 공격 시 몬스터를 참조하고 
    // MonsterController가 있는지 TryGetComponent로 확인 후 이벤트로 데미지 감소
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TopDownController>(out var outPlayer))
            HitDamage(1);
    }
}
