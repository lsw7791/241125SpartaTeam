using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> _projectilePools = new();

    public void InitializeProjectilePool(string inProjectileType, int inPoolSize)
    {
        Queue<GameObject> poolQueue = new Queue<GameObject>();
        for (int i = 0; i < inPoolSize; i++)
        {
            GameObject newObject = Instantiate(Resources.Load<GameObject>($"Prefabs/AttackObjects/{inProjectileType}"), transform);
            newObject.SetActive(false);  // 비활성화 상태로 추가
            poolQueue.Enqueue(newObject);
        }
        _projectilePools.Add(inProjectileType, poolQueue);
    }


    public GameObject SpawnFromPool(string inTag)
    {
        if (!_projectilePools.TryGetValue(inTag, out var objectPool))
        {
            return null;
        }
        for (int i = 0; i < objectPool.Count; i++)
        {
            GameObject outObject = objectPool.Dequeue();
            objectPool.Enqueue(outObject);

            if (!outObject.activeInHierarchy)
            {
                outObject.SetActive(true);
                return outObject;
            }
        }

        GameObject outNewObject = Instantiate(objectPool.Peek(), transform);
        outNewObject.SetActive(true);
        objectPool.Enqueue(outNewObject);

        return outNewObject;
    }
}