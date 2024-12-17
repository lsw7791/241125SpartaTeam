using UnityEngine;

public enum EquipSlot
{
    None,
    Head,    // ����
    Armor,    // ��
    Weapon,  // ����
    Shoes    // �Ź�
}

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
    public EquipSlot equipSlot; // ��� ����

    public InventoryItem(int itemID, string itemName, int quantity, ItemType itemType, Sprite itemIcon, EquipSlot slot)
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
        equipSlot = slot;
    }
}
