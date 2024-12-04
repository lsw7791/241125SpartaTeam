using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    MonsterData monsterData;  // 몬스터 데이터

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
        DropItems();  // 아이템 드랍 함수 호출
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

    // 몬스터가 죽을 때 호출되는 드랍 아이템 생성 함수
    public void DropItems()
    {
        Debug.Log($"Monster {monsterData.creatureName} is dropping items.");

        foreach (int itemId in monsterData.creaturedropItemIds)
        {
            Debug.Log($"Attempting to get item data for ID: {itemId}");

            var itemData = DataManager.Instance.GetItemDataById(itemId);

            if (itemData != null)
            {
                Debug.Log($"Found item data for ID {itemId}: {itemData.name}");
                Debug.Log($"Item prefab path: {itemData.prefabsPath}");

                // 아이템 프리팹 로드
                GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Items/Ladder");
                if (itemPrefab != null)
                {
                    Debug.Log($"Instantiating item prefab at {itemData.prefabsPath}");
                    GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);

                    // 아이템 스프라이트 로드
                    Sprite itemSprite = Resources.Load<Sprite>(itemData.spritePath);
                    if (itemSprite == null)
                    {
                        Debug.LogWarning($"Sprite not found at {itemData.spritePath}, using default sprite.");
                    }

                    // 아이템 데이터 설정
                    TestItem testItem = item.GetComponent<TestItem>();
                    if (testItem != null)
                    {
                        testItem.SetData(itemData, itemSprite);  // 아이템 데이터와 스프라이트 설정
                    }
                }
                else
                {
                    Debug.LogError($"Item prefab not found for item ID {itemId} at path {itemData.prefabsPath}");
                }
            }
            else
            {
                Debug.LogWarning($"No item data found for item ID {itemId}");
            }
        }
    }
}
