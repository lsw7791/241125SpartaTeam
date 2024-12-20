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

    // ���â�� ������ ������ ǥ�� + EquipManager ����
    public void UpdateEquipmentSlot(ItemType inSlot, Sprite itemIcon)
    {
        UpdateSlot(inSlot, itemIcon);
    }

    // ��� ���� �� ���� ���� + EquipManager ����
    public void ClearEquipmentSlot(ItemType inSlot)
    {
        UpdateSlot(inSlot, null);
    }

    private void UpdateSlot(ItemType inSlot, Sprite itemIcon)
    {
        if (_equipmentSlots.TryGetValue(inSlot, out Image outSlotImage))
        {
            outSlotImage.sprite = itemIcon;
            outSlotImage.enabled = itemIcon != null;
        }

        // EquipManager�� �ش� ���� Ŭ���� ��û
        EquipManager.Instance.UpdateEquipPlace(inSlot, itemIcon);
    }
}
