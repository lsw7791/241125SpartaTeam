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
    public IInteractable InteractableObject { get; set; } // ���� ��ȣ�ۿ� ������ ��ü
    public IPlayerRepository repository; // ������ �����

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("GameManager Awake ȣ��");

        // ������ �Ŵ����� ���� �Ŵ��� �ʱ�ȭ
        dataManager = new DataManager();

        // FilePlayerRepository�� ����� CharacterSlotManager �ʱ�ȭ
        repository = new FilePlayerRepository();
        Debug.Log("FilePlayerRepository ��ü ���� ��");
        characterSlotManager = new CharacterSlotManager(repository);

        // ���� UI �Ŵ����� �����Ϳ��� �Ҵ���� �ʾҴٸ�
        if (slotUIManager == null)
        {
            Debug.LogError("CharacterSlotUIManager�� �Ҵ���� �ʾҽ��ϴ�! �����Ϳ��� �Ҵ��� �ּ���.");
        }

        // �ٸ� �Ŵ��� �ʱ�ȭ
        if (craftingManager == null) craftingManager = gameObject.AddComponent<CraftingManager>();
        List<ItemInstance> items = new List<ItemInstance>();
        ItemManager.Instance.Initialize(items);

        sceneNum = 24;
    }

    void Start()
    {
        // FilePlayerRepository �ʱ�ȭ�� Start()���� ȣ��
        if (repository is FilePlayerRepository fileRepo)
        {
            Debug.Log("FilePlayerRepository Initialize ȣ��");
            fileRepo.Initialize();  // Initialize ȣ��
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        // ���� UI �Ŵ����� ������ ����
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
            Debug.Log("repository�� �ʱ�ȭ���� �ʾҽ��ϴ�!");
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
        // �� ��ȯ
        SceneManager.LoadScene(sceneName);
    }

    public PlayerData GetCurrentCharacter()
    {
        return _currentPlayer;
    }

    public void StartGame(PlayerData character)
    {
        _currentPlayer = character;
        Debug.Log($"���� ����: {character.NickName}");
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    // ������ ���� �޼���
  
    public void SavePlayerData(List<PlayerData> data)
    {
        repository.SavePlayerData(data);
    }
}
