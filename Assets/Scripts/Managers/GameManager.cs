using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public DataManager dataManager;
    public CraftingManager craftingManager;
    public SoundManager soundManager;
    public SpawnManager spawnManager;
    GameObject SoundManagerObject;
    public Player player;
    private PlayerData _currentPlayer;
    public int sceneNum;
    public IInteractable InteractableObject { get; set; }
    public IPlayerRepository repository;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("GameManager Awake 호출");

        // 데이터 매니저와 슬롯 매니저 초기화
        dataManager = new DataManager();

        // FilePlayerRepository를 사용해 CharacterSlotManager 초기화
        repository = new FilePlayerRepository();
        Debug.Log("FilePlayerRepository 객체 생성 후");

        // 다른 매니저 초기화
        if (craftingManager == null) craftingManager = gameObject.AddComponent<CraftingManager>();
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

    public List<PlayerData> GetAllPlayerData()
    {
        if (repository == null)
        {
            Debug.Log("repository가 초기화되지 않았습니다!");
            return new List<PlayerData>();
        }
        return repository.LoadPlayerData();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        spawnManager.playerObject.SetActive(false);
        if (scene.name == dataManager.scene.GetMapTo(sceneNum)) // TO Forest
        {
            Instantiate(sceneNum);
        }
    }

    void Instantiate(int mapNum)
    {
        GameObject miniCamera = Instantiate(Resources.Load<GameObject>("Prefabs/Cameras/MinimapCamera"));
        if (spawnManager == null) spawnManager = gameObject.AddComponent<SpawnManager>();
        if (soundManager == null) soundManager = gameObject.AddComponent<SoundManager>();
        spawnManager.SpawnPlayer(mapNum);
        spawnManager.Initialize(mapNum);
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
