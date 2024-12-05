using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Monster : MonoBehaviour, ICreature
{
    [SerializeField]private int currentHealth;
    [SerializeField]private bool isDie;
    [SerializeField] public int id { get; private set; }
    BoxCollider2D boxCollider2D;
    // 몬스터가 죽었을 때 호출되는 함수
    public void SetMonsterComponent(int value)
    {
        id = value;
        boxCollider2D = GetComponent<BoxCollider2D>();
        //boxCollider2D.offset = new Vector2(, (int)value);
        ResetStatus();

    }
    public void Die()
    {
        if (GameManager.Instance.monsterPool != null)
        {
            // 몬스터의 종류를 구분해서 풀에 반환 (creatureId로 구별)
            GameManager.Instance.monsterPool.ReturnMonster(id, gameObject);
        }
        else
        {
            Debug.LogWarning("MonsterPool is not found.");
        }

        isDie = true;  // 몬스터 죽음 처리
        DropItems();  // 아이템 드랍 함수 호출
    }

    // 데미지를 입을 때 호출되는 함수
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();  // 체력이 0 이하가 되면 죽음 처리
        }
        else
        {
            Debug.Log($"Monster {DataManager.Instance.creature.GetName(id)} remaining health: {currentHealth}");
        }
    }

    // 몬스터가 죽을 때 호출되는 드랍 아이템 생성 함수
    public void DropItems()
    {
        Debug.Log($"Monster {DataManager.Instance.creature.GetName(id)} is dropping items.");

        foreach (int itemId in DataManager.Instance.creature.GetDropItemIds(id))
        {
            Debug.Log($"Attempting to get item data for ID: {itemId}");

            var itemData = DataManager.Instance.GetItemDataById(itemId);

            if (itemData != null)
            {
                Debug.Log($"Found item data for ID {itemId}: {itemData.name}");
                Debug.Log($"Item prefab path: {itemData.prefabsPath}");

                // 아이템 프리팹 로드
                GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabsPath);
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
    public void ResetStatus()
    {
        currentHealth = DataManager.Instance.creature.GetHealth(id);  // 최대 체력으로 리셋
        isDie = false;  // 죽지 않은 상태로 리셋
    }
}
