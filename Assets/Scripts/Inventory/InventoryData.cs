using System.Collections.Generic;
using System;

[Serializable]
public class InventoryData
{
    public List<string> ItemIDs;  // ������ ID ����Ʈ

    public InventoryData()
    {
        ItemIDs = new List<string>();
    }

    // Inventory ��ü���� InventoryData�� ��ȯ
    public static InventoryData FromInventory(Inventory inventory)
    {
        InventoryData data = new InventoryData();
        foreach (var item in inventory.Items)
        {
            data.ItemIDs.Add(item.ItemID);  // �� �������� ID�� �߰�
        }
        return data;
    }

    // InventoryData ��ü���� Inventory�� ��ȯ
    public static Inventory ToInventory(InventoryData data)
    {
        Inventory inventory = new Inventory();
        foreach (var itemID in data.ItemIDs)
        {
            inventory.AddItem(itemID, "ItemName", 1, "ItemType", null);  // itemSprite�� �⺻�� null ����
        }
        return inventory;
    }

}
