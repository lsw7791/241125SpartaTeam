using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager _instance;
    // ���ӸŴ����� ��� �Ŵ������� ����Ǿ�����.
    GameManager _GameManager = new GameManager();
    DataManager _DataManager = new DataManager();
    TimeManager _TimeManager = new TimeManager();
    CreateManager _CreateManager = new CreateManager();
    CoroutineManager _CorutineManager = new CoroutineManager();
    UIManager _UIManager = new UIManager();
    SoundManager _SoundManager = new SoundManager();
    PlayerManager _PlayerManager = new PlayerManager();

    public static Manager Instance
    {
        get
        {
            // �̱��� ����: _instance�� null�� ���, ���� ������ GameManager ��ü�� ã��
            if (_instance == null)
            {
                _instance = FindObjectOfType<Manager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<Manager>();
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
        _GameManager.Init();
        _DataManager.Init();
        _TimeManager.Init();
        _CreateManager.Init();
        _CorutineManager.Init();
        _UIManager.Init();
        _SoundManager.Init();
        _PlayerManager.Init();
    }
}
