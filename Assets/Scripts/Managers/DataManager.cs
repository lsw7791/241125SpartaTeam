using MainData;
using System.Collections.Generic;
using UGS;
using UnityEngine;
using System.IO;

public class DataManager
{
    public PlayerData nowPlayer = new PlayerData();

    public string path; // 경로
    public int nowSlot; // 현재 슬롯번호

    public ItemDataManager Item;               // 아이템 데이터 관리
    public CreatureDataManager Creature;       // 몬스터 데이터 관리
    public CraftingDataManager Crafting;       // 제작 데이터 관리
    public SceneDataManager Scene;             // 씬 데이터 관리
    public ShopManager Shop;                   // 상점 관리
    public UpGradeDataManager Upgrade;       // 제작 데이터 관리
    public MineSpawnDataManager MineSpawn;
    public MonsterSpawnDataManager MonsterSpawn;
    public StatDataManager PlayerStat;
    public UIDataManager UIDataManager;
    public MainQuest MainQuest;

    public DataManager()
    {
        UnityGoogleSheet.LoadAllData();          // 구글 시트 데이터 로드
        Item = new ItemDataManager();
        Creature = new CreatureDataManager();
        Crafting = new CraftingDataManager();
        Scene = new SceneDataManager();
        Shop = new ShopManager();
        Upgrade = new UpGradeDataManager();
        MineSpawn = new MineSpawnDataManager();
        MonsterSpawn = new MonsterSpawnDataManager();
        PlayerStat = new StatDataManager();
        UIDataManager= new UIDataManager();
        MainQuest = new MainQuest();
        path = Application.persistentDataPath + "/save";
    }

    // 특정 아이템 데이터 조회
    public ItemData GetItemDataById(int itemId)
    {
        var itemData = Item.GetData(itemId);
        if (itemData == null)
        {
            Debug.LogWarning($"아이템 ID {itemId}에 해당하는 데이터를 찾을 수 없습니다.");
        }
        return itemData;
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(GameManager.Instance.GetCurrentCharacter());
        File.WriteAllText(path + nowSlot.ToString(), data);
        Debug.Log($"저장 경로: {path + nowSlot.ToString()}");
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        Debug.Log($"저장 경로: {path + nowSlot.ToString()}");
    }
    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
