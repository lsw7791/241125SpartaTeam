using MainData;
using System.Collections.Generic;
using UGS;
using UnityEngine;
using System.IO;

public class DataManager
{
    public PlayerData nowPlayer = new PlayerData();

    public string path; // ���
    public int nowSlot; // ���� ���Թ�ȣ

    public ItemDataManager Item;               // ������ ������ ����
    public CreatureDataManager Creature;       // ���� ������ ����
    public CraftingDataManager Crafting;       // ���� ������ ����
    public SceneDataManager Scene;             // �� ������ ����
    public ShopManager Shop;                   // ���� ����
    public UpGradeDataManager Upgrade;       // ���� ������ ����
    public MineSpawnDataManager MineSpawn;
    public MonsterSpawnDataManager MonsterSpawn;
    public StatDataManager PlayerStat;
    public UIDataManager UIDataManager;
    public MainQuest MainQuest;

    public DataManager()
    {
        UnityGoogleSheet.LoadAllData();          // ���� ��Ʈ ������ �ε�
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

    // Ư�� ������ ������ ��ȸ
    public ItemData GetItemDataById(int itemId)
    {
        var itemData = Item.GetData(itemId);
        if (itemData == null)
        {
            Debug.LogWarning($"������ ID {itemId}�� �ش��ϴ� �����͸� ã�� �� �����ϴ�.");
        }
        return itemData;
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(GameManager.Instance.GetCurrentCharacter());
        File.WriteAllText(path + nowSlot.ToString(), data);
        Debug.Log($"���� ���: {path + nowSlot.ToString()}");
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        Debug.Log($"���� ���: {path + nowSlot.ToString()}");
    }
    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
