using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Monster : MonoBehaviour, ICreature
{
    public int currentHealth { get; private set; }
   // private bool _isDie;
    [SerializeField] public int id { get; private set; }
    MonsterPool monsterPool;
    public Vector2 DiePosition;
    MonsterHealthBarUI monsterHealthBarUI;
    // ���Ͱ� �׾��� �� ȣ��Ǵ� �Լ�
    private void Start()
    {
        monsterPool = GetComponentInParent<MonsterPool>();
        monsterHealthBarUI = GetComponent<MonsterHealthBarUI>();
    }
    public void SetComponent(int value)
    {
        id = value;
        ResetStatus();// ���� �ʱ�ȭ
    }
    public void Die()
    {
        DiePosition = gameObject.transform.position;

        if (monsterPool != null)
        {
            // ������ ������ �����ؼ� Ǯ�� ��ȯ (creatureId�� ����)
            monsterPool.ReturnMonster(id, gameObject);
        }
        else
        {
            Debug.LogWarning("MonsterPool is not found.");
        }

       // _isDie = true;  // ���� ���� ó��
        DropItems();  // ������ ��� �Լ� ȣ��
    }

    // �������� ���� �� ȣ��Ǵ� �Լ�
    public void TakeDamage(int damage)
    {
        int value = GameManager.Instance.DataManager.Creature.GetDefense(id);
        value -= damage;
        if (value < 0)
        {
            currentHealth += value;
            //�̹��� ü�� ������Ʈ
            monsterHealthBarUI.UpdateHealthBar();
            if (currentHealth <= 0)
            {
                Die();  // ü���� 0 ���ϰ� �Ǹ� ���� ó��
            }
            else
            {
                Debug.Log($"Monster {GameManager.Instance.DataManager.Creature.GetName(id)} remaining health: {currentHealth}");
            }
        }     
    }

    // ���Ͱ� ���� �� ȣ��Ǵ� ��� ������ ���� �Լ�
    public void DropItems()
    {
        float dropRange = 0.5f; // ��� ��ġ�� ���� ���� ����

        foreach (int itemId in GameManager.Instance.DataManager.Creature.GetDropItemIds(id))
        {
            var itemData = GameManager.Instance.DataManager.GetItemDataById(itemId);

            // ������ �����Ϳ� ������ �ε�
            GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabPath);

            Vector2 randomOffset = new Vector3(
                Random.Range(-dropRange, dropRange),
                Random.Range(-dropRange, dropRange)
            );
            Vector2 dropPosition = DiePosition + randomOffset ;

            GameObject item = Instantiate(itemPrefab, dropPosition, Quaternion.identity);

            TestItem testItem = item.GetComponent<TestItem>();

            if (itemData.itemType == ItemType.Gold && itemData.goldRange != null && itemData.goldRange.Count == 2)
            {
                int minGold = itemData.goldRange[0]; // �ּ� ���
                int maxGold = itemData.goldRange[1]; // �ִ� ���
                testItem.SetData(itemData, minGold, maxGold);
            }
            else
            {
                testItem.SetData(itemData);
            }
        }
    }


    public void ResetStatus()
    {
        currentHealth = GameManager.Instance.DataManager.Creature.GetHealth(id);  // �ִ� ü������ ����
       // _isDie = false;  // ���� ���� ���·� ����
    }
}
