using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager _instance;
    // 게임매니저에 모든 매니저들이 연결되어있음.
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
            // 싱글톤 패턴: _instance가 null일 경우, 현재 씬에서 GameManager 객체를 찾음
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
        // 싱글톤 인스턴스가 여러 번 생성되지 않도록 방지
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환 시에도 GameManager를 유지
    }

    private void Start()
    {
        // 매니저들을 대신 초기화해줘야함 -> 다른 매니저는 모노비헤이비어가 없으므로
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
