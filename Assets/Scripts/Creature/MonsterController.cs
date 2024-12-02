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
        //GameManager.Instance.�÷��̾�.�����̺�Ʈ += HitDamage;
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

    // TODO :: TestTrigger // �� Ʈ���Ŵ� ����� �÷��̾� ������ ���� �� ���͸� �����ϰ� 
    // MonsterController�� �ִ��� TryGetComponent�� Ȯ�� �� �̺�Ʈ�� ������ ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TopDownController>(out var outPlayer))
            HitDamage(1);
    }
}
