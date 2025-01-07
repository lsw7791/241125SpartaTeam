using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : MonoSingleton<GameManager>
{
    public DataManager DataManager; // DataManager�� ����ϵ��� ����
    public CraftingManager CraftingManager;
    public SpawnManager SpawnManager;
    public Player Player;
    public int SceneNum;
    public GameObject miniCamera;
    //public RewardedAds rewardedAds;
    public IInteractable InteractableObject { get; set; }
    public AttackEffect attackEffect;

    public PlayerData nowPlayer = new PlayerData();
    // ���� ��θ� ���� ������ ���� ���� ������
    public Inventory nowInventory = new Inventory();
    // ���� ��θ� ���� ������ ���� �κ��丮 ������
    //public Equipment nowEquipment = new Equipment();
    // ���� ��θ� ���� ������ ���� ���� ������

    protected override void Awake()
    {
        base.Awake();
        DataManager = new DataManager();  

        // �ٸ� �Ŵ��� �ʱ�ȭ
        if (CraftingManager == null) CraftingManager = gameObject.AddComponent<CraftingManager>();
        //if (rewardedAds == null) rewardedAds = gameObject.GetComponentInChildren<RewardedAds>();

        SceneNum = 24;
        attackEffect = GetComponentInChildren<AttackEffect>();
    }

    void Start()
    {
        // �� �ε� �� ȣ��Ǵ� �̺�Ʈ
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
        // �� ��ȯ
        SceneManager.LoadScene(sceneName);
    }

    // ĳ���� ���� �� ���� ����
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

            // ���� ��ȯ�Ͽ� ���� ����
            SceneNum = 2;
            LoadScene(DataManager.Scene.GetMapTo(SceneNum));

            return;
        }

        Player.inventory.SaveEquipInIt();

        // ���� ��ȯ�Ͽ� ���� ����
        SceneNum = nowPlayer.nowMapNumber;
        LoadScene(DataManager.Scene.GetMapTo(SceneNum));

        Player.playerNameText.text = Player.stats.NickName;
    }
}
