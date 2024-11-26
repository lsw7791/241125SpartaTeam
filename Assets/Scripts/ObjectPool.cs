using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Ǯ�� ������ ������Ʈ�� ���� ����
    public GameObject objectPrefab;
    public int poolSize = 10;
    void Start()
    {
    }

    // ������Ʈ Ǯ �ʱ�ȭ
    private void InitializePool(Queue<GameObject> objectPool)
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
    public GameObject GetObject(Queue<GameObject> objectPool)
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
    public void ReturnObject(GameObject obj, Queue<GameObject> objectPool)
    {
        obj.SetActive(false); // ��Ȱ��ȭ
        objectPool.Enqueue(obj); // Ǯ�� ��ȯ
    }
}
