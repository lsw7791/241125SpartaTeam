using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string ItemID;  // 아이템 ID
    public string ItemName;  // 아이템 이름
    public int Quantity;  // 아이템 수량
    public string ItemType;  // 아이템 타입
    public int SlotIndex;  // 슬롯 번호
    public Sprite ItemIcon;  // 아이템 아이콘 (Sprite)

    public InventoryItem(string itemID, string itemName, int quantity, string itemType, int? slotIndex = null, Sprite itemIcon = null)
    {
        ItemID = itemID;
        ItemName = itemName;
        Quantity = quantity;
        ItemType = itemType;
        SlotIndex = slotIndex ?? 0;
        ItemIcon = itemIcon;  // 아이콘 설정
    }
}
