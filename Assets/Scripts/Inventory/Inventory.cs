using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Inventory
{
    public List<InventoryItem> Items; // ������ ���
    public event Action OnInventoryChanged; // �κ��丮 ���� �̺�Ʈ

    public Inventory()
    {
        Items = new List<InventoryItem>();
    }

    // ������ �߰� �޼���
    public void AddItem(string itemID, string itemName, int quantity, string itemType, Sprite itemSprite)
    {
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            Items.Add(new InventoryItem(itemID, itemName, quantity, itemType, itemSprite));
        }

        OnInventoryChanged?.Invoke();
    }


    // ������ ��ȸ �޼��� �߰�
    public InventoryItem GetItem(string itemID)
    {
        return Items.Find(item => item.ItemID == itemID); // �ش� ID�� ������ ��ȯ
    }

    // ������ ��� ��ȯ
    public List<InventoryItem> GetItems()
    {
        return Items;
    }

    // ������ ���� �޼���
    public void RemoveItem(string itemID, int quantity)
    {
        InventoryItem item = Items.Find(i => i.ItemID == itemID);
        if (item != null)
        {
            item.Quantity -= quantity;
            if (item.Quantity <= 0)
                Items.Remove(item);
        }
        OnInventoryChanged?.Invoke();
    }
}
