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
        // 데이터 로드
        UnityGoogleSheet.LoadAllData();

        // 매니저 초기화
        Item = new ItemDataManager();
        Creature = new CreatureDataManager();
        Crafting = new CraftingDataManager();
        Scene = new SceneDataManager();
        Shop = new ShopManager();

        // CharacterList 초기화 (repository 사용)
        CharacterList = new CharacterList(repository);
    }

    // 아이템 ID를 통해 아이템 데이터 조회
    public ItemData GetItemDataById(int itemId)
    {
        var itemData = Item.GetData(itemId);
        return itemData;
    }

    // 모든 플레이어 데이터를 가져오기
    public List<PlayerData> GetAllPlayerData()
    {
        return CharacterList.GetAllLists();
    }
}
