using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    public T prefab;            // 풀링할 오브젝트의 프리팹
    public int poolSize = 10;   // 풀 크기
    private Queue<T> poolQueue = new Queue<T>();  // 풀을 위한 큐

    // 풀 초기화
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            T obj = Instantiate(prefab);   // 프리팹을 인스턴스화
            obj.gameObject.SetActive(false);  // 비활성화
            poolQueue.Enqueue(obj);  // 풀에 추가
        }
    }

    // 오브젝트를 요청하는 함수
    public T GetObject()
    {
        if (poolQueue.Count > 0)
        {
            T obj = poolQueue.Dequeue();  // 풀에서 하나 꺼냄
            obj.gameObject.SetActive(true);  // 오브젝트 활성화
            return obj;
        }
        else
        {
            T obj = Instantiate(prefab);  // 풀에 남은 오브젝트가 없으면 새로 생성
            obj.gameObject.SetActive(true);
            return obj;
        }
    }

    // 오브젝트를 풀에 반환하는 함수
    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);  // 오브젝트 비활성화
        poolQueue.Enqueue(obj);  // 풀에 반환
    }
}