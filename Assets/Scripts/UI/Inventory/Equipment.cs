using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    //public Dictionary<EquipSlot, Equip> equipItems = new Dictionary<EquipSlot, Equip>();
    //[SerializeField] private EquipmentUI _equipmentUI;

    //public void EquipNew(InventoryItem inItem)
    //{
    //    if (inItem.equipSlot == EquipSlot.None)
    //    {
    //        return;
    //    }

    //    // ���� ��� ����
    //    UnEquip(inItem.equipSlot);

    //    // �� ��� ����
    //    GameObject newEquip = new GameObject(inItem.ItemName);
    //    newEquip.transform.SetParent(transform);

    //    Equip curEquip = newEquip.AddComponent<Equip>();
    //    Image newImage = curEquip.AddComponent<Image>();
    //    newImage.sprite = inItem.ItemIcon;

    //    equipItems[inItem.equipSlot] = curEquip;

    //    // UI ���â ������Ʈ
    //    _equipmentUI.UpdateEquipmentSlot(inItem.equipSlot, inItem.ItemIcon);
    //}

    //public void UnEquip(EquipSlot slot)
    //{
    //    if (equipItems.ContainsKey(slot))
    //    {
    //        Destroy(equipItems[slot].gameObject);
    //        equipItems.Remove(slot);

    //        // UI ���â Ŭ����
    //        _equipmentUI.ClearEquipmentSlot(slot);
    //    }
    //}

    //public void EquipmentUIReference(EquipmentUI equipmentUI)
    //{
    //    _equipmentUI = equipmentUI;
    //}
}
