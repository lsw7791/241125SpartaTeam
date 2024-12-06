using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string ItemID;  // 아이템 ID
    public string ItemName;  // 아이템 이름
    public int Quantity;  // 아이템 수량
    public string ItemType;  // 아이템 타입
    public Sprite ItemIcon;  // 아이템 아이콘 (Sprite)

    // 생성자
    public InventoryItem(string itemID, string itemName, int quantity, string itemType, string iconPath = "")
    {
        ItemID = itemID;
        ItemName = itemName;
        Quantity = quantity;
        ItemType = itemType;
        ItemIcon = LoadItemIcon(iconPath);  // 아이콘 로드
    }

    // 아이콘 로드 함수
    private Sprite LoadItemIcon(string iconPath)
    {
        // Resources 폴더 내에서 이미지 로드
        Sprite icon = Resources.Load<Sprite>(iconPath);

        return icon;  // 아이콘 반환
    }
}
