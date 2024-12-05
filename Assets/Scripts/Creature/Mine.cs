using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, ICreature
{
    
    GameManager gameManager;   // ���� �Ŵ���
    MineFull minefull;
    [SerializeField] private int currentHealth;
    [SerializeField] private bool isDie;
    [SerializeField] public int id;

    private void Awake()
    {
        gameManager = GameManager.Instance;  // ���� �Ŵ��� �ν��Ͻ� ��������
        minefull = GetComponentInChildren<MineFull>();
    }
    private void Start()
    {
        // 30�ʸ��� ObjectSetActive ȣ��
        StartCoroutine(ActivateObjectEvery30Seconds());
    }
    private IEnumerator ActivateObjectEvery30Seconds()
    {
        while (true)
        {
            // 30�� ��ٸ� ��
            yield return new WaitForSeconds(30f);

            // SetActive ȣ��
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
            Die();  // ü���� 0 ���ϰ� �Ǹ� ���� ó��
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
            // ������ ������ �����ؼ� Ǯ�� ��ȯ (creatureId�� ����)
            SpawnManager.Instance.monsterPool.ReturnMonster(id, gameObject);
        }
        else
        {
            Debug.LogWarning("MonsterPool is not found.");
        }

        isDie = true;  // ���� ���� ó��
        minefull.ObjectSetActive(false);

        Debug.Log($"Monster {DataManager.Instance.creature.GetName(id)} has died.");
        DropItems();  // ������ ��� �Լ� ȣ��
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

                // ������ ������ �ε�
                GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabsPath);
                if (itemPrefab != null)
                {
                    Debug.Log($"Instantiating item prefab at {itemData.prefabsPath}");
                    GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);

                    // ������ ��������Ʈ �ε�
                    Sprite itemSprite = Resources.Load<Sprite>(itemData.spritePath);
                    if (itemSprite == null)
                    {
                        Debug.LogWarning($"Sprite not found at {itemData.spritePath}, using default sprite.");
                    }

                    // ������ ������ ����
                    TestItem testItem = item.GetComponent<TestItem>();
                    if (testItem != null)
                    {
                        testItem.SetData(itemData, itemSprite);  // ������ �����Ϳ� ��������Ʈ ����
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
        currentHealth = DataManager.Instance.creature.GetHealth(id);  // �ִ� ü������ ����
        isDie = false;  // ���� ���� ���·� ����
    }
    public void SetComponent(int value)
    {
        id = value;
        ResetStatus();// ���� �ʱ�ȭ
    }
}
