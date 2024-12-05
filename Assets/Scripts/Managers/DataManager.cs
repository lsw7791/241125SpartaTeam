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

        // ������ �ε�
        UnityGoogleSheet.LoadAllData();
        item = new ItemDataManager();
        creature = new CreatureDataManager();
        crafting = new CraftingDataManager();

    }


    // ������ ID�� ���� ������ ������ ��ȸ
    public ItemData GetItemDataById(int itemId)
    {
        var itemData = item.GetItemDataById(itemId);
        return itemData;
    }

}
