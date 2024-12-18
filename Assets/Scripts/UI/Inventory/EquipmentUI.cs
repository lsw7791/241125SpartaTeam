using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    [Header("Equipment Slots")]
    [SerializeField] private Image _headSlot;
    [SerializeField] private Image _armorSlot;
    [SerializeField] private Image _weaponSlot;
    [SerializeField] private Image _shoesSlot;

    [SerializeField] private Image _shieldSlot;
    [SerializeField] private Image _topSlot;
    [SerializeField] private Image _capeSlot;

    private Dictionary<ItemType, Image> _equipmentSlots;

    private void Awake()
    {
        _equipmentSlots = new Dictionary<ItemType, Image>
        {
            
            { ItemType.Helmet, _headSlot },
            { ItemType.Armor, _armorSlot },
            { ItemType.Weapon, _weaponSlot },
            { ItemType.Bottom, _shoesSlot },
            { ItemType.Shield, _shieldSlot },
            { ItemType.Top, _topSlot },
            { ItemType.Cape, _capeSlot },
        };
    }

    private void OnEnable()
    {
        GameManager.Instance.Player.equipment.EquipmentUIReference(this);
    }

    // ���â�� ������ ������ ǥ��
    public void UpdateEquipmentSlot(ItemType slot, Sprite itemIcon)
    {
        if (_equipmentSlots.TryGetValue(slot, out Image slotImage))
        {
            slotImage.sprite = itemIcon;
            slotImage.enabled = itemIcon != null; // �������� ������ ��Ȱ��ȭ
        }
    }

    // ��� ���� �� ���� ����
    public void ClearEquipmentSlot(ItemType slot)
    {
        if (_equipmentSlots.TryGetValue(slot, out Image slotImage))
        {
            slotImage.sprite = null;
            slotImage.enabled = false;
        }
    }
}
