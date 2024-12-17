using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public int ItemID;  // ������ ID
    public string ItemName;  // ������ �̸�
    public int Quantity;  // ������ ����
    public ItemType ItemType;  // ������ Ÿ��
    public ItemUseType itemUseType;
    public Sprite ItemIcon;  // ������ ������ (Sprite)
    public bool IsEquipped; // ���� ����

    public InventoryItem(int itemID, string itemName, int quantity, ItemType itemType, Sprite itemIcon = null)
    {
        ItemID = itemID;
        ItemName = itemName;
        Quantity = quantity;
        ItemType = itemType;
        ItemIcon = itemIcon;  // ������ ����
        IsEquipped = false;
        itemUseType = ItemUseType.Equipment;
        // TODO :: ���� ��� ���� �������� ����
        // ���� �Ǵ��� Ȯ�� �� UGS ���� ����
    }
}
