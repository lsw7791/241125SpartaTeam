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
            GameObject minePrefab = Resources.Load<GameObject>(GameManager.Instance.DataManager.Creature.GetPrefabsPath(creatureId));
            GameObject obj = Instantiate(minePrefab, transform);
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
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Monster"), transform);
            obj.SetActive(false);  // ��Ȱ��ȭ ���·� �߰�
            // ���� ������ �ʱ�ȭ (creatureId�� �޾Ƽ� �ʱ�ȭ)
            Monster monster = obj.GetComponent<Monster>();
            if (monster != null)
            {
                GameObject childPrefab = Resources.Load<GameObject>(GameManager.Instance.DataManager.Creature.GetPrefabsPath(creatureId));
                GameObject child = Instantiate(childPrefab, transform);
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
            // Ǯ���� ���͸� ������
            GameObject obj = monsterPools[creatureId].Dequeue();
            obj.SetActive(true); // Ȱ��ȭ

            // ������ ������ �ʱ�ȭ
            Monster monster = obj.GetComponent<Monster>();
            if (monster != null)
            {
                monster.ResetStatus(); // Ǯ���� ������ ������ ���� �ʱ�ȭ
            }

            obj.transform.position = position; // ��ġ ����
            return obj; // Ȱ��ȭ�� ���� ��ȯ
        }
        else
        {
            // Ǯ�� ���� ���Ͱ� ���� ��� ���ο� ���� ����
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Monster"), transform);
            obj.transform.position = position; // ��ġ ����
            obj.SetActive(true); // Ȱ��ȭ

            // ���� ������ �ʱ�ȭ
            Monster monster = obj.GetComponent<Monster>();
            if (monster != null)
            {
                // �ڽ� ������ �ε�
                GameObject childPrefab = Resources.Load<GameObject>(GameManager.Instance.DataManager.Creature.GetPrefabsPath(creatureId));
                if (childPrefab != null)
                {
                    GameObject child = Instantiate(childPrefab, transform); // �ڽ� ������ ����
                    child.transform.SetParent(obj.transform); // obj�� �θ�� ����
                    child.transform.localPosition = Vector3.zero; // �ڽ� ��ġ�� �θ� �߽ɿ� ����
                }

                monster.SetComponent(creatureId); // ���� ������ �ʱ�ȭ
            }

            return obj; // ������ ���� ��ȯ
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
        GameObject childPrefab = Resources.Load<GameObject>(GameManager.Instance.DataManager.Creature.GetPrefabsPath(creatureId));
        return childPrefab;
    }
}