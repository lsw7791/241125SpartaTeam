using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager _instance;
    // 여러 매니저들을 관리할 싱글톤 클래스입니다.
    GameManager _GameManager = new GameManager();
    DataManager _DataManager = new DataManager();
    TimeManager _TimeManager = new TimeManager();
    CreateManager _CreateManager = new CreateManager();
    CoroutineManager _CorutineManager = new CoroutineManager();
    UIManager _UIManager = new UIManager();
    SoundManager _SoundManager = new SoundManager();

    public static Manager Instance
    {
        get
        {
            // 싱글톤 패턴: _instance가 null인 경우, 존재하는 GameManager 객체를 찾음
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
        // 싱글톤 인스턴스가 이미 존재할 경우 새로운 객체를 파괴
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
        // 매니저들의 초기화 함수 호출 -> 다른 매니저들의 의존성을 방지하기 위해
        _GameManager.Init();
        _DataManager.Init();
        _TimeManager.Init();
        _CreateManager.Init();
        _CorutineManager.Init();
        _UIManager.Init();
        _SoundManager.Init();
    }
}
