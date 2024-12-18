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

        // ���� ��� ����
        UnEquip(itemData.itemType);

        // �� ��� ����
        GameObject newEquip = new GameObject(itemData.name);
        newEquip.transform.SetParent(transform);

        Equip curEquip = newEquip.AddComponent<Equip>();

        equipItems[itemData.itemType] = curEquip;

        // UI ���â ������Ʈ
        _equipmentUI.UpdateEquipmentSlot(itemData.itemType, inItem.ItemIcon);
    }

    public void UnEquip(ItemType slot)
    {
        if (equipItems.ContainsKey(slot))
        {
            Destroy(equipItems[slot].gameObject);
            equipItems.Remove(slot);

            // UI ���â Ŭ����
            _equipmentUI.ClearEquipmentSlot(slot);
        }
    }

    public void EquipmentUIReference(EquipmentUI equipmentUI)
    {
        _equipmentUI = equipmentUI;
    }
}
