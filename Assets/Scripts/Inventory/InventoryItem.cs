using UnityEngine;

//public enum EquipSlot
//{
//    None,
//    Head,    // ����
//    Armor,    // ��
//    Weapon,  // ����
//    Shoes    // �Ź�
//}

[System.Serializable]
public class InventoryItem
{
    public int ItemID;  // ������ ID
    //public string ItemName;  // ������ �̸�
    public int Quantity;  // ������ ����
    //public ItemType ItemType;  // ������ Ÿ��
    //public ItemUseType itemUseType;
    public Sprite ItemIcon;  // ������ ������ (Sprite)
    public string SpritePath;    // ������ ������ ���
    public bool IsEquipped; // ���� ����
    //public EquipSlot equipSlot; // ��� ����
    public InventoryItem() { }
    public InventoryItem(int itemID, int quantity, string spritePath, bool isEquipped)
    {
        ItemID = itemID;
        //ItemName = itemName;
        Quantity = quantity;
        SpritePath = spritePath;
        ItemIcon = LoadSprite(SpritePath);
        IsEquipped = isEquipped;
        //ItemType = itemType;
        //ItemIcon = itemIcon;  // ������ ����
        // IsEquipped = false;
        //itemUseType = ItemUseType.Equipment;
        // TODO :: ���� ��� ���� �������� ����
        // ���� �Ǵ��� Ȯ�� �� UGS ���� ����
        //equipSlot = slot;
    }
    private Sprite LoadSprite(string path)
    {
        return Resources.Load<Sprite>(path); // ��ηκ��� ��������Ʈ �ε�
    }
}
