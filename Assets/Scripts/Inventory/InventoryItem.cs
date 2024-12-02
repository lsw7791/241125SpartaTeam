using System;

[Serializable]
public class InventoryItem
{
    public string ItemID;     // ������ ID
    public string ItemName;   // ������ �̸�
    public int Quantity;      // ������ ����
    public string ItemType;   // ������ ����
    public int? SlotIndex;    // ���� ��ȣ (optional)

    public InventoryItem(string itemID, string itemName, int quantity, string itemType, int? slotIndex = null)
    {
        ItemID = itemID;
        ItemName = itemName;
        Quantity = quantity;
        ItemType = itemType;
        SlotIndex = slotIndex;
    }
}
