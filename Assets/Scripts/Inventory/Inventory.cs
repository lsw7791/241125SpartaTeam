using System;
using System.Collections.Generic;

[System.Serializable]
public class Inventory
{
    public List<InventoryItem> Items; // �÷��̾��� �κ��丮 ������ ���
    public event Action OnInventoryChanged; // �κ��丮 ���� �̺�Ʈ

    public Inventory()
    {
        Items = new List<InventoryItem>();
    }

    public void Clear()
    {
        Items.Clear();
        OnInventoryChanged?.Invoke();
    }

    // ������ �߰� �޼���
    public void AddItem(int itemID)
    {
        // ������ �����͸� ���� ��Ʈ���� ��ȸ
        var itemData = GameManager.Instance.DataManager.GetItemDataById(itemID);

        // ������ ID�� ���� ������ ã��
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);

        if (existingItem != null)
        {
            existingItem.Quantity += 1;  // ���� ����
        }
        else
        {
            // ������ ID�� �������� ���� ��Ʈ���� ��ȸ�ؼ� �߰�
            if (itemData != null)
            {
                // ���� ��Ʈ���� ���� spritePath�� �������� �߰�
                Items.Add(new InventoryItem(itemID, 1, itemData.spritePath));  // ������ ������ ��η� ������ �ε�
            }
        }

        OnInventoryChanged?.Invoke();
    }


    // ������ ��ȸ �޼��� �߰�
    public InventoryItem GetItem(int itemID)
    {
        return Items.Find(item => item.ItemID == itemID); // �ش� ID�� ������ ��ȯ
    }

    // ������ ���� ��ȸ
    public int GetItemCount(int itemID)
    {
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);
        return existingItem?.Quantity ?? 0;
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
