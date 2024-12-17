using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MonsterPool monsterPool;

    public List<int> creatureIds = new List<int>(); // ������ ���� ID ���
    public List<Vector2> spawnPoints = new List<Vector2>(); // ���� ��ġ ���
    public float spawnInterval = 30f; // ���� ����
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
                    // ��ġ�� ���� ID�� ������� üũ �� ����
                    if (i < creatureIds.Count) // ���� ID�� ��ġ ������ �¾ƾ� ��
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
                currentMonsters.Add(monster); // ������ ���� ����
            }
        }
    }

    private bool IsMonsterActiveAtPosition(Vector2 position)
    {
        foreach (GameObject monster in currentMonsters)
        {
            if (monster != null && monster.activeInHierarchy && Vector2.Distance(monster.transform.position, position) < 1f)
            {
                return true; // �ش� ��ġ�� Ȱ��ȭ�� ���Ͱ� ����
            }
        }
        return false; // �ش� ��ġ�� ���Ͱ� ����
    }
}