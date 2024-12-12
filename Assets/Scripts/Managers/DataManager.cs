using MainData;
using UGS;
using UnityEngine;

public class DataManager
{
    public ItemDataManager item;
    public CreatureDataManager creature;
    public CraftingDataManager crafting;
    
    public DataManager()
    {
        Debug.Log("Initializing ItemDataManager...");
        // 데이터 로드
        UnityGoogleSheet.LoadAllData();
        item = new ItemDataManager();
        creature = new CreatureDataManager();
        crafting = new CraftingDataManager();
        Debug.Log("ItemDataManager initialized.");
    }


    // 아이템 ID를 통해 아이템 데이터 조회
    public ItemData GetItemDataById(int itemId)
    {
        Debug.Log($"Requesting ItemData for ID: {itemId}");
        var itemData = item.GetData(itemId);

        if (itemData == null)
        {
            Debug.LogWarning($"Item with ID {itemId} not found in ItemDataManager.");
        }
        else
        {
            Debug.Log($"Found ItemData for ID {itemId}: {itemData.name}");
        }

        return itemData;
    }

}
