using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    //[Header("Equipment Slots")]
    //[SerializeField] private Image _headSlot;
    //[SerializeField] private Image _armorSlot;
    //[SerializeField] private Image _weaponSlot;
    //[SerializeField] private Image _shoesSlot;

    //private Dictionary<EquipSlot, Image> _equipmentSlots;

    //private void Awake()
    //{
    //    _equipmentSlots = new Dictionary<EquipSlot, Image>
    //    {
    //        { EquipSlot.Head, _headSlot },
    //        { EquipSlot.Armor, _armorSlot },
    //        { EquipSlot.Weapon, _weaponSlot },
    //        { EquipSlot.Shoes, _shoesSlot }
    //    };
    //}

    //private void OnEnable()
    //{
    //    GameManager.Instance.Player.equipment.EquipmentUIReference(this);
    //}

    //// 장비창에 아이템 아이콘 표시
    //public void UpdateEquipmentSlot(EquipSlot slot, Sprite itemIcon)
    //{
    //    if (_equipmentSlots.TryGetValue(slot, out Image slotImage))
    //    {
    //        slotImage.sprite = itemIcon;
    //        slotImage.enabled = itemIcon != null; // 아이콘이 없으면 비활성화
    //    }
    //}

    //// 장비 해제 시 슬롯 비우기
    //public void ClearEquipmentSlot(EquipSlot slot)
    //{
    //    if (_equipmentSlots.TryGetValue(slot, out Image slotImage))
    //    {
    //        slotImage.sprite = null;
    //        slotImage.enabled = false;
    //    }
    //}
}
