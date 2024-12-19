using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public Dictionary<ItemType, InventoryItem> equipItems = new Dictionary<ItemType, InventoryItem>();
    [SerializeField] private EquipmentUI _equipmentUI;

    public void EquipNew(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        if (itemData.itemType > ItemType.Mine)
        {
            return;
        }

        // ���� ��� ����
        UnEquip(itemData.itemType);

        equipItems[itemData.itemType] = inItem;

        // UI ���â ������Ʈ
        _equipmentUI.UpdateEquipmentSlot(itemData.itemType, inItem.ItemIcon);
        GameManager.Instance.Player._playerWeapon.ATKType = inItem.ATKType;

        inItem.IsEquipped = true;
    }

    public void UnEquip(ItemType itemType)
    {
        if (equipItems.ContainsKey(itemType))
        {

            equipItems.Remove(itemType);

            // UI ���â Ŭ����
            _equipmentUI.ClearEquipmentSlot(itemType);

            // �κ��丮 ���� ���� ó��
            foreach (var item in GameManager.Instance.Player.inventory.Items)
            {
                var itemData = GameManager.Instance.DataManager.GetItemDataById(item.ItemID);
                if (itemData != null && itemData.itemType == itemType)
                {
                    item.IsEquipped = false;
                }
            }
        }
    }


    public void EquipmentUIReference(EquipmentUI equipmentUI)
    {
        _equipmentUI = equipmentUI;
    }
}
