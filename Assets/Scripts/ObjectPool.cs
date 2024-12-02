using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    public T prefab;            // Ǯ���� ������Ʈ�� ������
    public int poolSize = 10;   // Ǯ ũ��
    private Queue<T> poolQueue = new Queue<T>();  // Ǯ�� ���� ť

    // Ǯ �ʱ�ȭ
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            T obj = Instantiate(prefab);   // �������� �ν��Ͻ�ȭ
            obj.gameObject.SetActive(false);  // ��Ȱ��ȭ
            poolQueue.Enqueue(obj);  // Ǯ�� �߰�
        }
    }

    // ������Ʈ�� ��û�ϴ� �Լ�
    public T GetObject()
    {
        if (poolQueue.Count > 0)
        {
            T obj = poolQueue.Dequeue();  // Ǯ���� �ϳ� ����
            obj.gameObject.SetActive(true);  // ������Ʈ Ȱ��ȭ
            return obj;
        }
        else
        {
            T obj = Instantiate(prefab);  // Ǯ�� ���� ������Ʈ�� ������ ���� ����
            obj.gameObject.SetActive(true);
            return obj;
        }
    }

    // ������Ʈ�� Ǯ�� ��ȯ�ϴ� �Լ�
    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);  // ������Ʈ ��Ȱ��ȭ
        poolQueue.Enqueue(obj);  // Ǯ�� ��ȯ
    }
}