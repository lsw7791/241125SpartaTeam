using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public DataManager DataManager; // DataManager만 사용하도록 변경
    public CraftingManager CraftingManager;
    public SoundManager SoundManager;
    public SpawnManager SpawnManager;
    public Player Player;
    private PlayerData _currentPlayer;
    public int SceneNum;
    public IInteractable InteractableObject { get; set; }

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("GameManager Awake 호출");

        // DataManager 초기화
        DataManager = new DataManager();  // DataManager의 기본 생성자로 초기화
        Debug.Log("DataManager 객체 생성 후");

        // 다른 매니저 초기화
        if (CraftingManager == null) CraftingManager = gameObject.AddComponent<CraftingManager>();
        List<ItemInstance> items = new List<ItemInstance>();
        ItemManager.Instance.Initialize(items);

        SceneNum = 24;
    }

    void Start()
    {

        SceneManager.sceneLoaded += OnSceneLoaded;

        Instantiate(SceneNum);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnManager.playerObject.SetActive(false);
        if (scene.name == DataManager.Scene.GetMapTo(SceneNum)) // TO Forest
        {
            Instantiate(SceneNum);
        }
    }

    void Instantiate(int mapNum)
    {
        GameObject miniCamera = Instantiate(Resources.Load<GameObject>("Prefabs/Cameras/MinimapCamera"));
        if (SpawnManager == null) SpawnManager = gameObject.AddComponent<SpawnManager>();
        if (SoundManager == null) SoundManager = gameObject.AddComponent<SoundManager>();
        SpawnManager.SpawnPlayer(mapNum);
        SpawnManager.Initialize(mapNum);
    }

    public void LoadScene(string sceneName)
    {
        // 씬 전환
        SceneManager.LoadScene(sceneName);
    }

    public PlayerData GetCurrentCharacter()
    {
        return _currentPlayer;
    }

    public void StartGame(PlayerData character)
    {
        _currentPlayer = character;
        Debug.Log($"게임 시작: {character.NickName}");

        // 캐릭터 생성 시 데이터를 즉시 저장
        DataManager.SaveCharacterData();

        GameManager.Instance.SceneNum = 2;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }

    // 캐릭터 슬롯 정보 로드
   
    // 게임 종료 시 플레이어 정보 저장
    public void SavePlayerData()
    {
        if (_currentPlayer != null)
        {
            DataManager.SaveCharacterData();  // 현재 플레이어의 데이터를 저장
        }
    }
}
