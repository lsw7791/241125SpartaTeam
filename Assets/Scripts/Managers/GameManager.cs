using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public DataManager DataManager; // DataManager만 사용하도록 변경
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

        // 다른 매니저 초기화
        if (CraftingManager == null) CraftingManager = gameObject.AddComponent<CraftingManager>();
        if (rewardedAds == null) rewardedAds = gameObject.GetComponentInChildren<RewardedAds>();

        SceneNum = 24;
    }

    void Start()
    {
        // 씬 로딩 후 호출되는 이벤트
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 마지막으로 저장된 캐릭터 로드
        _currentPlayer = DataManager.GetLastPlayedCharacter();
        if (_currentPlayer != null)
        {
            Debug.Log($"마지막으로 선택된 캐릭터: {_currentPlayer.NickName}");
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
        // 씬 전환
        SceneManager.LoadScene(sceneName);
    }

    public PlayerData GetCurrentCharacter()
    {
        return _currentPlayer;
    }

    // 캐릭터 선택 후 게임 시작
    public void StartGame(PlayerData character)
    {
        _currentPlayer = character;
        Debug.Log($"게임 시작: {_currentPlayer.NickName}");

        // 선택된 캐릭터의 데이터를 게임에 로드
        LoadPlayerData(character);

        // 씬을 전환하여 게임 시작
        SceneNum = 2;
        LoadScene(DataManager.Scene.GetMapTo(SceneNum));
    }

    // 플레이어 데이터 로드
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
