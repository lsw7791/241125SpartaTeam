using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> monsterPools = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, int> poolSizes = new Dictionary<string, int>();  // �� ���� Ÿ���� �ʱ� Ǯ ũ�� ����
    private const int MAX_POOL_SIZE = 20;  // Ǯ�� �ִ� ũ�� ����

    // Ǯ �ʱ�ȭ (�����Ϳ� �Բ� �ʱ�ȭ)
    public void InitializeMonsterPool(string monsterType, GameObject prefab, int poolSize, int creatureId)
    {
        Queue<GameObject> poolQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);  // ��Ȱ��ȭ ���·� �߰�

            // ���� ������ �ʱ�ȭ (���� creatureId�� �޾Ƽ� �ʱ�ȭ)
            MonsterData monsterData = obj.GetComponent<MonsterData>();
            if (monsterData != null)
            {
                monsterData.Initialize(creatureId);  // ���� creatureId�� ���� ������ �ʱ�ȭ
            }

            poolQueue.Enqueue(obj);
        }

        monsterPools[monsterType] = poolQueue;
        poolSizes[monsterType] = poolSize;  // �ش� ������ Ǯ ũ�� ����
    }

    // ���͸� ��û�ϴ� �Լ� (��ġ �Ű����� �߰�)
    public GameObject GetMonster(string monsterType, Vector2 position)
    {
        if (monsterPools.ContainsKey(monsterType) && monsterPools[monsterType].Count > 0)
        {
            GameObject monsterObj = monsterPools[monsterType].Dequeue();
            monsterObj.SetActive(true);  // Ȱ��ȭ

            // ������ �����Ϳ� ���� �ʱ�ȭ (Ǯ���� ������ �� ����)
            MonsterData monsterData = monsterObj.GetComponent<MonsterData>();
            if (monsterData != null)
            {
                monsterData.ResetStatus();  // Ǯ���� ��ȯ�� ���� ���� ����
            }

            monsterObj.transform.position = position;  // ��ġ ����

            return monsterObj;
        }
        else
        {
            // Ǯ�� ���Ͱ� ����, �ִ� Ǯ ũ�⸦ �ʰ����� �ʴ� ��쿡�� ���ο� ���� ����
            if (!monsterPools.ContainsKey(monsterType) || monsterPools[monsterType].Count < MAX_POOL_SIZE)
            {
                GameObject newMonster = Instantiate(Resources.Load(monsterType) as GameObject);
                newMonster.transform.position = position;  // ��ġ ����

                return newMonster;
            }
            else
            {
                Debug.LogWarning("Max pool size reached. Unable to create new monster.");
                return null;  // Ǯ ũ�� �ʰ� �� null�� ��ȯ
            }
        }
    }

    // ���͸� Ǯ�� ��ȯ�ϴ� �Լ�
    public void ReturnMonster(string monsterType, GameObject monster)
    {
        monster.SetActive(false);  // ��Ȱ��ȭ

        // ������ ���� ����
        MonsterData monsterData = monster.GetComponent<MonsterData>();
        if (monsterData != null)
        {
            monsterData.ResetStatus();  // ���� ���� (currentHealth, isDie)
        }

        // Ǯ�� ��ȯ
        if (monsterPools.ContainsKey(monsterType))
        {
            monsterPools[monsterType].Enqueue(monster);
        }
        else
        {
            // Ǯ�� �ش� ���� Ÿ���� ���ٸ� ���� ó�� (Ǯ�� �߰��ϰų�, ���ο� Ǯ ���� ����)
            Debug.LogWarning($"Monster pool for type {monsterType} does not exist.");
        }
    }
}