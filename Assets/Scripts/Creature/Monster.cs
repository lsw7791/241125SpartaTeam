using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Monster : MonoBehaviour, ICreature
{
    [SerializeField]private int currentHealth;
    [SerializeField]private bool isDie;
    [SerializeField] public int id { get; private set; }
    MonsterPool monsterPool;
    // ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
    private void Start()
    {
        monsterPool = GetComponentInParent<MonsterPool>();
    }
    public void SetComponent(int value)
    {
        id = value;
        ResetStatus();// ���� �ʱ�ȭ
    }
    public void Die()
    {
        if (monsterPool != null)
        {
            // ������ ������ �����ؼ� Ǯ�� ��ȯ (creatureId�� ����)
            monsterPool.ReturnMonster(id, gameObject);
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
            Debug.Log($"Monster {GameManager.Instance.DataManager.Creature.GetName(id)} remaining health: {currentHealth}");
        }
    }

    // ���Ͱ� ���� �� ȣ��Ǵ� ��� ������ ���� �Լ�
    // DropItems �޼��� ����
    public void DropItems()
    {
        float dropRange = 1.5f; // ��� ��ġ�� ���� ���� ����

        foreach (int itemId in GameManager.Instance.DataManager.Creature.GetDropItemIds(id))
        {
            var itemData = GameManager.Instance.DataManager.GetItemDataById(itemId);

            // ������ �����Ϳ� ������ �ε�
            GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabPath);

            Vector3 randomOffset = new Vector3(
                Random.Range(-dropRange, dropRange),
                Random.Range(-dropRange, dropRange),
                transform.position.z
            );
            Vector3 dropPosition = transform.position + randomOffset;

            GameObject item = Instantiate(itemPrefab, dropPosition, Quaternion.identity);
            Sprite itemSprite = Resources.Load<Sprite>(itemData.spritePath);

            TestItem testItem = item.GetComponent<TestItem>();

            // goldRange�� List<int>�� ���� ��� ó��
            if (itemData.itemType == ItemType.Gold && itemData.goldRange != null && itemData.goldRange.Count == 2)
            {
                int minGold = itemData.goldRange[0]; // �ּ� ���
                int maxGold = itemData.goldRange[1]; // �ִ� ���
                testItem.SetData(itemData, itemSprite, minGold, maxGold);
            }
            else
            {
                testItem.SetData(itemData, itemSprite);
            }
        }
    }


    public void ResetStatus()
    {
        currentHealth = GameManager.Instance.DataManager.Creature.GetHealth(id);  // �ִ� ü������ ����
        isDie = false;  // ���� ���� ���·� ����
    }
}
