using MainData;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataManager
{
    public ItemDataManager Item;               // ������ ������ ����
    public CreatureDataManager Creature;       // ���� ������ ����
    public CraftingDataManager Crafting;       // ���� ������ ����
    public SceneDataManager Scene;             // �� ������ ����
    public ShopManager Shop;                   // ���� ����
    public UpGradeDataManager Upgrade;       // ���� ������ ����
    public CharacterList CharacterList;        // ĳ���� ����Ʈ ����
    public MineSpawnDataManager MineSpawn;
    public MonsterSpawnDataManager MonsterSpawn;
    public StatDataManager PlayerStat;
    public IPlayerRepository Repository;       // �÷��̾� ������ ����� �������̽�
   public UIDataManager UIDataManager;
    public DataManager()
    {
        Repository = new FilePlayerRepository();  // repository�� �ܺο��� ���޹޵��� ����
        UnityGoogleSheet.LoadAllData();          // ���� ��Ʈ ������ �ε�
        Repository.Initialize();
        Item = new ItemDataManager();
        Creature = new CreatureDataManager();
        Crafting = new CraftingDataManager();
        Scene = new SceneDataManager();
        Shop = new ShopManager();
        Upgrade = new UpGradeDataManager();
        CharacterList = new CharacterList(Repository);  // CharacterList�� repository�� ����Ͽ� �ʱ�ȭ
        MineSpawn = new MineSpawnDataManager();
        MonsterSpawn = new MonsterSpawnDataManager();
        PlayerStat = new StatDataManager();
        UIDataManager= new UIDataManager();
}

// ��� �÷��̾� �����͸� ��������
public List<PlayerData> GetAllPlayerData()
    {
        return CharacterList.GetAllCharacters();
    }

    // ���ο� ĳ���� �߰�
    public void AddCharacter(PlayerData newCharacter)
    {
        CharacterList.AddCharacter(newCharacter);
    }

    // ĳ���� ������ ����
    public void SaveCharacterData()
    {
        CharacterList.SaveListsData();  // ĳ���� ����Ʈ ������ ����
    }

    // ĳ���� ������ �ε�
    public void LoadCharacterData()
    {
        CharacterList.LoadAllCharacters();
    }

    // PlayerPrefs���� ������ ĳ���� ������ �ҷ�����
    public PlayerData GetLastPlayedCharacter()
    {
        string lastNickName = PlayerPrefs.GetString("LastCharacterNickName", "");
        if (string.IsNullOrEmpty(lastNickName)) return null;

        List<PlayerData> allPlayers = CharacterList.GetAllCharacters();
        return allPlayers.Find(player => player.NickName == lastNickName);
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
}
