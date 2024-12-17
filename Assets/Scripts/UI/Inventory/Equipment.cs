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

    //    // 기존 장비 해제
    //    UnEquip(inItem.equipSlot);

    //    // 새 장비 장착
    //    GameObject newEquip = new GameObject(inItem.ItemName);
    //    newEquip.transform.SetParent(transform);

    //    Equip curEquip = newEquip.AddComponent<Equip>();
    //    Image newImage = curEquip.AddComponent<Image>();
    //    newImage.sprite = inItem.ItemIcon;

    //    equipItems[inItem.equipSlot] = curEquip;

    //    // UI 장비창 업데이트
    //    _equipmentUI.UpdateEquipmentSlot(inItem.equipSlot, inItem.ItemIcon);
    //}

    //public void UnEquip(EquipSlot slot)
    //{
    //    if (equipItems.ContainsKey(slot))
    //    {
    //        Destroy(equipItems[slot].gameObject);
    //        equipItems.Remove(slot);

    //        // UI 장비창 클리어
    //        _equipmentUI.ClearEquipmentSlot(slot);
    //    }
    //}

    //public void EquipmentUIReference(EquipmentUI equipmentUI)
    //{
    //    _equipmentUI = equipmentUI;
    //}
}
