using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // 싱글톤 인스턴스
    private static T instance;

    // 싱글톤 인스턴스 접근용 프로퍼티
    public static T Instance
    {
        get
        {
            // 인스턴스가 없을 때만 찾거나 생성
            if (instance == null)
            {
                // 씬에서 이미 존재하는 인스턴스를 찾음
                instance = FindObjectOfType<T>();

                // 인스턴스가 없으면 새로운 게임 오브젝트에 추가해서 생성
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";

                    // 싱글톤이 씬 전환 시에도 파괴되지 않도록 설정
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    // 인스턴스가 여러 개 생성되지 않도록 하기 위해 Awake에서 체크
    protected virtual void Awake()
    {
        // 이미 인스턴스가 있을 경우, 중복 생성 방지
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // 초기화 과정에서 인스턴스를 설정
            instance = this as T;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 파괴되지 않도록 설정
        }
    }
}