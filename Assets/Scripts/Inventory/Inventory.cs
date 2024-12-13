using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Inventory
{
    public List<InventoryItem> Items; // �÷��̾��� �κ��丮 ������ ���
    public event Action OnInventoryChanged; // �κ��丮 ���� �̺�Ʈ

    public Inventory()
    {
        Items = new List<InventoryItem>();
    }

    // ������ �߰� �޼���
    public void AddItem(int itemID, string itemName, int quantity, ItemType itemType, Sprite itemSprite)
    {
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);
        // ������ itemID�� ���� ������ Ž��
        if (existingItem != null && (int)itemType > 10)
        { // �κ��丮 ���� ������ itemID�� �ְ� ��� �������� �ƴ϶��
            existingItem.Quantity += quantity;
            // ã�� �������� �ִٸ� ���� ����
        }
        else
        {
            Items.Add(new InventoryItem(itemID, itemName, quantity, itemType, itemSprite));
            // ������ itemID ���ų� ��� �������̶�� ����Ʈ�� �߰�
        }

        OnInventoryChanged?.Invoke();
    }


    // ������ ��ȸ �޼��� �߰�
    public InventoryItem GetItem(int itemID)
    {
        return Items.Find(item => item.ItemID == itemID); // �ش� ID�� ������ ��ȯ
    }

    // ������ ��� ��ȯ
    public List<InventoryItem> GetItems()
    {
        return Items;
    }

    // ������ ���� �޼���
    public void RemoveItem(int itemID, int quantity)
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
