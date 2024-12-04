using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    MonsterData monsterData;  // ���� ������

    [SerializeField] GameObject[] dropItem;

    private void Awake()
    {
        monsterData = GetComponent<MonsterData>();  // MonsterData ������Ʈ ��������
    }

    // ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
    public void Die()
    {
        if (GameManager.Instance.monsterPool != null)
        {
            // ������ ������ �����ؼ� Ǯ�� ��ȯ (creatureId�� ����)
            GameManager.Instance.monsterPool.ReturnMonster(monsterData.id, gameObject);
        }
        else
        {
            Debug.LogWarning("MonsterPool is not found.");
        }

        monsterData.isDie = true;  // ���� ���� ó��
        Debug.Log($"Monster {monsterData.creatureName} has died.");
        Drop();
    }

    // �������� ���� �� ȣ��Ǵ� �Լ�
    public void TakeDamage(int damage)
    {
        Debug.Log($"Monster {monsterData.creatureName} takes {damage} damage.");
        monsterData.currentHealth -= damage;

        if (monsterData.currentHealth <= 0)
        {
            Debug.Log($"Monster {monsterData.creatureName} has been defeated.");
            Die();  // ü���� 0 ���ϰ� �Ǹ� ���� ó��
        }
        else
        {
            Debug.Log($"Monster {monsterData.creatureName} remaining health: {monsterData.currentHealth}");
        }
    }

    void Drop()
    {
        int randomItemCount = Random.Range( 0, dropItem.Length );

        if (randomItemCount != 0)
        {
            for (int i = 0; i < randomItemCount; i++)
            {
                int randomItem = Random.Range(0, dropItem.Length);

                GameObject itemObject = Instantiate(dropItem[randomItem]);
                if (itemObject.TryGetComponent<TestItem>(out var outItem))
                {
                    outItem.DropPosition(transform.position);
                }

            }
        }
    }
}
