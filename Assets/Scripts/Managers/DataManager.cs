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
        // ������ �ε�
        UnityGoogleSheet.LoadAllData();
        item = new ItemDataManager();
        creature = new CreatureDataManager();
        crafting = new CraftingDataManager();
        scene = new SceneDataManager();
        shop = new ShopManager();
    }


    // ������ ID�� ���� ������ ������ ��ȸ
    public ItemData GetItemDataById(int itemId)
    {
        var itemData = item.GetData(itemId);

        return itemData;
    }
    
}
