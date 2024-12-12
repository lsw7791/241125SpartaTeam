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
        // ������ �ε�
        UnityGoogleSheet.LoadAllData();
        item = new ItemDataManager();
        creature = new CreatureDataManager();
        crafting = new CraftingDataManager();
        Debug.Log("ItemDataManager initialized.");
    }


    // ������ ID�� ���� ������ ������ ��ȸ
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
