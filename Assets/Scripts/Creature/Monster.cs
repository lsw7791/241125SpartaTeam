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
    // ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
    public void SetComponent(int value)
    {
        id = value;
        ResetStatus();// ���� �ʱ�ȭ
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
            Debug.Log($"Monster {GameManager.Instance.dataManager.creature.GetName(id)} remaining health: {currentHealth}");
        }
    }

    // ���Ͱ� ���� �� ȣ��Ǵ� ��� ������ ���� �Լ�
    public void DropItems()
    {
        // ��� ��ġ�� ���� ���� ���� (x, z �� ����)
        float dropRange = 1.5f;

        foreach (int itemId in GameManager.Instance.dataManager.creature.GetDropItemIds(id))
        {
            // ������ �����Ϳ� ������ �ε�
            var itemData = GameManager.Instance.dataManager.GetItemDataById(itemId);
            GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabPath);

            // ������ ��� ��ġ ����
            Vector3 randomOffset = new Vector3(
                Random.Range(-dropRange, dropRange),
                Random.Range(-dropRange, dropRange),
                transform.position.z
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
        currentHealth = GameManager.Instance.dataManager.creature.GetHealth(id);  // �ִ� ü������ ����
        isDie = false;  // ���� ���� ���·� ����
    }
}
