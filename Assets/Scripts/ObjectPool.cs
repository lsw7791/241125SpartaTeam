using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Ǯ�� ������ ������Ʈ�� ���� ����
    public GameObject objectPrefab;
    public int poolSize = 10;
    private Queue<GameObject> objectPool = new Queue<GameObject>();

    void Start()
    {
        // Ǯ �ʱ�ȭ
        InitializePool();
    }

    // ������Ʈ Ǯ �ʱ�ȭ
    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            // ������Ʈ�� �����Ͽ� Ǯ�� �߰�
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false); // ó������ ��Ȱ��ȭ
            objectPool.Enqueue(obj);
        }
    }

    // Ǯ���� ������Ʈ ������
    public GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            // Ǯ���� ��ü ������
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true); // Ȱ��ȭ
            return obj;
        }
        else
        {
            // Ǯ�� ��ü�� ������ ���ο� ��ü ����
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(true);
            return obj;
        }
    }

    // ����� ���� ������Ʈ�� Ǯ�� ��ȯ
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // ��Ȱ��ȭ
        objectPool.Enqueue(obj); // Ǯ�� ��ȯ
    }
}
