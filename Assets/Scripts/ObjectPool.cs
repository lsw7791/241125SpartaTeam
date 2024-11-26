using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // 풀에 저장할 오브젝트를 위한 변수
    public GameObject objectPrefab;
    public int poolSize = 10;
    private Queue<GameObject> objectPool = new Queue<GameObject>();

    void Start()
    {
        // 풀 초기화
        InitializePool();
    }

    // 오브젝트 풀 초기화
    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            // 오브젝트를 생성하여 풀에 추가
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false); // 처음에는 비활성화
            objectPool.Enqueue(obj);
        }
    }

    // 풀에서 오브젝트 꺼내기
    public GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            // 풀에서 객체 꺼내기
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true); // 활성화
            return obj;
        }
        else
        {
            // 풀에 객체가 없으면 새로운 객체 생성
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(true);
            return obj;
        }
    }

    // 사용이 끝난 오브젝트를 풀로 반환
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false); // 비활성화
        objectPool.Enqueue(obj); // 풀에 반환
    }
}
