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
            obj.SetActive(false);  // ��Ȱ��ȭ ���·� �߰�
            poolQueue.Enqueue(obj);
        }
        projectilePools[projectileType] = poolQueue;
    }

    public GameObject GetProjectile(string projectileType)
    {
        if (projectilePools.ContainsKey(projectileType) && projectilePools[projectileType].Count > 0)
        {
            GameObject projectile = projectilePools[projectileType].Dequeue();
            projectile.SetActive(true);  // Ȱ��ȭ
            return projectile;
        }
        else
        {
            // Ǯ�� ����ü�� ������ ���� ����
            return Instantiate(Resources.Load(projectileType) as GameObject);
        }
    }

    public void ReturnProjectile(string projectileType, GameObject projectile)
    {
        projectile.SetActive(false);  // ��Ȱ��ȭ
        projectilePools[projectileType].Enqueue(projectile);  // Ǯ�� ��ȯ
    }
}
