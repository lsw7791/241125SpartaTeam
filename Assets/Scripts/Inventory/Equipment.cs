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

        // ���� ��� ����
        UnEquip(itemData.itemType);


        _equipItems[itemData.itemType] = inItem;

        // UI ���â ������Ʈ
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
            // ��� ã��
            InventoryItem unequippedItem = _equipItems[inItemType];

            // ���� ���� ó��
            GameManager.Instance.Player.stats.PlayerStatsUpdate(unequippedItem, false);
            //GameManager.Instance.Player.inventory.UnEquipItem(unequippedItem.ItemID);

            // ��ųʸ����� ����
            _equipItems.Remove(inItemType);

            // UI ���â Ŭ����
            _equipmentUI.ClearEquipmentSlot(inItemType);

            // �κ��丮 ���� ���� ó��
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
