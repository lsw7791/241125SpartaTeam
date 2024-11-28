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
        InitializePool<Monster>(monsterPool, goblinPrefab, "Goblin", maxMonsterCount);

        // 예시: Goblin 몬스터를 초기화하고 생성
        GameObject goblin = InitializeObject<Monster>(goblinPrefab, "Goblin");
        player = InitializeObject<Player>(playerPrefab, "Player");

        goblin.SetActive(true);  // 예시로 Goblin을 활성화
        player.SetActive(true);  // 플레이어 활성화
    }

    private GameObject InitializeObject<T>(GameObject prefab, string name) where T : MonoBehaviour
    {
        // GameObject를 생성하고 DataManager에서 필요한 정보를 통해 초기화
        GameObject obj = Instantiate(prefab);
        DontDestroyOnLoad(obj);

        // T 타입의 컴포넌트를 가져와 초기화
        T component = obj.GetComponent<T>();
        if (component != null)
        {
            // 예시: 만약 T가 Monster라면, 몬스터 데이터로 초기화
            if (typeof(T) == typeof(Monster))
            {
                MonsterData monsterData = DataManager.Instance.GetMonsterData(name);
                if (monsterData != null)
                {
                    (component as Monster).Initialize(monsterData); // Monster 초기화
                }
            }
            // T 타입이 다른 객체라면 그에 맞는 초기화 작업을 추가하면 된다.
            // 예: (component as Weapon).Initialize(weaponData);
        }

        obj.SetActive(false); // 초기에는 객체가 비활성화됨
        return obj;
    }

    private void InitializePool<T>(Queue<GameObject> pool, GameObject prefab, string name, int count) where T : MonoBehaviour
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab);
            DontDestroyOnLoad(obj);

            // T 타입의 객체에 대해 초기화 작업을 한다면, 해당 컴포넌트를 가져온 후 초기화
            T component = obj.GetComponent<T>();
            if (component != null)
            {
                // 예시: 만약 T가 Monster라면, 데이터 초기화
                if (typeof(T) == typeof(Monster))
                {
                    MonsterData monsterData = DataManager.Instance.GetMonsterData(name);
                    if (monsterData != null)
                    {
                        (component as Monster).Initialize(monsterData); // 컴포넌트를 캐스팅하여 초기화
                    }
                }

                // T 타입이 다른 클래스라면 그에 맞는 초기화 작업을 추가하면 된다.
                // 예: (component as Weapon).Initialize(weaponData);
            }

            obj.SetActive(false); // 초기에는 객체가 비활성화되도록 설정
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