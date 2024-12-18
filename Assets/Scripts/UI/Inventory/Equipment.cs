using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    public Dictionary<ItemType, Equip> equipItems = new Dictionary<ItemType, Equip>();
    [SerializeField] private EquipmentUI _equipmentUI;

    public void EquipNew(InventoryItem inItem)
    {
        var itemData = GameManager.Instance.DataManager.GetItemDataById(inItem.ItemID);

        if (itemData.itemType > ItemType.Mine)
        {
            return;
        }

        // 기존 장비 해제
        UnEquip(itemData.itemType);

        // 새 장비 장착
        GameObject newEquip = new GameObject(itemData.name);
        newEquip.transform.SetParent(transform);

        Equip curEquip = newEquip.AddComponent<Equip>();

        equipItems[itemData.itemType] = curEquip;

        // UI 장비창 업데이트
        _equipmentUI.UpdateEquipmentSlot(itemData.itemType, inItem.ItemIcon);
    }

    public void UnEquip(ItemType slot)
    {
        if (equipItems.ContainsKey(slot))
        {
            Destroy(equipItems[slot].gameObject);
            equipItems.Remove(slot);

            // UI 장비창 클리어
            _equipmentUI.ClearEquipmentSlot(slot);
        }
    }

    public void EquipmentUIReference(EquipmentUI equipmentUI)
    {
        _equipmentUI = equipmentUI;
    }
}
