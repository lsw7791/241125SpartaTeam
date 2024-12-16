using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public DataManager dataManager;
    public CraftingManager craftingManager;
    public SoundManager soundManager;
    public SpawnManager spawnManager;
    public CharacterSlotUIManager slotUIManager;
    public CharacterSlotManager characterSlotManager;
    GameObject SoundManagerObject;
    public Player player;
    private PlayerData _currentPlayer;
    public int sceneNum;
    public IInteractable InteractableObject { get; set; } // 현재 상호작용 가능한 객체
    public IPlayerRepository repository; // 데이터 저장소

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("GameManager Awake 호출");

        // 데이터 매니저와 슬롯 매니저 초기화
        dataManager = new DataManager();

        // FilePlayerRepository를 사용해 CharacterSlotManager 초기화
        repository = new FilePlayerRepository();
        Debug.Log("FilePlayerRepository 객체 생성 후");
        characterSlotManager = new CharacterSlotManager(repository);

        // 슬롯 UI 매니저가 에디터에서 할당되지 않았다면
        if (slotUIManager == null)
        {
            Debug.LogError("CharacterSlotUIManager가 할당되지 않았습니다! 에디터에서 할당해 주세요.");
        }

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

        // 슬롯 UI 매니저에 데이터 연동
        if (slotUIManager != null)
        {
            slotUIManager.UpdateSlotUI(characterSlotManager.GetAllSlotData());
        }

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
