using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> projectilePools = new Dictionary<string, Queue<GameObject>>();

    public void InitializeProjectilePool(string projectileType, GameObject prefab, int poolSize)
    {
        Queue<GameObject> poolQueue = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);  // 비활성화 상태로 추가
            poolQueue.Enqueue(obj);
        }
        projectilePools[projectileType] = poolQueue;
    }

    public GameObject GetProjectile(string projectileType)
    {
        if (projectilePools.ContainsKey(projectileType) && projectilePools[projectileType].Count > 0)
        {
            GameObject projectile = projectilePools[projectileType].Dequeue();
            projectile.SetActive(true);  // 활성화
            return projectile;
        }
        else
        {
            // 풀에 투사체가 없으면 새로 생성
            return Instantiate(Resources.Load(projectileType) as GameObject);
        }
    }

    public void ReturnProjectile(string projectileType, GameObject projectile)
    {
        projectile.SetActive(false);  // 비활성화
        projectilePools[projectileType].Enqueue(projectile);  // 풀에 반환
    }
}
