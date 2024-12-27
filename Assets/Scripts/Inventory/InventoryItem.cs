using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public int ItemID;  // ������ ID
    public int Quantity;  // ������ ����
    public Sprite ItemIcon;  // ������ ������ (Sprite)
    public string SpritePath;  // ������ ������ ���
    public bool IsEquipped;  // ���� ����
    public int ATKType;  // ���� Ÿ��
    public int enhenceCount;  // ��ȭ Ƚ��

    public InventoryItem() { }

    public InventoryItem(int itemID, int quantity, string spritePath, bool isEquipped, int atkType)
    {
        ItemID = itemID;
        Quantity = quantity;
        SpritePath = spritePath;
        ItemIcon = LoadSprite(SpritePath);
        IsEquipped = isEquipped;
        ATKType = atkType;
        enhenceCount = 0;
    }

    private Sprite LoadSprite(string path)
    {
        // Resources �������� ��θ� ���� Sprite �ε�
        return Resources.Load<Sprite>(path);
    }

    public Color TierColoer(int inTier)
    {
        Color[] Colors = {
        Color.white,
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.black,
        };
        return Colors[inTier];
    }
}
