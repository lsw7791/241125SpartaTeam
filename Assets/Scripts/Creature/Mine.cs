using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, ICreature
{
    
    GameManager gameManager;   // 게임 매니저
    MineFull minefull;
    [SerializeField] private int currentHealth;
    [SerializeField] private bool isDie;
    [SerializeField] public int id;

    private void Awake()
    {
        gameManager = GameManager.Instance;  // 게임 매니저 인스턴스 가져오기
        minefull = GetComponentInChildren<MineFull>();
    }
    private void Start()
    {
        // 30초마다 ObjectSetActive 호출
        StartCoroutine(ActivateObjectEvery30Seconds());
    }
    private IEnumerator ActivateObjectEvery30Seconds()
    {
        while (true)
        {
            // 30초 기다린 후
            yield return new WaitForSeconds(30f);

            // SetActive 호출
            if (isDie == true)
            {
                minefull.ObjectSetActive(true);
                isDie = false;
                ResetStatus();
            }
        }
    }
    public void TakeDamage(int damage)
    {
        Debug.Log($"Monster {DataManager.Instance.creature.GetName(id)} takes {damage} damage.");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log($"Monster {DataManager.Instance.creature.GetName(id)} has been defeated.");
            Die();  // 체력이 0 이하가 되면 죽음 처리
        }
        else
        {
            Debug.Log($"Monster {DataManager.Instance.creature.GetName(id)} remaining health: {currentHealth}");
        }
    }
    public void Die()
    {
        if (SpawnManager.Instance.monsterPool != null)
        {
            // 몬스터의 종류를 구분해서 풀에 반환 (creatureId로 구별)
            SpawnManager.Instance.monsterPool.ReturnMonster(id, gameObject);
        }
        else
        {
            Debug.LogWarning("MonsterPool is not found.");
        }

        isDie = true;  // 몬스터 죽음 처리
        minefull.ObjectSetActive(false);

        Debug.Log($"Monster {DataManager.Instance.creature.GetName(id)} has died.");
        DropItems();  // 아이템 드랍 함수 호출
    }
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
    public void SetComponent(int value)
    {
        id = value;
        ResetStatus();// 스텟 초기화
    }
}
