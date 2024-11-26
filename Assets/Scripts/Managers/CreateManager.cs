using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoSingleton<CreateManager>
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] GameObject minealPrefab;

    // ���Ϳ� �̳׶��� ���� ��ü Ǯ
    private Queue<GameObject> monsterPool;
    private Queue<GameObject> minealPool;
    GameObject player;

    // ���Ϳ� �̳׶� ���� �ִ� ����
    [SerializeField] private int maxMonsterCount = 10;
    [SerializeField] private int maxMineralCount = 10;

    private void Awake()
    {
        // ������ �ε�
        playerPrefab = Resources.Load<GameObject>("Prefabs/TestPlayer");
        monsterPrefab = Resources.Load<GameObject>("Prefabs/TestMonster");
        minealPrefab = Resources.Load<GameObject>("Prefabs/TestMineral");
    }

    private void Start()
    {
        // ��ü Ǯ �ʱ�ȭ
        monsterPool = new Queue<GameObject>();
        minealPool = new Queue<GameObject>();
        player = new GameObject();
        // Ǯ �ʱ�ȭ (�̸� ��ü�� �����Ͽ� Ǯ�� ����)
        InitializePool(monsterPool, monsterPrefab, maxMonsterCount);
        InitializePool(minealPool, minealPrefab, maxMineralCount);
        player = InitializeObject(playerPrefab);
        player.SetActive(true);

        GameObject monster = GetMonster(Vector3.zero);
    }

    // ��ü Ǯ �ʱ�ȭ
    private GameObject InitializeObject(GameObject prefab)
    {    
            GameObject obj = Instantiate(prefab);
            DontDestroyOnLoad(obj);
            obj.SetActive(false); // ��Ȱ��ȭ�Ͽ� Ǯ�� �ֱ�
           return obj;
    }
    private void InitializePool(Queue<GameObject> pool, GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab);
            DontDestroyOnLoad(obj);
            obj.SetActive(false); // ��Ȱ��ȭ�Ͽ� Ǯ�� �ֱ�
            pool.Enqueue(obj);
        }
    }

    // ���� ��ü Ǯ���� ������
    public GameObject GetMonster(Vector3 position)
    {
        return GetObjectFromPool(monsterPool, monsterPrefab, position);
    }

    // �̳׶� ��ü Ǯ���� ������
    public GameObject GetMineral(Vector3 position)
    {
        return GetObjectFromPool(minealPool, minealPrefab, position);
    }

    // Ǯ���� ��ü�� ������ ���� �Լ�
    private GameObject GetObjectFromPool(Queue<GameObject> pool, GameObject prefab, Vector3 position)
    {
        GameObject obj;

        if (pool.Count > 0)
        {
            // Ǯ���� ���� ��ü Ȱ��ȭ
            obj = pool.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            // Ǯ�� ��ü�� ������ ���� ����
            obj = Instantiate(prefab);
        }

        // ��ġ ����
        obj.transform.position = position;

        return obj;
    }

    // Ǯ�� ��ü ��ȯ
    public void ReturnObjectToPool(GameObject obj, Queue<GameObject> pool)
    {
        obj.SetActive(false); // ��Ȱ��ȭ
        pool.Enqueue(obj);     // Ǯ�� ��ȯ
    }
}