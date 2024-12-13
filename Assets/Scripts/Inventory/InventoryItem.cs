using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public int ItemID;  // 아이템 ID
    public string ItemName;  // 아이템 이름
    public int Quantity;  // 아이템 수량
    public ItemType ItemType;  // 아이템 타입
    public Sprite ItemIcon;  // 아이템 아이콘 (Sprite)

    public InventoryItem(int itemID, string itemName, int quantity, ItemType itemType, Sprite itemIcon = null)
    {
        ItemID = itemID;
        ItemName = itemName;
        Quantity = quantity;
        ItemType = itemType;
        ItemIcon = itemIcon;  // 아이콘 설정
    }
}
