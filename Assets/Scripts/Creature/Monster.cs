using UnityEngine;

public class Monster : MonoBehaviour, IDamageable
{
    MonsterData monsterData;  // ���� ������

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
        DropItems();  // ������ ��� �Լ� ȣ��
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

    // ���Ͱ� ���� �� ȣ��Ǵ� ��� ������ ���� �Լ�
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

                GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabsPath);

                if (itemPrefab != null)
                {
                    Debug.Log($"Instantiating item prefab at {itemData.prefabsPath}");
                    Instantiate(itemPrefab, transform.position, Quaternion.identity);
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
