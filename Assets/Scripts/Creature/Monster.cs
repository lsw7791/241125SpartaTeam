using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Monster : MonoBehaviour, ICreature
{
    [SerializeField]private int currentHealth;
    [SerializeField]private bool isDie;
    [SerializeField] public int id { get; private set; }
    // ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
    public void SetMonsterComponent(int value)
    {
        id = value;
        ResetStatus();// ���� �ʱ�ȭ
        GameObject MonsterPrefab = Resources.Load<GameObject>("Prefabs/Monsters/Goblin");
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
        // ��� ��ġ�� ���� ���� ���� (x, z �� ����)
        float dropRange = 1.5f;

        foreach (int itemId in DataManager.Instance.creature.GetDropItemIds(id))
        {
            // ������ �����Ϳ� ������ �ε�
            var itemData = DataManager.Instance.GetItemDataById(itemId);
            GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabsPath);

            // ������ ��� ��ġ ����
            Vector3 randomOffset = new Vector3(
                Random.Range(-dropRange, dropRange),
                0f, // ����(y)�� �״�� ����
                Random.Range(-dropRange, dropRange)
            );
            Vector3 dropPosition = transform.position + randomOffset;

            // ������ �ν��Ͻ� ����
            GameObject item = Instantiate(itemPrefab, dropPosition, Quaternion.identity);

            // ������ ��������Ʈ �ε�
            Sprite itemSprite = Resources.Load<Sprite>(itemData.spritePath);

            // ������ ������ ����
            TestItem testItem = item.GetComponent<TestItem>();
            testItem.SetData(itemData, itemSprite);
        }
    }


    public void ResetStatus()
    {
        currentHealth = DataManager.Instance.creature.GetHealth(id);  // �ִ� ü������ ����
        isDie = false;  // ���� ���� ���·� ����
    }
}
