//using MainData;
//using System.Collections.Generic;

//public class Equipment
//{
//    //public List<InventoryItem> saveItemList = new List<InventoryItem>();

//    private Dictionary<ItemType, InventoryItem> _equipItems = new Dictionary<ItemType, InventoryItem>();
//    public EquipmentUI _equipmentUI;

//    public void SaveEquipInIt()
//    {
//        foreach (InventoryItem item in saveItemList)
//        {
//            var itemData = GameManager.Instance.DataManager.GetItemDataById(item.ItemID);

//            _equipItems[itemData.itemType] = item;

//            // UI ���â ������Ʈ
//            _equipmentUI.UpdateEquipmentSlot(itemData.itemType, UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath));

//            if (itemData.itemType == ItemType.Weapon)
//            {
//                GameManager.Instance.Player._playerWeapon.ATKType = item.ATKType;
//                GameManager.Instance.Player.Stats.WeaponType = item.ATKType;
//            }
//        }
//    }

//    public void EquipNew(InventoryItem inItem)
//    {
//        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

//        if (itemData.itemType > ItemType.Mine)
//        {
//            return;
//        }

//        // ���� ��� ����
//        UnEquip(inItem);


//        _equipItems[itemData.itemType] = inItem;

//        // UI ���â ������Ʈ
//        _equipmentUI.UpdateEquipmentSlot(itemData.itemType, UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath));
        
//        if(itemData.itemType == ItemType.Weapon)
//        {
//            GameManager.Instance.Player._playerWeapon.ATKType = inItem.ATKType;
//            GameManager.Instance.Player.Stats.WeaponType = inItem.ATKType;
//        }

//        inItem.IsEquipped = true;
//        GameManager.Instance.Player.stats.PlayerStatsUpdate(inItem, true);
//        //saveItemList.Add(inItem);
//    }

//    public void UnEquip(InventoryItem inItem)
//    {
//        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

//        if (_equipItems.ContainsKey(itemData.itemType))
//        {
//            // ��� ã��
//            InventoryItem unequippedItem = _equipItems[itemData.itemType];

//            // ���� ���� ó��
//            GameManager.Instance.Player.stats.PlayerStatsUpdate(unequippedItem, false);

//            //saveItemList.Remove(inItem);
//            // ��ųʸ����� ����
//            _equipItems.Remove(itemData.itemType);

//            // UI ���â Ŭ����
//            _equipmentUI.ClearEquipmentSlot(itemData.itemType);

//            inItem.IsEquipped = false;
//        }
//    }

//    //public void EquipmentUIReference()
//    //{
//    //    _equipmentUI = UIManager.Instance.GetUI<>;
//    //}
//}
