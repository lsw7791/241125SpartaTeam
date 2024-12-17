using MainData;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataManager
{
    public ItemDataManager Item;              // ������ ������ ����
    public CreatureDataManager Creature;      // ���� ������ ����
    public CraftingDataManager Crafting;      // ���� ������ ����
    public SceneDataManager Scene;            // �� ������ ����
    public ShopManager Shop;                  // ���� ����
    public CharacterList CharacterList;       // ĳ���� ����Ʈ ����

    public IPlayerRepository Repository;    // �÷��̾� ������ ����� �������̽�

    // �����ڿ��� Repository�� ���޹޾� �ʱ�ȭ
    public DataManager()
    {
        // FilePlayerRepository�� �⺻������ ���
        Repository = new FilePlayerRepository();

        // ���� ��Ʈ �����͸� �ε�
        UnityGoogleSheet.LoadAllData();

        // ������ �Ŵ��� �ʱ�ȭ
        Item = new ItemDataManager();
        Creature = new CreatureDataManager();
        Crafting = new CraftingDataManager();
        Scene = new SceneDataManager();
        Shop = new ShopManager();

        // CharacterList �ʱ�ȭ (Repository�� ���ڷ� ����)
        CharacterList = new CharacterList(Repository);
    }

    // ��� �÷��̾� �����͸� ��������
    public List<PlayerData> GetAllPlayerData()
    {
        var playerData = CharacterList.GetAllCharacters();
        Debug.Log($"��ü ĳ���� ��: {playerData.Count}");
        return playerData;
    }


    // ���ο� ĳ���� �߰�
    public void AddCharacter(PlayerData newCharacter)
    {
        CharacterList.AddCharacter(newCharacter);
    }

    // ĳ���� ������ ����
    public void SaveCharacterData()
    {
        CharacterList.SaveListsData();
    }

    // ĳ���� ������ �ε�
    public void LoadCharacterData()
    {
        CharacterList.LoadAllCharacters();
    }

    // Ư�� ������ ������ ��ȸ
    public ItemData GetItemDataById(int itemId)
    {
        var itemData = Item.GetData(itemId);
        return itemData;
    }

    // Ư�� ���� ������ ��ȸ
    public CreatureData GetCreatureDataById(int creatureId)
    {
        var creatureData = Creature.GetData(creatureId);
        return creatureData;
    }
}
