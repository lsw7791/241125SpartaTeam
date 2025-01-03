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

    // �κ��丮 �ʱ�ȭ
    public void Clear()
    {
        Items.Clear();
        OnInventoryChanged?.Invoke();
    }

    // ������ �߰� �޼���
    public void AddItem(int itemID, int quantity)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(itemID);
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);

        // ���� ����: ��� Ÿ�� �����Ͽ� ������ �߰�/���� ����
        if (existingItem != null && itemData.itemType >= ItemType.Mine)
        {
            existingItem.Quantity += quantity; // ��� �������� ��� ���� ����
        }
        else
        {
            if (itemData != null)
            {
                InventoryItem newItem = new InventoryItem(itemID, quantity, itemData.atlasPath, false, itemData.atkType);
                Items.Add(newItem); // �� ������ �߰�
                GameManager.Instance.GetCurrentCharacter().AddItem(newItem); // PlayerData���� �߰�
            }
        }

        OnInventoryChanged?.Invoke();
    }


    // ������ ��ȸ �޼���
    public InventoryItem GetItem(int itemID)
    {
        return Items.Find(item => item.ItemID == itemID); // �ش� ID�� ������ ��ȯ
    }

    // ������ ���� ��ȸ
    public int GetItemCount(int itemID)
    {
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);
        return existingItem?.Quantity ?? 0; // ������ ���� ��ȯ, ������ 0
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
            {
                GameManager.Instance.GetCurrentCharacter().RemoveItem(item); // PlayerData���� ����
                Items.Remove(item); // �κ��丮���� ����
            }
        }

        OnInventoryChanged?.Invoke();
    }

    // ������ ���� �޼���
    public void EquipItem(int itemID)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(itemID);
        InventoryItem item = GetItem(itemID);

        if (item != null && itemData != null)
        {
            // ���� �� �÷��̾� ���� ����
            //PlayerData playerData = GameManager.Instance.Player.stats;
            //playerData.PlayerStatsEquip(item);
            //item.IsEquipped = true;  // ������ ���� ���� ������Ʈ

            //GameManager.Instance.Player.stats.PlayerStatsUpdate(item, true);
            //item.IsEquipped = true;
            //GameManager.Instance.Player.stats.AddItem(item); // PlayerData���� �߰�
            OnInventoryChanged?.Invoke();
        }
    }

    // ������ ���� �޼���
    public void UnEquipItem(int itemID)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(itemID);
        InventoryItem item = GetItem(itemID);

        if (item != null && itemData != null)
        {
            // ���� �� �÷��̾� ���� ����
            //PlayerData playerData = GameManager.Instance.Player.stats;
            //playerData.PlayerStatsUnEquip(item);
            //item.IsEquipped = false;  // ������ ���� ���� ������Ʈ

            // ���� ���� ó��
            //GameManager.Instance.Player.stats.PlayerStatsUpdate(item, false);
            //item.IsEquipped = false;
            //GameManager.Instance.Player.stats.RemoveItem(item); // PlayerData���� ����

            OnInventoryChanged?.Invoke();
        }
    }

    // ������ ��� �޼���
    public void DropItem(int itemID)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(itemID);
        InventoryItem item = GetItem(itemID);

        if (item != null)
        {
            RemoveItem(itemID, 1); // ������ ������ ���ҽ�Ű��

            // ������ �����Ϳ� ������ �ε�
            GameObject itemPrefab = Resources.Load<GameObject>(itemData.prefabPath);
            GameObject dropItem = GameObject.Instantiate(itemPrefab, GameManager.Instance.Player.transform.position, Quaternion.identity);
            if (dropItem.TryGetComponent<TestItem>(out var outItem))
            {
                outItem.isPlayerDrop = true;
                outItem.itemData = itemData;
            }

            OnInventoryChanged?.Invoke();
        }
    }

    // �κ��丮 �������� JSON �������� ����
    public string SaveInventoryToJson()
    {
        return JsonUtility.ToJson(this, true);
    }

    // JSON �������� ����� �κ��丮 �����͸� �ҷ��ͼ� �κ��丮 ����
    public void LoadInventoryFromJson(string json)
    {
        Inventory loadedInventory = JsonUtility.FromJson<Inventory>(json);
        Items = loadedInventory.Items;  // �ҷ��� ������ ����Ʈ�� ��ü
        OnInventoryChanged?.Invoke();
    }
}
