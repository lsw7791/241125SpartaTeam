using MainData;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataManager
{
    public ItemDataManager item;
    public CreatureDataManager creature;
    public CraftingDataManager crafting;
    public SceneDataManager scene;
    public ShopManager shop;
    public DataManager()
    {
        // 데이터 로드
        UnityGoogleSheet.LoadAllData();
        item = new ItemDataManager();
        creature = new CreatureDataManager();
        crafting = new CraftingDataManager();
        scene = new SceneDataManager();
        shop = new ShopManager();
    }


    // 아이템 ID를 통해 아이템 데이터 조회
    public ItemData GetItemDataById(int itemId)
    {
        var itemData = item.GetData(itemId);

        return itemData;
    }
    
}
