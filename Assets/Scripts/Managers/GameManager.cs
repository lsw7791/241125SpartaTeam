using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
static GameManager _instance;
    // ���ӸŴ����� ��� �Ŵ������� ����Ǿ�����.
    DataManager DataManager = new DataManager();
    TimeManager TimeManager = new TimeManager();
    CreateManager CreateManager = new CreateManager();
    CoroutineManager CorutineManager = new CoroutineManager();
    UIManager UIManager = new UIManager();
    SoundManager SoundManager = new SoundManager();
    public static GameManager Instance
    {
        get
        {
            // �̱��� ����: _instance�� null�� ���, ���� ������ GameManager ��ü�� ã��
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // �̱��� �ν��Ͻ��� ���� �� �������� �ʵ��� ����
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� GameManager�� ����
    }

    private void Start()
    {
        // �Ŵ������� ��� �ʱ�ȭ������� -> �ٸ� �Ŵ����� �������̺� �����Ƿ�
        DataManager.Init();
        TimeManager.Init();
        CreateManager.Init();
        CorutineManager.Init();
        UIManager.Init();
        SoundManager.Init();
    }
}
