using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    // creatureId�� Ű�� ����Ͽ� Ǯ�� ����
    private Dictionary<int, Queue<GameObject>> monsterPools = new Dictionary<int, Queue<GameObject>>();
    private Dictionary<int, int> poolSizes = new Dictionary<int, int>();  // �� ������ creatureId�� �������� �ʱ� Ǯ ũ�� ����
    private const int MAX_POOL_SIZE = 20;  // Ǯ�� �ִ� ũ�� ����
    // Ǯ �ʱ�ȭ (creatureId�� �����͸� �Բ� �ʱ�ȭ)
    public GameObject InitializeMine(int creatureId, Vector2 position)
    {
        if(creatureId <13) return null;// ���� ��ȯ ���ϰ� ����
            GameObject minePrefab = Resources.Load<GameObject>(GameManager.Instance.dataManager.creature.GetPrefabsPath(creatureId));
            GameObject obj = Instantiate(minePrefab);
            Mine mine = obj.GetComponent<Mine>();
            mine.SetComponent(creatureId);// ���� ��� ������ �ʱ�ȭ
            obj.transform.position = position;
        return obj;
    }
    public void InitializeMonsterPool(int creatureId, int poolSize)
    {      
        Queue<GameObject> poolQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Monster"));
            obj.SetActive(false);  // ��Ȱ��ȭ ���·� �߰�
            // ���� ������ �ʱ�ȭ (creatureId�� �޾Ƽ� �ʱ�ȭ)
            Monster monster = obj.GetComponent<Monster>();
            if (monster != null)
            {
                GameObject childPrefab = Resources.Load<GameObject>(GameManager.Instance.dataManager.creature.GetPrefabsPath(creatureId));
                GameObject child = Instantiate(childPrefab);
                child.transform.SetParent(obj.transform);  // obj�� �θ� �ǵ��� ����
                child.transform.localPosition = Vector3.zero;  // �ڽ��� ��ġ�� �θ��� (0, 0, 0)���� ���� (�ʿ�� ����)
                monster.SetComponent(creatureId);// ���� ��� ������ �ʱ�ȭ

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
            Monster monster = monsterObj.GetComponent<Monster>();
            if (monster != null)
            {

                monster.ResetStatus();  // Ǯ���� ��ȯ�� ���� ���� ����
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
    public void ReturnMonster(int creatureId, GameObject thisObject)
    {
        thisObject.SetActive(false);  // ��Ȱ��ȭ

        // ������ ���� ����
        Monster monster = thisObject.GetComponent<Monster>();
        if (thisObject.GetComponent<Monster>() != null)
        {
            monster.ResetStatus();  // ���� ���� (currentHealth, isDie)
        }

        // Ǯ�� ��ȯ
        if (monsterPools.ContainsKey(creatureId))
        {
            monsterPools[creatureId].Enqueue(thisObject);
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
        GameObject childPrefab = Resources.Load<GameObject>(GameManager.Instance.dataManager.creature.GetPrefabsPath(creatureId));
        return childPrefab;
    }
}