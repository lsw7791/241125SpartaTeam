using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public DataManager DataManager; // DataManager�� ����ϵ��� ����
    public CraftingManager CraftingManager;
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
        SoundManager.Instance.PlayStartBGMMystical();
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

    // ĳ���� ���� �� ���� ����
    public void StartGame(PlayerData character)
    {
        _currentPlayer = character;
        Debug.Log($"���� ����: {_currentPlayer.NickName}");

        // ���õ� ĳ������ �����͸� ���ӿ� �ε�
        LoadPlayerData(character);

        // ĳ���� ���� �� �����͸� ��� ����
        DataManager.SaveCharacterData();

        // ���� ��ȯ�Ͽ� ���� ����
        SceneNum = 23;
        LoadScene(DataManager.Scene.GetMapTo(SceneNum));
    }

    // �÷��̾� ������ �ε�
    private void LoadPlayerData(PlayerData playerData)
    {
        Player.nickName = playerData.NickName;
        Player.Stats.MaxHP = playerData.MaxHP;   // MaxHP
        Player.Stats.CurrentHP = playerData.CurrentHP;   // CurrentHP
        Player.Stats.Damage = playerData.Damage;   // Damage
        Player.Stats.MoveSpeed = playerData.MoveSpeed;   // Speed
        Player.Stats.Def = playerData.Def;   // Defense
        Player.Stats.ATKSpeed = playerData.ATKSpeed;   // AttackSpeed
        Player.Stats.WeaponType = playerData.WeaponType;   // WeaponType

        // �κ��丮 �ε�
        LoadInventory(playerData);
        LoadQuickSlots(playerData);
    }

    // �κ��丮 �ε�
    private void LoadInventory(PlayerData playerData)
    {
        Player.Inventory.Clear();  // ���� �������� ����
        foreach (var item in playerData.InventoryItems) // InventoryItems�� ����
        {
            Player.AddItemToInventory(item.ItemID, item.Quantity, item.SpritePath);
        }
    }

    // ������ �ε�
    private void LoadQuickSlots(PlayerData playerData)
    {
        Player.QuickSlots.Slots.Clear();  // ���� ������ ����
        foreach (var item in playerData.QuickSlotItems)
        {
            Player.QuickSlots.AddQuickSlotItem(item.ItemID, item.SlotIndex);
        }
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
