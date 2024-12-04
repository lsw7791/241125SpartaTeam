using System.Collections.Generic;
using System;

[Serializable]
public class InventoryData
{
    public List<string> ItemIDs;  // 아이템 ID 리스트

    public InventoryData()
    {
        ItemIDs = new List<string>();
    }

    // Inventory 객체에서 InventoryData로 변환
    public static InventoryData FromInventory(Inventory inventory)
    {
        InventoryData data = new InventoryData();
        foreach (var item in inventory.Items)
        {
            data.ItemIDs.Add(item.ItemID);  // 각 아이템의 ID를 추가
        }
        return data;
    }

    // InventoryData 객체에서 Inventory로 변환
    public static Inventory ToInventory(InventoryData data)
    {
        Inventory inventory = new Inventory();
        foreach (var itemID in data.ItemIDs)
        {
            inventory.AddItem(itemID, "ItemName", 1, "ItemType", null);  // itemSprite에 기본값 null 설정
        }
        return inventory;
    }

}
