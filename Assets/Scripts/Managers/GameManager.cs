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

    // CharacterList�� GameManager���� ����

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("GameManager Awake ȣ��");

        // FilePlayerRepository�� ����� CharacterList �ʱ�ȭ
        Repository = new FilePlayerRepository();
        // ������ �Ŵ��� �ʱ�ȭ
        DataManager = new DataManager();
        Debug.Log("CharacterList ��ü ���� ��");

        // �ٸ� �Ŵ��� �ʱ�ȭ
        if (CraftingManager == null) CraftingManager = gameObject.AddComponent<CraftingManager>();
        List<ItemInstance> items = new List<ItemInstance>();
        ItemManager.Instance.Initialize(items);

        SceneNum = 24;
    }

    void Start()
    {
        // FilePlayerRepository �ʱ�ȭ�� Start()���� ȣ��
        if (Repository is FilePlayerRepository fileRepo)
        {
            Debug.Log("FilePlayerRepository Initialize ȣ��");
            fileRepo.Initialize();  // Initialize ȣ��
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
        GameManager.Instance.SceneNum = 26;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }

    // ������ ���� �޼���
    //public void SavePlayerData(List<PlayerData> data)
    //{
    //    Repository.SavePlayerData(data);
    //}
}
