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
    public GameObject miniCamera;
    public RewardedAds rewardedAds;
    public IInteractable InteractableObject { get; set; }

    protected override void Awake()
    {
        base.Awake();
        DataManager = new DataManager();  

        // �ٸ� �Ŵ��� �ʱ�ȭ
        if (CraftingManager == null) CraftingManager = gameObject.AddComponent<CraftingManager>();
        if (rewardedAds == null) rewardedAds = gameObject.GetComponentInChildren<RewardedAds>();

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
        miniCamera = Instantiate(Resources.Load<GameObject>("Prefabs/Cameras/MinimapCamera"));
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

        // ���� ��ȯ�Ͽ� ���� ����
        SceneNum = 2;
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
        Player.stats.Gold = playerData.Gold;

    }
}
