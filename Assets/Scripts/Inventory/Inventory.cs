using System;
using System.Collections.Generic;
using UnityEngine;

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
    // ������ �߰� �޼���
    public void AddItem(int itemID, int quantity)
    {
        // ������ �����͸� ���� ��Ʈ���� ��ȸ
        var itemData = GameManager.Instance.DataManager.GetItemDataById(itemID);

        // ������ ID�� ���� ������ ã��
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);

        if (existingItem != null && itemData.itemType >= ItemType.Mine)
        { // TODO :: ��� Ÿ���� ������ ������ �ʿ�
            existingItem.Quantity += quantity;  // ���� ����
        }
        else
        {
            // ������ ID�� �������� ���� ��Ʈ���� ��ȸ�ؼ� �߰�
            if (itemData != null)
            {
                // ���� ��Ʈ���� ���� spritePath�� �������� �߰�
                Items.Add(new InventoryItem(itemID, quantity, itemData.spritePath, false, itemData.atkType));  // ������ �����Ͽ� �߰�
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

    //������ ���� �޼���
    public void EquipItem(int itemID)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(itemID);

        InventoryItem item = GetItem(itemID);

        if (item != null && itemData.itemType <= ItemType.Mine)
        {
            OnInventoryChanged?.Invoke();
            // UI ����
        }
    }

    public void DropItem(int itemID)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(itemID);

        InventoryItem item = GetItem(itemID);

        if (item != null)
        {
            RemoveItem(itemID, 1);
            // ������ �����Ϳ� ������ �ε�
            GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabPath);
            GameObject dropItem = GameObject.Instantiate(itemPrefab, GameManager.Instance.Player.transform.position, Quaternion.identity);
            if (dropItem.TryGetComponent<TestItem>(out var outItem))
            {
                outItem.isPlayerDrop = true;
                outItem.itemData = itemData;
            }
            OnInventoryChanged?.Invoke();
            // UI ����
        }
    }
}
