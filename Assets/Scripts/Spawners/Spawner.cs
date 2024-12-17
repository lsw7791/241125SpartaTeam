using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MonsterPool monsterPool;

    public List<int> creatureIds = new List<int>(); // 스폰할 몬스터 ID 목록
    public List<Vector2> spawnPoints = new List<Vector2>(); // 스폰 위치 목록
    public float spawnInterval = 30f; // 스폰 간격
    public int sceneNumber;
    private List<GameObject> currentMonsters = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (sceneNumber == GameManager.Instance.SceneNum)
            {
                for (int i = 0; i < spawnPoints.Count; i++)
                {
                    // 위치와 몬스터 ID를 기반으로 체크 및 스폰
                    if (i < creatureIds.Count) // 몬스터 ID가 위치 개수와 맞아야 함
                    {
                        CheckAndSpawnMonster(spawnPoints[i], creatureIds[i]);
                    }
                }

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    private void CheckAndSpawnMonster(Vector2 spawnPoint, int creatureId)
    {
        if (!IsMonsterActiveAtPosition(spawnPoint))
        {
            GameObject monster = monsterPool.GetMonster(creatureId, spawnPoint);
            if (monster != null)
            {
                currentMonsters.Add(monster); // 스폰된 몬스터 관리
            }
        }
    }

    private bool IsMonsterActiveAtPosition(Vector2 position)
    {
        foreach (GameObject monster in currentMonsters)
        {
            if (monster != null && monster.activeInHierarchy && Vector2.Distance(monster.transform.position, position) < 1f)
            {
                return true; // 해당 위치에 활성화된 몬스터가 있음
            }
        }
        return false; // 해당 위치에 몬스터가 없음
    }
}