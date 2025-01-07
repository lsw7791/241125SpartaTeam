using MainData;
using UGS;
using UnityEngine;
using System.IO;

public class DataManager
{
    public string savePath; // ���
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
        savePath = Application.persistentDataPath;
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

    public void SaveData<T>(T inData)
    {
        string json = JsonUtility.ToJson(inData);
        File.WriteAllText(savePath + $"/{typeof(T).ToString()}{nowSlot.ToString()}.txt", json);
    }

    public T GetLoadData<T>()
    {
        if (IsData<T>())
        {
            string outLoadJson = File.ReadAllText(savePath + $"/{typeof(T).ToString()}{nowSlot.ToString()}.txt");
            return JsonUtility.FromJson<T>(outLoadJson);
        }
        else
        {
            return default(T);
        }
    }

    public void DataClear()
    {
        nowSlot = -1;
        GameManager.Instance.nowPlayer = new PlayerData();
        GameManager.Instance.nowInventory = new Inventory();
        //GameManager.Instance.nowEquipment = new Equipment();
    }

    public void DeleteData<T>()
    {
        if (IsData<T>())
        {
            File.Delete(savePath + $"/{typeof(T).ToString()}{nowSlot.ToString()}.txt");
        }
    }

    public bool IsData<T>()
    {
        if (File.Exists(savePath + $"/{typeof(T).ToString()}{nowSlot.ToString()}.txt"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
