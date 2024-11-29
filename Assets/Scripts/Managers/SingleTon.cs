using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon<T> where T : class, new()
{
    // 정적 인스턴스를 저장할 변수
    private static T _instance;

    // 싱글톤 인스턴스 접근용 프로퍼티
    public static T Instance
    {
        get
        {
            // 인스턴스가 아직 생성되지 않았다면 생성
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }

    // 생성자는 외부에서 인스턴스를 생성하지 못하도록 private으로 설정
    protected SingleTon() { }
}