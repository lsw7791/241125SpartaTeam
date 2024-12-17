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

    public DataManager()
    {
        // ������ �ε�
        UnityGoogleSheet.LoadAllData();

        // �Ŵ��� �ʱ�ȭ
        Item = new ItemDataManager();
        Creature = new CreatureDataManager();
        Crafting = new CraftingDataManager();
        Scene = new SceneDataManager();
        Shop = new ShopManager();

        // CharacterList �ʱ�ȭ
        CharacterList = new CharacterList();
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

    // ���ο� ĳ���� �߰�
    public void AddCharacter(PlayerData newCharacter)
    {
        CharacterList.AddCharacter(newCharacter); // CharacterList���� ����
    }

    // ĳ���� �����͸� ����
    public void SaveCharacterData()
    {
        CharacterList.SaveListsData();
    }

    // ĳ���� �����͸� �ε�
    public void LoadCharacterData()
    {
        CharacterList.LoadListsData();
    }
}
