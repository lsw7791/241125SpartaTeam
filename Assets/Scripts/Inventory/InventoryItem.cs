using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public int ItemID;  // 아이템 ID
    public int Quantity;  // 아이템 수량
    public Sprite ItemIcon;  // 아이템 아이콘 (Sprite)
    public string SpritePath;  // 아이템 아이콘 경로
    public bool IsEquipped;  // 장착 상태
    public int ATKType;  // 공격 타입
    public int enhenceCount;  // 강화 횟수

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
        // Resources 폴더에서 경로를 통해 Sprite 로드
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
