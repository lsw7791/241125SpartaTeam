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
        UnityGoogleSheet.LoadAllData();
        item = new ItemDataManager();
        creature = new CreatureDataManager();
        crafting = new CraftingDataManager();
    }


    // ������ ID�� ���� ������ ������ ��ȸ
    public ItemData GetItemDataById(int itemId)
    {
        Debug.Log($"Requesting ItemData for ID: {itemId}");
        var itemData = item.GetItemDataById(itemId);

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
