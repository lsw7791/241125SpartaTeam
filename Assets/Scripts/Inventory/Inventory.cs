using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public List<InventoryItem> Items; // �÷��̾��� �κ��丮 ������ ���
    public event Action OnInventoryChanged; // �κ��丮 ���� �̺�Ʈ

    private Dictionary<ItemType, InventoryItem> _equipItems = new Dictionary<ItemType, InventoryItem>();
    // ����Ʈ�� �����غ���
    //public EquipmentUI _equipmentUI;
    //UI�� ���� ����

    public Inventory()
    {
        Items = new List<InventoryItem>();
    }

    public void SaveEquipInIt()
    {
        GameManager.Instance.Player._playerWeapon.ATKType = 0;
        GameManager.Instance.Player.Stats.WeaponType = 0;

        //InventoryUI inventoryUI = UIManager.Instance.GetUI<InventoryUI>();
        //inventoryUI.EquipmentUIReset();

        foreach (InventoryItem item in Items)
        {
            if (item.IsEquipped)
            {
                var itemData = GameManager.Instance.DataManager.GetItemDataById(item.ItemID);

                _equipItems[itemData.itemType] = item;

                // UI ���â ������Ʈ
                //inventoryUI.UpdateEquipmentSlot(itemData.itemType, UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath));
                //inventoryUI.EquipmentRefresh();

                if (itemData.itemType == ItemType.Weapon)
                {
                    GameManager.Instance.Player._playerWeapon.ATKType = item.ATKType;
                    GameManager.Instance.Player.Stats.WeaponType = item.ATKType;
                }
            }
        }
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
            }
        }

        OnInventoryChanged?.Invoke();
        GameManager.Instance.DataManager.SaveData(this);
        GameManager.Instance.DataManager.SaveData(GameManager.Instance.Player.stats);
        GameManager.Instance.Player.stats.nowMapNumber = GameManager.Instance.SceneNum;
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

    public void Equip(InventoryItem inItem)
    {
        var newItemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        if (newItemData.itemType > ItemType.Mine)
        {
            return;
        }

        // ���� Ÿ���� ���� ��� �ִ��� Ȯ��
        if (_equipItems.TryGetValue(newItemData.itemType, out var equippedItem))
        {
            // ���� ��� ����
            UnEquip(newItemData.itemType);
        }

        // �� ������ ����
        EquipNew(inItem);
    }

    public void EquipNew(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        // ���� ��� ����
        UnEquip(itemData.itemType);


        _equipItems[itemData.itemType] = inItem;

        if (itemData.itemType == ItemType.Weapon)
        {
            GameManager.Instance.Player._playerWeapon.ATKType = inItem.ATKType;
            GameManager.Instance.Player.Stats.WeaponType = inItem.ATKType;
        }

        inItem.IsEquipped = true;
        GameManager.Instance.Player.stats.PlayerStatsUpdate(inItem, true);
    }

    public void UnEquip(ItemType inItemType)
    {
        if (inItemType == ItemType.Weapon)
        {
            GameManager.Instance.Player._playerWeapon.ATKType = 0;
            GameManager.Instance.Player.Stats.WeaponType = 0;
        }

        if (_equipItems.TryGetValue(inItemType, out var equippedItem))
        {
            // ���� ���� ó��
            GameManager.Instance.Player.stats.PlayerStatsUpdate(equippedItem, false);

            //saveItemList.Remove(inItem);
            // ��ųʸ����� ����
            _equipItems.Remove(inItemType);

            equippedItem.IsEquipped = false;
        }
    }
}
