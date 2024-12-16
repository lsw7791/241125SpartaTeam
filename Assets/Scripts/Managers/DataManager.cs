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
        // 데이터 로드
        UnityGoogleSheet.LoadAllData();

        // 매니저 초기화
        Item = new ItemDataManager();
        Creature = new CreatureDataManager();
        Crafting = new CraftingDataManager();
        Scene = new SceneDataManager();
        Shop = new ShopManager();

        // CharacterList 초기화
        CharacterList = new CharacterList();
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

    // 새로운 캐릭터 추가
    public void AddCharacter(PlayerData newCharacter)
    {
        CharacterList.AddCharacter(newCharacter); // CharacterList에서 관리
    }

    // 캐릭터 데이터를 저장
    public void SaveCharacterData()
    {
        CharacterList.SaveListsData();
    }

    // 캐릭터 데이터를 로드
    public void LoadCharacterData()
    {
        CharacterList.LoadListsData();
    }
}
