using MainData;
using UGS;
using UnityEngine;

public class DataManager : SingleTon<DataManager>
{
    public ItemDataManager item;
    public CreatureDataManager creature;
    public CraftingDataManager crafting;

    public void Initialize()
    {

        // 데이터 로드
        UnityGoogleSheet.LoadAllData();
        item = new ItemDataManager();
        creature = new CreatureDataManager();
        crafting = new CraftingDataManager();

    }


    // 아이템 ID를 통해 아이템 데이터 조회
    public ItemData GetItemDataById(int itemId)
    {
        var itemData = item.GetItemDataById(itemId);
        return itemData;
    }

}
