using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateManager : MonoSingleton<CreateManager>
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject goblinPrefab; // 몬스터 프리팹
    [SerializeField] GameObject minealPrefab;

    // Object Pool 변수들
    private Queue<GameObject> monsterPool;
    private Queue<GameObject> minealPool;
    private GameObject player;

    // Object Pool에 생성할 최대 개수
    [SerializeField] private int maxMonsterCount = 10;
    [SerializeField] private int maxMineralCount = 10;

    // 몬스터 데이터를 담을 리스트
    private List<MonsterData> monsterDataList = new List<MonsterData>();

    protected override void Awake()
    {
        base.Awake();

        // 프리팹 불러오기
        playerPrefab = Resources.Load<GameObject>("Prefabs/TestPlayer");
        minealPrefab = Resources.Load<GameObject>("Prefabs/TestMineral");

        // 몬스터 프리팹을 Resources에서 동적으로 로드
        goblinPrefab = Resources.Load<GameObject>("Prefabs/Monsters/Goblin");  // 예시로 Goblin 프리팹 로드

        // 몬스터 데이터를 DataManager에서 가져옴
        monsterDataList = DataManager.Instance.GetMonsterDataList();  // DataManager에서 몬스터 데이터 리스트 가져오기
    }

    private void Start()
    {
        // Object Pool 초기화
        monsterPool = new Queue<GameObject>();
        minealPool = new Queue<GameObject>();
        player = new GameObject();

        // Object Pool 초기화 (객체들을 미리 생성해서 Pool에 저장)
        InitializePool(monsterPool, goblinPrefab, "Goblin", maxMineralCount);

        // 예시: Goblin 몬스터를 초기화하고 생성
        GameObject goblin = InitializeObject(goblinPrefab,"Orc");
        player = InitializeObject(playerPrefab, "Goblin");

        player.SetActive(true);
    }

    private GameObject InitializeObject(GameObject prefab, string name)
    {
        // GameObject를 생성하고 DataManager에서 필요한 정보를 통해 초기화
        GameObject obj = Instantiate(prefab);
        DontDestroyOnLoad(obj);

        // 몬스터 데이터가 존재하면 해당 데이터로 몬스터를 초기화
        if (prefab == goblinPrefab)
        {
            MonsterData monsterData = DataManager.Instance.GetMonsterData(name); // "Goblin" 이름을 가진 몬스터 데이터 검색

            if (monsterData != null)
            {
                obj.GetComponent<Monster>().Initialize(monsterData); // 몬스터 초기화
            }
        }

        obj.SetActive(false); // 초기에는 비활성화
        return obj;
    }

    private void InitializePool(Queue<GameObject> pool, GameObject prefab, string name,int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab);
            DontDestroyOnLoad(obj);
            // 몬스터 데이터가 존재하면 해당 데이터로 몬스터를 초기화
            if (prefab == goblinPrefab)
            {
                MonsterData monsterData = DataManager.Instance.GetMonsterData(name); // "Goblin" 이름을 가진 몬스터 데이터 검색

                if (monsterData != null)
                {
                    obj.GetComponent<Monster>().Initialize(monsterData); // 몬스터 초기화
                }
            }
            obj.SetActive(false); // 초기에는 객체가 활성화되지 않도록 설정
            pool.Enqueue(obj);
        }
    }

    // 광물 객체를 Object Pool에서 가져오기
    public GameObject GetMineral(Vector3 position)
    {
        return GetObjectFromPool(minealPool, minealPrefab, position);
    }

    // Object Pool에서 객체를 꺼내는 메서드
    private GameObject GetObjectFromPool(Queue<GameObject> pool, GameObject prefab, Vector3 position)
    {
        GameObject obj;

        if (pool.Count > 0)
        {
            // Object Pool에서 비활성화된 객체를 활성화
            obj = pool.Dequeue();
            obj.SetActive(true);
        }
        else
        {
            // Object Pool이 비어있을 경우 새로운 객체 생성
            obj = Instantiate(prefab);
        }

        // 위치 설정
        obj.transform.position = position;

        return obj;
    }

    // Object Pool에 객체 반환
    public void ReturnObjectToPool(GameObject obj, Queue<GameObject> pool)
    {
        obj.SetActive(false); // 비활성화
        pool.Enqueue(obj);     // Object Pool에 반환
    }
}