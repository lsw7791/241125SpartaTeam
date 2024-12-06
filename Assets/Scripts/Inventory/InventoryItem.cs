using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string ItemID;  // ������ ID
    public string ItemName;  // ������ �̸�
    public int Quantity;  // ������ ����
    public string ItemType;  // ������ Ÿ��
    public Sprite ItemIcon;  // ������ ������ (Sprite)

    // ������
    public InventoryItem(string itemID, string itemName, int quantity, string itemType, string iconPath = "")
    {
        ItemID = itemID;
        ItemName = itemName;
        Quantity = quantity;
        ItemType = itemType;
        ItemIcon = LoadItemIcon(iconPath);  // ������ �ε�
    }

    // ������ �ε� �Լ�
    private Sprite LoadItemIcon(string iconPath)
    {
        // Resources ���� ������ �̹��� �ε�
        Sprite icon = Resources.Load<Sprite>(iconPath);

        return icon;  // ������ ��ȯ
    }
}
