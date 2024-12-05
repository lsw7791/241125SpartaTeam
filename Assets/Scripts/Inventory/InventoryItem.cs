using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string ItemID;  // ������ ID
    public string ItemName;  // ������ �̸�
    public int Quantity;  // ������ ����
    public string ItemType;  // ������ Ÿ��
    public Sprite ItemIcon;  // ������ ������ (Sprite)

    public InventoryItem(string itemID, string itemName, int quantity, string itemType, Sprite itemIcon = null)
    {
        ItemID = itemID;
        ItemName = itemName;
        Quantity = quantity;
        ItemType = itemType;
        ItemIcon = itemIcon;  // ������ ����
    }
}
