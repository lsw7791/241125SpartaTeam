using MainData;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataManager
{
    public ItemDataManager Item;              // 아이템 데이터 관리
    public CreatureDataManager Creature;      // 몬스터 데이터 관리
    public CraftingDataManager Crafting;      // 제작 데이터 관리
    public SceneDataManager Scene;            // 씬 데이터 관리
    public ShopManager Shop;                  // 상점 관리
    public CharacterList CharacterList;       // 캐릭터 리스트 관리

    public IPlayerRepository Repository;    // 플레이어 데이터 저장소 인터페이스

    // 생성자에서 Repository를 전달받아 초기화
    public DataManager()
    {
        // FilePlayerRepository를 기본값으로 사용
        Repository = new FilePlayerRepository();

        // 구글 시트 데이터를 로드
        UnityGoogleSheet.LoadAllData();

        // 데이터 매니저 초기화
        Item = new ItemDataManager();
        Creature = new CreatureDataManager();
        Crafting = new CraftingDataManager();
        Scene = new SceneDataManager();
        Shop = new ShopManager();

        // CharacterList 초기화 (Repository를 인자로 전달)
        CharacterList = new CharacterList(Repository);
    }

    // 모든 플레이어 데이터를 가져오기
    public List<PlayerData> GetAllPlayerData()
    {
        var playerData = CharacterList.GetAllCharacters();
        Debug.Log($"전체 캐릭터 수: {playerData.Count}");
        return playerData;
    }


    // 새로운 캐릭터 추가
    public void AddCharacter(PlayerData newCharacter)
    {
        CharacterList.AddCharacter(newCharacter);
    }

    // 캐릭터 데이터 저장
    public void SaveCharacterData()
    {
        CharacterList.SaveListsData();
    }

    // 캐릭터 데이터 로드
    public void LoadCharacterData()
    {
        CharacterList.LoadAllCharacters();
    }

    // 특정 아이템 데이터 조회
    public ItemData GetItemDataById(int itemId)
    {
        var itemData = Item.GetData(itemId);
        return itemData;
    }

    // 특정 몬스터 데이터 조회
    public CreatureData GetCreatureDataById(int creatureId)
    {
        var creatureData = Creature.GetData(creatureId);
        return creatureData;
    }
}
