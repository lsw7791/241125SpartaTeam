using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    public DataManager DataManager; // DataManager만 사용하도록 변경
    public CraftingManager CraftingManager;
    public SpawnManager SpawnManager;
    public Player Player;
    public int SceneNum;
    public GameObject miniCamera;
    //public RewardedAds rewardedAds;
    public IInteractable InteractableObject { get; set; }
    public AttackEffect attackEffect;

    public PlayerData nowPlayer = new PlayerData();
    // 저장 경로를 통해 가져온 현재 유저 데이터
    public Inventory nowInventory = new Inventory();
    // 저장 경로를 통해 가져온 현재 인벤토리 데이터
    //public Equipment nowEquipment = new Equipment();
    // 저장 경로를 통해 가져온 현재 장착 데이터

    protected override void Awake()
    {
        base.Awake();
        DataManager = new DataManager();  

        // 다른 매니저 초기화
        if (CraftingManager == null) CraftingManager = gameObject.AddComponent<CraftingManager>();
        //if (rewardedAds == null) rewardedAds = gameObject.GetComponentInChildren<RewardedAds>();

        SceneNum = 24;
        attackEffect = GetComponentInChildren<AttackEffect>();
    }

    void Start()
    {
        // 씬 로딩 후 호출되는 이벤트
        SceneManager.sceneLoaded += OnSceneLoaded;

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

    // 캐릭터 선택 후 게임 시작
    public void StartGame()
    {
        Player.stats = nowPlayer;
        Player.inventory = nowInventory;
        //Player.equipment = nowEquipment;

        CharacterSelectUI selectUI = UIManager.Instance.GetUI<CharacterSelectUI>();
        if (selectUI.isNewGame)
        {
            Player.stats.Initialize();
            nowPlayer.CurrentQuestId = DataManager.MainQuest.CurrentQuestId;

            Player.inventory.SaveEquipInIt();

            // 씬을 전환하여 게임 시작
            SceneNum = 2;
            LoadScene(DataManager.Scene.GetMapTo(SceneNum));

            return;
        }

        Player.inventory.SaveEquipInIt();

        // 씬을 전환하여 게임 시작
        SceneNum = nowPlayer.nowMapNumber;
        LoadScene(DataManager.Scene.GetMapTo(SceneNum));

        Player.playerNameText.text = Player.stats.NickName;
    }
}
