using MainData;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataManager
{
    public ItemDataManager Item;               // 아이템 데이터 관리
    public CreatureDataManager Creature;       // 몬스터 데이터 관리
    public CraftingDataManager Crafting;       // 제작 데이터 관리
    public SceneDataManager Scene;             // 씬 데이터 관리
    public ShopManager Shop;                   // 상점 관리
    public UpGradeDataManager Upgrade;       // 제작 데이터 관리
    public CharacterList CharacterList;        // 캐릭터 리스트 관리
    public MineSpawnDataManager MineSpawn;
    public MonsterSpawnDataManager MonsterSpawn;
    public StatDataManager PlayerStat;
    public IPlayerRepository Repository;       // 플레이어 데이터 저장소 인터페이스
   public UIDataManager UIDataManager;
    public DataManager()
    {
        Repository = new FilePlayerRepository();  // repository를 외부에서 전달받도록 수정
        UnityGoogleSheet.LoadAllData();          // 구글 시트 데이터 로드
        Repository.Initialize();
        Item = new ItemDataManager();
        Creature = new CreatureDataManager();
        Crafting = new CraftingDataManager();
        Scene = new SceneDataManager();
        Shop = new ShopManager();
        Upgrade = new UpGradeDataManager();
        CharacterList = new CharacterList(Repository);  // CharacterList도 repository를 사용하여 초기화
        MineSpawn = new MineSpawnDataManager();
        MonsterSpawn = new MonsterSpawnDataManager();
        PlayerStat = new StatDataManager();
        UIDataManager= new UIDataManager();
}

// 모든 플레이어 데이터를 가져오기
public List<PlayerData> GetAllPlayerData()
    {
        return CharacterList.GetAllCharacters();
    }

    // 새로운 캐릭터 추가
    public void AddCharacter(PlayerData newCharacter)
    {
        CharacterList.AddCharacter(newCharacter);
    }

    // 캐릭터 데이터 저장
    public void SaveCharacterData()
    {
        CharacterList.SaveListsData();  // 캐릭터 리스트 데이터 저장
    }

    // 캐릭터 데이터 로드
    public void LoadCharacterData()
    {
        CharacterList.LoadAllCharacters();
    }

    // PlayerPrefs에서 마지막 캐릭터 데이터 불러오기
    public PlayerData GetLastPlayedCharacter()
    {
        string lastNickName = PlayerPrefs.GetString("LastCharacterNickName", "");
        if (string.IsNullOrEmpty(lastNickName)) return null;

        List<PlayerData> allPlayers = CharacterList.GetAllCharacters();
        return allPlayers.Find(player => player.NickName == lastNickName);
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
}
