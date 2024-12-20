using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MonsterPool monsterPool;

    public List<int> creatureIds = new List<int>(); // 스폰할 몬스터 ID 목록
    public List<Vector2> spawnPoints = new List<Vector2>(); // 스폰 위치 목록
    public float spawnInterval = 10f; // 스폰 간격
    public int sceneNumber;
    public int ActiveMonsterCount; // 최대 활성화 가능한 몬스터 개수
    private List<GameObject> currentMonsters = new List<GameObject>();

    private void Awake()
    {
        sceneNumber = GameManager.Instance.SceneNum;
    }

    private void Start()
    {
        if (monsterPool != null) Destroy(monsterPool);
        if (monsterPool == null) monsterPool = GameManager.Instance.SpawnManager.GroupSpawn($"Monster{sceneNumber}").AddComponent<MonsterPool>();

        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (sceneNumber == GameManager.Instance.SceneNum)
            {
                // 활성화된 몬스터 개수를 기준으로 스폰
                int activeCount = GetActiveMonsterCount();
                if (activeCount < ActiveMonsterCount)
                {
                    SpawnMonster();
                }

                yield return new WaitForSeconds(spawnInterval);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void SpawnMonster()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (currentMonsters.Count >= ActiveMonsterCount) break; // 최대 활성화 개수를 초과하면 중단

            int creatureId = i < creatureIds.Count ? creatureIds[i] : creatureIds[0]; // ID 순환 사용
            GameObject monster = monsterPool.GetMonster(creatureId, spawnPoints[i]);

            if (monster != null)
            {
                monster.SetActive(true); // 몬스터 활성화
                currentMonsters.Add(monster); // 현재 활성화된 몬스터 관리
            }
        }
    }

    private int GetActiveMonsterCount()
    {
        // 현재 활성화된 몬스터 개수를 계산
        currentMonsters.RemoveAll(monster => monster == null || !monster.activeInHierarchy);
        return currentMonsters.Count;
    }
}