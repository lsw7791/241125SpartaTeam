using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateManager : MonoSingleton<CreateManager>
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject[] monsterPrefabs; // 몬스터 프리팹 배열
    [SerializeField] GameObject minealPrefab;

    // Object Pool 변수들
    private Queue<GameObject> monsterPool;
    private Queue<GameObject> minealPool;
    GameObject player;

    // Object Pool에 생성할 최대 개수
    [SerializeField] private int maxMonsterCount = 10;
    [SerializeField] private int maxMineralCount = 10;

    // 몬스터 데이터를 담을 리스트
    private List<MonsterData> monsterDataList = new List<MonsterData>();

    private void Awake()
    {
        // 프리팹 불러오기
        playerPrefab = Resources.Load<GameObject>("Prefabs/TestPlayer");
        minealPrefab = Resources.Load<GameObject>("Prefabs/TestMineral");

        // 몬스터 프리팹을 Resources에서 동적으로 로드
        monsterPrefabs = Resources.LoadAll<GameObject>("Prefabs/Monsters");

        // CSV 파일에서 몬스터 데이터 불러오기
        LoadMonsterData();
    }

    private void Start()
    {
        // Object Pool 초기화
        monsterPool = new Queue<GameObject>();
        minealPool = new Queue<GameObject>();
        player = new GameObject();

        // Object Pool 초기화 (객체들을 미리 생성해서 Pool에 저장)
        InitializePool(minealPool, minealPrefab, maxMineralCount);
        InitializeObject(player, playerPrefab);
        player.SetActive(true);
    }

    // 몬스터 데이터 CSV에서 불러오기
    private void LoadMonsterData()
    {
        string filePath = "Assets/Resources/SCV/MonsterData.csv"; // CSV 파일 경로

        // 파일이 존재하는지 확인
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] values = line.Split(',');

                // CSV에서 이름, 체력, 스피드, 데미지 데이터를 읽어옴
                string name = values[0].Trim();
                int health = int.Parse(values[1].Trim());
                float speed = float.Parse(values[2].Trim());
                int damage = int.Parse(values[3].Trim());

                // 몬스터 데이터 생성
                MonsterData monsterData = new MonsterData(name, health, speed, damage);
                monsterDataList.Add(monsterData);
            }
        }
        else
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다: " + filePath);
        }
    }

    // Object Pool에 객체 초기화
    private void InitializeObject(GameObject thisObject, GameObject prefab)
    {
        GameObject obj = Instantiate(prefab);
        DontDestroyOnLoad(obj);
        obj.SetActive(false); // 초기에는 객체가 활성화되지 않도록 설정
        thisObject = obj;
    }

    private void InitializePool(Queue<GameObject> pool, GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab);
            DontDestroyOnLoad(obj);
            obj.SetActive(false); // 초기에는 객체가 활성화되지 않도록 설정
            pool.Enqueue(obj);
        }
    }

    // 몬스터 객체를 Object Pool에서 가져오기
    public GameObject GetMonster(Vector3 position, string monsterName)
    {
        // CSV에서 몬스터 데이터를 찾음
        MonsterData monsterData = monsterDataList.Find(m => m.Name == monsterName);

        if (monsterData != null)
        {
            // 해당 이름의 몬스터 데이터를 찾으면, 해당 프리팹을 소환
            GameObject monsterPrefab = GetMonsterPrefabByName(monsterName);
            if (monsterPrefab != null)
            {
                GameObject monster = Instantiate(monsterPrefab, position, Quaternion.identity);
                monster.GetComponent<Monster>().Initialize(monsterData); // 몬스터 초기화
                return monster;
            }
        }

        return null;
    }

    // 몬스터 이름에 맞는 프리팹을 찾는 메서드
    private GameObject GetMonsterPrefabByName(string monsterName)
    {
        foreach (GameObject prefab in monsterPrefabs)
        {
            if (prefab.name == monsterName)
            {
                return prefab;
            }
        }
        return null;
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