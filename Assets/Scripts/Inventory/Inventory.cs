using System;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    public List<InventoryItem> Items; // ������ ���

    public Inventory()
    {
        Items = new List<InventoryItem>(); // �ʱ�ȭ
    }

    // ������ �߰�
    public void AddItem(string itemID, string itemName, int quantity, string itemType, int? slotIndex = null)
    {
        // �̹� �����ϴ� ���������� Ȯ��
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity; // ���� ����
        }
        else
        {
            // �� ������ �߰�
            InventoryItem newItem = new InventoryItem(itemID, itemName, quantity, itemType, slotIndex);
            Items.Add(newItem);
        }
    }

    // ������ ����
    public void RemoveItem(string itemID, int quantity)
    {
        InventoryItem item = Items.Find(i => i.ItemID == itemID);
        if (item != null)
        {
            item.Quantity -= quantity;
            if (item.Quantity <= 0) Items.Remove(item); // ������ 0 ���Ϸ� �������� ������ ����
        }
    }

    // Ư�� ������ �˻�
    public InventoryItem GetItem(string itemID)
    {
        return Items.Find(item => item.ItemID == itemID);
    }
}
