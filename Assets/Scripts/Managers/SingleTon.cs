using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon<T> where T : class, new()
{
    // ���� �ν��Ͻ��� ������ ����
    private static T _instance;

    // �̱��� �ν��Ͻ� ���ٿ� ������Ƽ
    public static T Instance
    {
        get
        {
            // �ν��Ͻ��� ���� �������� �ʾҴٸ� ����
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }

    // �����ڴ� �ܺο��� �ν��Ͻ��� �������� ���ϵ��� private���� ����
    protected SingleTon() { }
}