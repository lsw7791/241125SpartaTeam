using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateManager : MonoSingleton<CreateManager>
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] GameObject minealPrefab;

    // 몬스터와 미네랄을 위한 객체 풀
    private Queue<GameObject> monsterPool;
    private Queue<GameObject> minealPool;
    GameObject player;

    // 몬스터와 미네랄 생성 최대 개수
    [SerializeField] private int maxMonsterCount = 10;
    [SerializeField] private int maxMineralCount = 10;

    private void Awake()
    {
        // 프리팹 로드
        playerPrefab = Resources.Load<GameObject>("Prefabs/TestPlayer");
        monsterPrefab = Resources.Load<GameObject>("Prefabs/TestMonster");
        minealPrefab = Resources.Load<GameObject>("Prefabs/TestMineral");
    }

    private void Start()
    {
        // 객체 풀 초기화
        monsterPool = new Queue<GameObject>();
        minealPool = new Queue<GameObject>();
        player = new GameObject();
        // 풀 초기화 (미리 객체를 생성하여 풀에 넣음)
        InitializePool(monsterPool, monsterPrefab, maxMonsterCount);
        InitializePool(minealPool, minealPrefab, maxMineralCount);
        player = InitializeObject(playerPrefab);
        player.SetActive(true);

        GameObject monster = GetMonster(Vector3.zero);
    }

    // 객체 풀 초기화
    private GameObject InitializeObject(GameObject prefab)
    {    
            GameObject obj = Instantiate(prefab);
            DontDestroyOnLoad(obj);
            obj.SetActive(false); // 비활성화하여 풀에 넣기
           return obj;
    }
    private void InitializePool(Queue<GameObject> pool, GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab);
            DontDestroyOnLoad(obj);
            obj.SetActive(false); // 비활성화하여 풀에 넣기
            pool.Enqueue(obj);
        }
    }

    // 몬스터 객체 풀에서 꺼내기
    public GameObject GetMonster(Vector3 position)
    {
        return GetObjectFromPool(monsterPool, monsterPrefab, position);
    }

    // 미네랄 객체 풀에서 꺼내기
    public GameObject GetMineral(Vector3 position)
    {
        return GetObjectFromPool(minealPool, minealPrefab, position);
    }

    // 풀에서 객체를 꺼내는 공통 함수
    private GameObject GetObjectFromPool(Queue<GameObject> pool, GameObject prefab, Vector3 position)
    {
        GameObject obj;

        if (pool.Count > 0)
        {
            // 풀에서 꺼낸 객체 활성화
            obj = pool.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            // 풀에 객체가 없으면 새로 생성
            obj = Instantiate(prefab);
        }

        // 위치 설정
        obj.transform.position = position;

        return obj;
    }

    // 풀에 객체 반환
    public void ReturnObjectToPool(GameObject obj, Queue<GameObject> pool)
    {
        obj.SetActive(false); // 비활성화
        pool.Enqueue(obj);     // 풀에 반환
    }
}