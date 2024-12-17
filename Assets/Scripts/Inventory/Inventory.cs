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

    public int GetItemCount(int itemID)
    {
        InventoryItem existingItem = Items.Find(item => item.ItemID == itemID);
        // 동일한 itemID를 가진 아이템 탐색
        if (existingItem != null)
        { // 인벤토리 내에 동일한 itemID가 있고 장비 아이템이 아니라면
            return existingItem.Quantity;
            // 찾은 아이템이 있다면 수량 증가
        }
        return 0;
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

    public void EquipItem(int itemID)
    {
        InventoryItem item = GetItem(itemID);

        if (item != null && item.itemUseType == ItemUseType.Equipment)
        { // 아이템이 장착 가능한 아이템인지 확인
            if (item.IsEquipped)
            { // 장착한 아이템인지 여부 확인
                item.IsEquipped = false; // 장착 해제
                // TODO :: 장비창에서 아이템 해제 or 다른 아이템 장착
            }
            else
            {
                item.IsEquipped = true; // 장착
                // TODO :: 장비창에서 아이템 해제 or 다른 아이템 장착
            }
            // TODO :: 장비창에서 ItemType타입 부위가 null이 아니라면
            // 이라는 조건 추가하기

            OnInventoryChanged?.Invoke();
            // UI 갱신
        }
    }
}
