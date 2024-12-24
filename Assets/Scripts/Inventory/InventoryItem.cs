using UnityEngine;

//public enum EquipSlot
//{
//    None,
//    Head,    // 모자
//    Armor,    // 옷
//    Weapon,  // 무기
//    Shoes    // 신발
//}

[System.Serializable]
public class InventoryItem
{
    public int ItemID;  // 아이템 ID
    //public string ItemName;  // 아이템 이름
    public int Quantity;  // 아이템 수량
    //public ItemType ItemType;  // 아이템 타입
    //public ItemUseType itemUseType;
    public Sprite ItemIcon;  // 아이템 아이콘 (Sprite)
    public string SpritePath;    // 아이템 아이콘 경로
    public bool IsEquipped; // 장착 상태
    public int ATKType;
    //public EquipSlot equipSlot; // 장비 부위

    public int enhenceCount;
    public InventoryItem() { }
    public InventoryItem(int itemID, int quantity, string spritePath, bool isEquipped, int atkType)
    {
        ItemID = itemID;
        //ItemName = itemName;
        Quantity = quantity;
        SpritePath = spritePath;
        ItemIcon = LoadSprite(SpritePath);
        IsEquipped = isEquipped;
        ATKType = atkType;
        //ItemType = itemType;
        //ItemIcon = itemIcon;  // 아이콘 설정
        // IsEquipped = false;
        //itemUseType = ItemUseType.Equipment;
        // TODO :: 현재 모두 장착 아이템인 상태
        // 장착 되는지 확인 후 UGS 변경 예정
        //equipSlot = slot;
        enhenceCount = 0;
    }
    private Sprite LoadSprite(string path)
    {
        return Resources.Load<Sprite>(path); // 경로로부터 스프라이트 로드
    }
}
