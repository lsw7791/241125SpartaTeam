using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;

public class MonsterPool : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> monsterPools = new Dictionary<string, Queue<GameObject>>();

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
    }

    // ���͸� ��û�ϴ� �Լ�
    public GameObject GetMonster(string monsterType)
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

            return monsterObj;
        }
        else
        {
            // Ǯ�� ���Ͱ� ������ ���� ���� (���� ����ȭ�� ���� �ּ����� �ε带 ����� �� ����)
            GameObject newMonster = Instantiate(Resources.Load(monsterType) as GameObject);
            return newMonster;
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