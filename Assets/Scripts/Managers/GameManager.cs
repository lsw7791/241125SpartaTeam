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
    public int sceneNum;
    public IInteractable InteractableObject { get; set; }
    public IPlayerRepository repository;

    // CharacterList를 GameManager에서 관리

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("GameManager Awake 호출");

        // FilePlayerRepository를 사용해 CharacterList 초기화
        repository = new FilePlayerRepository();
        // 데이터 매니저 초기화
        DataManager = new DataManager(repository);
        Debug.Log("CharacterList 객체 생성 후");

        // 다른 매니저 초기화
        if (CraftingManager == null) CraftingManager = gameObject.AddComponent<CraftingManager>();
        List<ItemInstance> items = new List<ItemInstance>();
        ItemManager.Instance.Initialize(items);

        sceneNum = 24;
    }

    void Start()
    {
        // FilePlayerRepository 초기화는 Start()에서 호출
        if (repository is FilePlayerRepository fileRepo)
        {
            Debug.Log("FilePlayerRepository Initialize 호출");
            fileRepo.Initialize();  // Initialize 호출
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        Instantiate(sceneNum);
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnManager.playerObject.SetActive(false);
        if (scene.name == DataManager.Scene.GetMapTo(sceneNum)) // TO Forest
        {
            Instantiate(sceneNum);
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    // 데이터 관련 메서드
    public void SavePlayerData(List<PlayerData> data)
    {
        repository.SavePlayerData(data);
    }
}
