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

    // CharacterList�� GameManager���� ����

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("GameManager Awake ȣ��");

        // FilePlayerRepository�� ����� CharacterList �ʱ�ȭ
        repository = new FilePlayerRepository();
        // ������ �Ŵ��� �ʱ�ȭ
        DataManager = new DataManager(repository);
        Debug.Log("CharacterList ��ü ���� ��");

        // �ٸ� �Ŵ��� �ʱ�ȭ
        if (CraftingManager == null) CraftingManager = gameObject.AddComponent<CraftingManager>();
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
