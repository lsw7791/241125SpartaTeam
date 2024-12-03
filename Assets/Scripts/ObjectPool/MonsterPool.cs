using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    // creatureId를 키로 사용하여 풀을 관리
    private Dictionary<int, Queue<GameObject>> monsterPools = new Dictionary<int, Queue<GameObject>>();
    private Dictionary<int, int> poolSizes = new Dictionary<int, int>();  // 각 몬스터의 creatureId를 기준으로 초기 풀 크기 저장
    private const int MAX_POOL_SIZE = 20;  // 풀의 최대 크기 제한
    private GameManager gameManager;
    // 풀 초기화 (creatureId와 데이터를 함께 초기화)
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
            obj.SetActive(false);  // 비활성화 상태로 추가

            // 몬스터 데이터 초기화 (creatureId를 받아서 초기화)
            MonsterData monsterData = obj.GetComponent<MonsterData>();
            if (monsterData != null)
            {
                monsterData.Initialize(creatureId);  // creatureId로 몬스터 데이터 초기화
            }

            poolQueue.Enqueue(obj);
        }

        monsterPools[creatureId] = poolQueue;
        poolSizes[creatureId] = poolSize;  // 해당 creatureId의 풀 크기 저장
    }

    // 몬스터를 요청하는 함수 (creatureId를 기준으로 풀에서 가져오기)
    public GameObject GetMonster(int creatureId, Vector2 position)
    {
        if (monsterPools.ContainsKey(creatureId) && monsterPools[creatureId].Count > 0)
        {
            GameObject monsterObj = monsterPools[creatureId].Dequeue();
            monsterObj.SetActive(true);  // 활성화

            // 몬스터의 데이터와 상태 초기화 (풀에서 가져올 때 리셋)
            MonsterData monsterData = monsterObj.GetComponent<MonsterData>();
            if (monsterData != null)
            {
                monsterData.ResetStatus();  // 풀에서 반환된 몬스터 상태 리셋
            }

            monsterObj.transform.position = position;  // 위치 설정

            return monsterObj;
        }
        else
        {
            // 풀에 몬스터가 없고, 최대 풀 크기를 초과하지 않는 경우에만 새로운 몬스터 생성
            if (!monsterPools.ContainsKey(creatureId) || monsterPools[creatureId].Count < MAX_POOL_SIZE)
            {
                GameObject prefab = GetPrefabByCreatureId(creatureId);  // creatureId로 프리팹을 가져옴
                if (prefab != null)
                {
                    GameObject newMonster = Instantiate(prefab);
                    newMonster.transform.position = position;  // 위치 설정

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
                return null;  // 풀 크기 초과 시 null을 반환
            }
        }
    }

    // 몬스터를 풀에 반환하는 함수 (creatureId 기준)
    public void ReturnMonster(int creatureId, GameObject monster)
    {
        monster.SetActive(false);  // 비활성화

        // 몬스터의 상태 리셋
        MonsterData monsterData = monster.GetComponent<MonsterData>();
        if (monsterData != null)
        {
            monsterData.ResetStatus();  // 상태 리셋 (currentHealth, isDie)
        }

        // 풀에 반환
        if (monsterPools.ContainsKey(creatureId))
        {
            monsterPools[creatureId].Enqueue(monster);
        }
        else
        {
            // 풀에 해당 creatureId가 없다면 에러 처리 (풀에 추가하거나, 새로운 풀 생성 가능)
            Debug.LogWarning($"Monster pool for creatureId {creatureId} does not exist.");
        }
    }

    // creatureId에 해당하는 프리팹을 가져오는 함수
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
            default: return null;  // 해당하는 creatureId가 없을 경우 null 반환
        }
    }
}