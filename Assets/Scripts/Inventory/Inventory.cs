using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Inventory
{
    public List<InventoryItem> Items; // 아이템 목록
    public event Action OnInventoryChanged; // 인벤토리 변경 이벤트

    public Inventory()
    {
        Items = new List<InventoryItem>();
    }

    // 아이템 추가 메서드
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


    // 아이템 조회 메서드 추가
    public InventoryItem GetItem(string itemID)
    {
        return Items.Find(item => item.ItemID == itemID); // 해당 ID의 아이템 반환
    }

    // 아이템 목록 반환
    public List<InventoryItem> GetItems()
    {
        return Items;
    }

    // 아이템 제거 메서드
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
