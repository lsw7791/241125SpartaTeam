using System;
using System.Collections.Generic;

[System.Serializable]
public class Inventory
{
    public List<InventoryItem> Items; // 플레이어의 인벤토리 아이템 목록
    public event Action OnInventoryChanged; // 인벤토리 변경 이벤트

    public Inventory()
    {
        Items = new List<InventoryItem>();
    }

    public void Clear()
    {
        Items.Clear();
        OnInventoryChanged?.Invoke();
    }

    // 아이템 추가 메서드
    public void AddItem(int itemID)
    {
        // 아이템 데이터를 구글 시트에서 조회
        var itemData = GameManager.Instance.DataManager.GetItemDataById(itemID);

        // 아이템 ID로 기존 아이템 찾기
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);

        if (existingItem != null)
        {
            existingItem.Quantity += 1;  // 수량 증가
        }
        else
        {
            // 아이템 ID로 아이템을 구글 시트에서 조회해서 추가
            if (itemData != null)
            {
                // 구글 시트에서 받은 spritePath로 아이템을 추가
                Items.Add(new InventoryItem(itemID, 1, itemData.spritePath));  // 아이템 생성시 경로로 아이콘 로드
            }
        }

        OnInventoryChanged?.Invoke();
    }


    // 아이템 조회 메서드 추가
    public InventoryItem GetItem(int itemID)
    {
        return Items.Find(item => item.ItemID == itemID); // 해당 ID의 아이템 반환
    }

    // 아이템 수량 조회
    public int GetItemCount(int itemID)
    {
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);
        return existingItem?.Quantity ?? 0;
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
