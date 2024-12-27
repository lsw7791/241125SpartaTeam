using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private Dictionary<ItemType, InventoryItem> _equipItems = new Dictionary<ItemType, InventoryItem>();
    private EquipmentUI _equipmentUI;

    public void EquipNew(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        if (itemData.itemType > ItemType.Mine)
        {
            return;
        }

        // 기존 장비 해제
        UnEquip(itemData.itemType);


        _equipItems[itemData.itemType] = inItem;

        // UI 장비창 업데이트
        _equipmentUI.UpdateEquipmentSlot(itemData.itemType, UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath));
        GameManager.Instance.Player._playerWeapon.ATKType = inItem.ATKType;

        inItem.IsEquipped = true;
        GameManager.Instance.Player.stats.PlayerStatsUpdate(inItem, true);
        //GameManager.Instance.Player.inventory.EquipItem(inItem.ItemID);
    }

    public void UnEquip(ItemType inItemType)
    {
        if (_equipItems.ContainsKey(inItemType))
        {
            // 장비를 찾음
            InventoryItem unequippedItem = _equipItems[inItemType];

            // 스탯 감소 처리
            GameManager.Instance.Player.stats.PlayerStatsUpdate(unequippedItem, false);
            //GameManager.Instance.Player.inventory.UnEquipItem(unequippedItem.ItemID);

            // 딕셔너리에서 제거
            _equipItems.Remove(inItemType);

            // UI 장비창 클리어
            _equipmentUI.ClearEquipmentSlot(inItemType);

            // 인벤토리 상태 해제 처리
            foreach (var item in GameManager.Instance.Player.inventory.Items)
            {
                var itemData = GameManager.Instance.DataManager.GetItemDataById(item.ItemID);
                if (itemData != null && itemData.itemType == inItemType)
                {
                    item.IsEquipped = false;
                }
            }
        }
    }



    public void EquipmentUIReference(EquipmentUI inEquipmentUI)
    {
        _equipmentUI = inEquipmentUI;
    }
}
