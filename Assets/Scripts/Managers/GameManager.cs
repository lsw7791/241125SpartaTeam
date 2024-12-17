using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public DataManager DataManager; // DataManager�� ����ϵ��� ����
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
        Debug.Log("GameManager Awake ȣ��");

        // Repository �ʱ�ȭ
        IPlayerRepository repository = new FilePlayerRepository();
        repository.Initialize();

        // DataManager �ʱ�ȭ
        DataManager = new DataManager(repository);  // DataManager�� �����ڿ��� repository ����
        Debug.Log("DataManager ��ü ���� ��");

        // �ٸ� �Ŵ��� �ʱ�ȭ
        if (CraftingManager == null) CraftingManager = gameObject.AddComponent<CraftingManager>();
        List<ItemInstance> items = new List<ItemInstance>();
        ItemManager.Instance.Initialize(items);

        SceneNum = 24;
    }

    void Start()
    {
        // �� �ε� �� ȣ��Ǵ� �̺�Ʈ
        SceneManager.sceneLoaded += OnSceneLoaded;

        // ���������� ����� ĳ���� �ε�
        _currentPlayer = DataManager.GetLastPlayedCharacter();
        if (_currentPlayer != null)
        {
            Debug.Log($"���������� ���õ� ĳ����: {_currentPlayer.NickName}");
        }

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
        UIManager.Instance.ToggleUI<CharacterSlotUI>();

        _currentPlayer = character;
        Debug.Log($"���� ����: {_currentPlayer.NickName}");

        // ĳ���� ���� �� �����͸� ��� ����
        DataManager.SaveCharacterData();

        GameManager.Instance.SceneNum = 2;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }

    // ���� ���� �� �÷��̾� ���� ����
    public void SavePlayerData()
    {
        if (_currentPlayer != null)
        {
            DataManager.SaveCharacterData();  // ���� �÷��̾��� �����͸� ����
            PlayerPrefs.SetString("LastCharacterNickName", _currentPlayer.NickName);
            Debug.Log("�÷��̾� ������ ���� �Ϸ�");
        }
    }
}
