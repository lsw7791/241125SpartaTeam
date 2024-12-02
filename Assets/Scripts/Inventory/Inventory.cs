using System;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    public List<InventoryItem> Items; // 아이템 목록

    public Inventory()
    {
        Items = new List<InventoryItem>(); // 초기화
    }

    // 아이템 추가
    public void AddItem(string itemID, string itemName, int quantity, string itemType, int? slotIndex = null)
    {
        // 이미 존재하는 아이템인지 확인
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity; // 수량 증가
        }
        else
        {
            // 새 아이템 추가
            InventoryItem newItem = new InventoryItem(itemID, itemName, quantity, itemType, slotIndex);
            Items.Add(newItem);
        }
    }

    // 아이템 제거
    public void RemoveItem(string itemID, int quantity)
    {
        InventoryItem item = Items.Find(i => i.ItemID == itemID);
        if (item != null)
        {
            item.Quantity -= quantity;
            if (item.Quantity <= 0) Items.Remove(item); // 수량이 0 이하로 떨어지면 아이템 제거
        }
    }

    // 특정 아이템 검색
    public InventoryItem GetItem(string itemID)
    {
        return Items.Find(item => item.ItemID == itemID);
    }
}
