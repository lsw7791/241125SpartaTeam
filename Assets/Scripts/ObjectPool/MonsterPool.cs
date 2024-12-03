using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    // creatureId�� Ű�� ����Ͽ� Ǯ�� ����
    private Dictionary<int, Queue<GameObject>> monsterPools = new Dictionary<int, Queue<GameObject>>();
    private Dictionary<int, int> poolSizes = new Dictionary<int, int>();  // �� ������ creatureId�� �������� �ʱ� Ǯ ũ�� ����
    private const int MAX_POOL_SIZE = 20;  // Ǯ�� �ִ� ũ�� ����
    private GameManager gameManager;
    // Ǯ �ʱ�ȭ (creatureId�� �����͸� �Բ� �ʱ�ȭ)
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    public void InitializeMonsterPool(int creatureId, GameObject prefab, int poolSize)
    {
        Queue<GameObject> poolQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);  // ��Ȱ��ȭ ���·� �߰�

            // ���� ������ �ʱ�ȭ (creatureId�� �޾Ƽ� �ʱ�ȭ)
            MonsterData monsterData = obj.GetComponent<MonsterData>();
            if (monsterData != null)
            {
                monsterData.Initialize(creatureId);  // creatureId�� ���� ������ �ʱ�ȭ
            }

            poolQueue.Enqueue(obj);
        }

        monsterPools[creatureId] = poolQueue;
        poolSizes[creatureId] = poolSize;  // �ش� creatureId�� Ǯ ũ�� ����
    }

    // ���͸� ��û�ϴ� �Լ� (creatureId�� �������� Ǯ���� ��������)
    public GameObject GetMonster(int creatureId, Vector2 position)
    {
        if (monsterPools.ContainsKey(creatureId) && monsterPools[creatureId].Count > 0)
        {
            GameObject monsterObj = monsterPools[creatureId].Dequeue();
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
            if (!monsterPools.ContainsKey(creatureId) || monsterPools[creatureId].Count < MAX_POOL_SIZE)
            {
                GameObject prefab = GetPrefabByCreatureId(creatureId);  // creatureId�� �������� ������
                if (prefab != null)
                {
                    GameObject newMonster = Instantiate(prefab);
                    newMonster.transform.position = position;  // ��ġ ����

                    return newMonster;
                }
                else
                {
                    Debug.LogWarning("No prefab found for creatureId: " + creatureId);
                    return null;
                }
            }
            else
            {
                Debug.LogWarning("Max pool size reached for creatureId: " + creatureId);
                return null;  // Ǯ ũ�� �ʰ� �� null�� ��ȯ
            }
        }
    }

    // ���͸� Ǯ�� ��ȯ�ϴ� �Լ� (creatureId ����)
    public void ReturnMonster(int creatureId, GameObject monster)
    {
        monster.SetActive(false);  // ��Ȱ��ȭ

        // ������ ���� ����
        MonsterData monsterData = monster.GetComponent<MonsterData>();
        if (monsterData != null)
        {
            monsterData.ResetStatus();  // ���� ���� (currentHealth, isDie)
        }

        // Ǯ�� ��ȯ
        if (monsterPools.ContainsKey(creatureId))
        {
            monsterPools[creatureId].Enqueue(monster);
        }
        else
        {
            // Ǯ�� �ش� creatureId�� ���ٸ� ���� ó�� (Ǯ�� �߰��ϰų�, ���ο� Ǯ ���� ����)
            Debug.LogWarning($"Monster pool for creatureId {creatureId} does not exist.");
        }
    }

    // creatureId�� �ش��ϴ� �������� �������� �Լ�
    private GameObject GetPrefabByCreatureId(int creatureId)
    {
        switch (creatureId)
        {
            case 1: return gameManager.goblinPrefab;
            case 2: return gameManager.zombiePrefab;
            case 3: return gameManager.ImpPrefab;
            case 4: return gameManager.lizardPrefab;
            case 5: return gameManager.orcShamanPrefab;
            case 6: return gameManager.BigzombiePrefab;
            case 7: return gameManager.skeletPrefab;
            case 8: return gameManager.iceZombiePrefab;
            case 9: return gameManager.ogrePrefab;
            case 10: return gameManager.knightPrefab;
            case 11: return gameManager.necromancerPrefab;
            case 12: return gameManager.demonPrefab;
            default: return null;  // �ش��ϴ� creatureId�� ���� ��� null ��ȯ
        }
    }
}