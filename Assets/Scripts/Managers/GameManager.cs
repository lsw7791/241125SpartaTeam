using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public DataManager DataManager;
    public CraftingManager CraftingManager;
    public SoundManager SoundManager;
    public SpawnManager SpawnManager;
    GameObject SoundManagerObject;
    public Player Player;
    private PlayerData _currentPlayer;
    public int SceneNum;
    public IInteractable InteractableObject { get; set; }
    public IPlayerRepository Repository;

    // CharacterList를 GameManager에서 관리

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("GameManager Awake 호출");

        // FilePlayerRepository를 사용해 CharacterList 초기화
        Repository = new FilePlayerRepository();
        // 데이터 매니저 초기화
        DataManager = new DataManager();
        Debug.Log("CharacterList 객체 생성 후");

        // 다른 매니저 초기화
        if (CraftingManager == null) CraftingManager = gameObject.AddComponent<CraftingManager>();
        List<ItemInstance> items = new List<ItemInstance>();
        ItemManager.Instance.Initialize(items);

        SceneNum = 24;
    }

    void Start()
    {
        // FilePlayerRepository 초기화는 Start()에서 호출
        if (Repository is FilePlayerRepository fileRepo)
        {
            Debug.Log("FilePlayerRepository Initialize 호출");
            fileRepo.Initialize();  // Initialize 호출
        }

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
        GameManager.Instance.SceneNum = 26;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }

    // 데이터 관련 메서드
    //public void SavePlayerData(List<PlayerData> data)
    //{
    //    Repository.SavePlayerData(data);
    //}
}
