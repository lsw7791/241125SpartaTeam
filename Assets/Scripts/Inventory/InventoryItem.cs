using System;

[Serializable]
public class InventoryItem
{
    public string ItemID;     // 아이템 ID
    public string ItemName;   // 아이템 이름
    public int Quantity;      // 아이템 개수
    public string ItemType;   // 아이템 종류
    public int? SlotIndex;    // 슬롯 번호 (optional)

    public InventoryItem(string itemID, string itemName, int quantity, string itemType, int? slotIndex = null)
    {
        ItemID = itemID;
        ItemName = itemName;
        Quantity = quantity;
        ItemType = itemType;
        SlotIndex = slotIndex;
    }
}
