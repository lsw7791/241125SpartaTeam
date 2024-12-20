using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MonsterPool monsterPool;

    public List<int> creatureIds = new List<int>(); // ������ ���� ID ���
    public List<Vector2> spawnPoints = new List<Vector2>(); // ���� ��ġ ���
    public float spawnInterval = 10f; // ���� ����
    public int sceneNumber;
    public int ActiveMonsterCount; // �ִ� Ȱ��ȭ ������ ���� ����
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
                // Ȱ��ȭ�� ���� ������ �������� ����
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
            if (currentMonsters.Count >= ActiveMonsterCount) break; // �ִ� Ȱ��ȭ ������ �ʰ��ϸ� �ߴ�

            int creatureId = i < creatureIds.Count ? creatureIds[i] : creatureIds[0]; // ID ��ȯ ���
            GameObject monster = monsterPool.GetMonster(creatureId, spawnPoints[i]);

            if (monster != null)
            {
                monster.SetActive(true); // ���� Ȱ��ȭ
                currentMonsters.Add(monster); // ���� Ȱ��ȭ�� ���� ����
            }
        }
    }

    private int GetActiveMonsterCount()
    {
        // ���� Ȱ��ȭ�� ���� ������ ���
        currentMonsters.RemoveAll(monster => monster == null || !monster.activeInHierarchy);
        return currentMonsters.Count;
    }
}