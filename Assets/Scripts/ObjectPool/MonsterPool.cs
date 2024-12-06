using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    // creatureId를 키로 사용하여 풀을 관리
    private Dictionary<int, Queue<GameObject>> monsterPools = new Dictionary<int, Queue<GameObject>>();
    private Dictionary<int, int> poolSizes = new Dictionary<int, int>();  // 각 몬스터의 creatureId를 기준으로 초기 풀 크기 저장
    private const int MAX_POOL_SIZE = 20;  // 풀의 최대 크기 제한
    // 풀 초기화 (creatureId와 데이터를 함께 초기화)
    public GameObject InitializeMine(int creatureId, Vector2 position)
    {
        if(creatureId <13) return null;// 몬스터 소환 못하게 막기
            GameObject minePrefab = Resources.Load<GameObject>(GameManager.Instance.dataManager.creature.GetPrefabsPath(creatureId));
            GameObject obj = Instantiate(minePrefab);
            Mine mine = obj.GetComponent<Mine>();
            mine.SetComponent(creatureId);// 몬스터 모든 데이터 초기화
            obj.transform.position = position;
        return obj;
    }
    public void InitializeMonsterPool(int creatureId, int poolSize)
    {      
        Queue<GameObject> poolQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("Prefabs/Monster"));
            obj.SetActive(false);  // 비활성화 상태로 추가
            // 몬스터 데이터 초기화 (creatureId를 받아서 초기화)
            Monster monster = obj.GetComponent<Monster>();
            if (monster != null)
            {
                GameObject childPrefab = Resources.Load<GameObject>(GameManager.Instance.dataManager.creature.GetPrefabsPath(creatureId));
                GameObject child = Instantiate(childPrefab);
                child.transform.SetParent(obj.transform);  // obj가 부모가 되도록 설정
                child.transform.localPosition = Vector3.zero;  // 자식의 위치를 부모의 (0, 0, 0)으로 설정 (필요시 조정)
                monster.SetComponent(creatureId);// 몬스터 모든 데이터 초기화

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
            Monster monster = monsterObj.GetComponent<Monster>();
            if (monster != null)
            {

                monster.ResetStatus();  // 풀에서 반환된 몬스터 상태 리셋
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
    public void ReturnMonster(int creatureId, GameObject thisObject)
    {
        thisObject.SetActive(false);  // 비활성화

        // 몬스터의 상태 리셋
        Monster monster = thisObject.GetComponent<Monster>();
        if (thisObject.GetComponent<Monster>() != null)
        {
            monster.ResetStatus();  // 상태 리셋 (currentHealth, isDie)
        }

        // 풀에 반환
        if (monsterPools.ContainsKey(creatureId))
        {
            monsterPools[creatureId].Enqueue(thisObject);
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
        GameObject childPrefab = Resources.Load<GameObject>(GameManager.Instance.dataManager.creature.GetPrefabsPath(creatureId));
        return childPrefab;
    }
}