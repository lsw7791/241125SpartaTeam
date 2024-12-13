using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public int ItemID;  // ������ ID
    public string ItemName;  // ������ �̸�
    public int Quantity;  // ������ ����
    public ItemType ItemType;  // ������ Ÿ��
    public Sprite ItemIcon;  // ������ ������ (Sprite)

    public InventoryItem(int itemID, string itemName, int quantity, ItemType itemType, Sprite itemIcon = null)
    {
        ItemID = itemID;
        ItemName = itemName;
        Quantity = quantity;
        ItemType = itemType;
        ItemIcon = itemIcon;  // ������ ����
    }
}
