using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    MonsterData monsterData;  // 몬스터 데이터

    [SerializeField] GameObject[] dropItem;

    private void Awake()
    {
        monsterData = GetComponent<MonsterData>();  // MonsterData 컴포넌트 가져오기
    }

    // 몬스터가 죽었을 때 호출되는 함수
    public void Die()
    {
        if (GameManager.Instance.monsterPool != null)
        {
            // 몬스터의 종류를 구분해서 풀에 반환 (creatureId로 구별)
            GameManager.Instance.monsterPool.ReturnMonster(monsterData.id, gameObject);
        }
        else
        {
            Debug.LogWarning("MonsterPool is not found.");
        }

        monsterData.isDie = true;  // 몬스터 죽음 처리
        Debug.Log($"Monster {monsterData.creatureName} has died.");
        Drop();
    }

    // 데미지를 입을 때 호출되는 함수
    public void TakeDamage(int damage)
    {
        Debug.Log($"Monster {monsterData.creatureName} takes {damage} damage.");
        monsterData.currentHealth -= damage;

        if (monsterData.currentHealth <= 0)
        {
            Debug.Log($"Monster {monsterData.creatureName} has been defeated.");
            Die();  // 체력이 0 이하가 되면 죽음 처리
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
