using System.Collections.Generic;
using System;

[Serializable]
public class QuickSlot
{
    public List<QuickSlotItem> Slots;

    public QuickSlot()
    {
        Slots = new List<QuickSlotItem>();  // 슬롯 초기화
    }

    // 퀵슬롯에 아이템 추가
    public void AddQuickSlotItem(string itemID, int slotIndex)
    {
        // 아이템이 이미 슬롯에 있는지 확인
        QuickSlotItem existingItem = Slots.Find(item => item.ItemID == itemID);
        if (existingItem == null)
        {
            QuickSlotItem newItem = new QuickSlotItem(itemID, slotIndex);
            Slots.Add(newItem);
        }
    }

    // 퀵슬롯에서 아이템 제거
    public void RemoveQuickSlotItem(int slotIndex)
    {
        QuickSlotItem item = Slots.Find(i => i.SlotIndex == slotIndex);
        if (item != null)
        {
            Slots.Remove(item);
        }
    }

    // 슬롯 인덱스로 아이템 가져오기
    public QuickSlotItem GetSlotItem(int slotIndex)
    {
        return Slots.Find(item => item.SlotIndex == slotIndex);
    }
}

[Serializable]
public class QuickSlotItem
{
    public string ItemID;  // 아이템 ID
    public int SlotIndex;  // 슬롯 번호

    public QuickSlotItem(string itemID, int slotIndex)
    {
        ItemID = itemID;
        SlotIndex = slotIndex;
    }
}
