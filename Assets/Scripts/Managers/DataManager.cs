using MainData;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataManager
{
    public ItemDataManager Item;
    public CreatureDataManager Creature;
    public CraftingDataManager Crafting;
    public SceneDataManager Scene;
    public ShopManager Shop;
    public CharacterList CharacterList;

    public DataManager(IPlayerRepository repository)
    {
        // ������ �ε�
        UnityGoogleSheet.LoadAllData();

        // �Ŵ��� �ʱ�ȭ
        Item = new ItemDataManager();
        Creature = new CreatureDataManager();
        Crafting = new CraftingDataManager();
        Scene = new SceneDataManager();
        Shop = new ShopManager();

        // CharacterList �ʱ�ȭ (repository ���)
        CharacterList = new CharacterList(repository);
    }

    // ������ ID�� ���� ������ ������ ��ȸ
    public ItemData GetItemDataById(int itemId)
    {
        var itemData = Item.GetData(itemId);
        return itemData;
    }

    // ��� �÷��̾� �����͸� ��������
    public List<PlayerData> GetAllPlayerData()
    {
        return CharacterList.GetAllLists();
    }
}
