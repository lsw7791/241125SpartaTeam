using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;

public class MonsterPool : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> monsterPools = new Dictionary<string, Queue<GameObject>>();

    // 풀 초기화 (데이터와 함께 초기화)
    public void InitializeMonsterPool(string monsterType, GameObject prefab, int poolSize, int creatureId)
    {
        Queue<GameObject> poolQueue = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);  // 비활성화 상태로 추가

            // 몬스터 데이터 초기화 (단일 creatureId를 받아서 초기화)
            MonsterData monsterData = obj.GetComponent<MonsterData>();
            if (monsterData != null)
            {
                monsterData.Initialize(creatureId);  // 단일 creatureId로 몬스터 데이터 초기화
            }

            poolQueue.Enqueue(obj);
        }

        monsterPools[monsterType] = poolQueue;
    }

    // 몬스터를 요청하는 함수
    public GameObject GetMonster(string monsterType)
    {
        if (monsterPools.ContainsKey(monsterType) && monsterPools[monsterType].Count > 0)
        {
            GameObject monsterObj = monsterPools[monsterType].Dequeue();
            monsterObj.SetActive(true);  // 활성화

            // 몬스터의 데이터와 상태 초기화 (풀에서 가져올 때 리셋)
            MonsterData monsterData = monsterObj.GetComponent<MonsterData>();
            if (monsterData != null)
            {
                monsterData.ResetStatus();  // 풀에서 반환된 몬스터 상태 리셋
            }

            return monsterObj;
        }
        else
        {
            // 풀에 몬스터가 없으면 새로 생성 (성능 최적화를 위해 최소한의 로드를 고려할 수 있음)
            GameObject newMonster = Instantiate(Resources.Load(monsterType) as GameObject);
            return newMonster;
        }
    }

    // 몬스터를 풀에 반환하는 함수
    public void ReturnMonster(string monsterType, GameObject monster)
    {
        monster.SetActive(false);  // 비활성화

        // 몬스터의 상태 리셋
        MonsterData monsterData = monster.GetComponent<MonsterData>();
        if (monsterData != null)
        {
            monsterData.ResetStatus();  // 상태 리셋 (currentHealth, isDie)
        }

        // 풀에 반환
        if (monsterPools.ContainsKey(monsterType))
        {
            monsterPools[monsterType].Enqueue(monster);
        }
        else
        {
            // 풀에 해당 몬스터 타입이 없다면 에러 처리 (풀에 추가하거나, 새로운 풀 생성 가능)
            Debug.LogWarning($"Monster pool for type {monsterType} does not exist.");
        }
    }
}