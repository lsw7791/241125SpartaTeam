using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    private static T instance;

    // �̱��� �ν��Ͻ� ���ٿ� ������Ƽ
    public static T Instance
    {
        get
        {
            // �ν��Ͻ��� ���� ���� ã�ų� ����
            if (instance == null)
            {
                // ������ �̹� �����ϴ� �ν��Ͻ��� ã��
                instance = FindObjectOfType<T>();

                // �ν��Ͻ��� ������ ���ο� ���� ������Ʈ�� �߰��ؼ� ����
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";

                    // �̱����� �� ��ȯ �ÿ��� �ı����� �ʵ��� ����
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    // �ν��Ͻ��� ���� �� �������� �ʵ��� �ϱ� ���� Awake���� üũ
    protected virtual void Awake()
    {
        // �̹� �ν��Ͻ��� ���� ���, �ߺ� ���� ����
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // �ʱ�ȭ �������� �ν��Ͻ��� ����
            instance = this as T;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �ı����� �ʵ��� ����
        }
    }
}