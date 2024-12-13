using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Inventory
{
    public List<InventoryItem> Items; // 플레이어의 인벤토리 아이템 목록
    public event Action OnInventoryChanged; // 인벤토리 변경 이벤트

    public Inventory()
    {
        Items = new List<InventoryItem>();
    }

    // 아이템 추가 메서드
    public void AddItem(int itemID, string itemName, int quantity, ItemType itemType, Sprite itemSprite)
    {
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);
        // 동일한 itemID를 가진 아이템 탐색
        if (existingItem != null && (int)itemType > 10)
        { // 인벤토리 내에 동일한 itemID가 있고 장비 아이템이 아니라면
            existingItem.Quantity += quantity;
            // 찾은 아이템이 있다면 수량 증가
        }
        else
        {
            Items.Add(new InventoryItem(itemID, itemName, quantity, itemType, itemSprite));
            // 동일한 itemID 없거나 장비 아이템이라면 리스트에 추가
        }

        OnInventoryChanged?.Invoke();
    }


    // 아이템 조회 메서드 추가
    public InventoryItem GetItem(int itemID)
    {
        return Items.Find(item => item.ItemID == itemID); // 해당 ID의 아이템 반환
    }

    // 아이템 목록 반환
    public List<InventoryItem> GetItems()
    {
        return Items;
    }

    // 아이템 제거 메서드
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
