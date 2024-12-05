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
    // ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
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
            // ������ ������ �����ؼ� Ǯ�� ��ȯ (creatureId�� ����)
            GameManager.Instance.monsterPool.ReturnMonster(id, gameObject);
        }
        else
        {
            Debug.LogWarning("MonsterPool is not found.");
        }

        isDie = true;  // ���� ���� ó��
        DropItems();  // ������ ��� �Լ� ȣ��
    }

    // �������� ���� �� ȣ��Ǵ� �Լ�
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();  // ü���� 0 ���ϰ� �Ǹ� ���� ó��
        }
        else
        {
            Debug.Log($"Monster {DataManager.Instance.creature.GetName(id)} remaining health: {currentHealth}");
        }
    }

    // ���Ͱ� ���� �� ȣ��Ǵ� ��� ������ ���� �Լ�
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
}
