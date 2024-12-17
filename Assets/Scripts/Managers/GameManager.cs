using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

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

        // DataManager �ʱ�ȭ
        DataManager = new DataManager();  // DataManager�� �⺻ �����ڷ� �ʱ�ȭ
        Debug.Log("DataManager ��ü ���� ��");

        // �ٸ� �Ŵ��� �ʱ�ȭ
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

        // ĳ���� ���� �� �����͸� ��� ����
        DataManager.SaveCharacterData();

        GameManager.Instance.SceneNum = 2;
        GameManager.Instance.LoadScene(GameManager.Instance.DataManager.Scene.GetMapTo(GameManager.Instance.SceneNum));
    }

    // ĳ���� ���� ���� �ε�
   
    // ���� ���� �� �÷��̾� ���� ����
    public void SavePlayerData()
    {
        if (_currentPlayer != null)
        {
            DataManager.SaveCharacterData();  // ���� �÷��̾��� �����͸� ����
        }
    }
}
