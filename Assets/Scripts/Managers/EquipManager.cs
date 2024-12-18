using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoSingleton<EquipManager>
{
    [System.Serializable]
    public class EquipmentSlot
    {
        public ItemType Type;          // ���� Ÿ�� (Helmet, Weapon ��)
        public GameObject EquipPlace;  // ���� ��ġ
        [HideInInspector] public SpriteRenderer EquipSpriteRenderer; // SpriteRenderer
    }

    [Header("Equipment Slots")]
    public List<EquipmentSlot> EquipmentSlots; // ��� ��� ����

    private Dictionary<ItemType, EquipmentSlot> _slotMap;

    void Awake()
    {
        // ���� Ÿ�԰� EquipmentSlot ����
        _slotMap = new Dictionary<ItemType, EquipmentSlot>();
        foreach (var slot in EquipmentSlots)
        {
            slot.EquipSpriteRenderer = slot.EquipPlace.GetComponentInChildren<SpriteRenderer>();
            _slotMap[slot.Type] = slot;

            // �ʱ�ȭ: ������ �������� ������ �⺻ ��������Ʈ�� null�� ����
            SetSprite(slot, null);
        }
    }

    // Ư�� ���� ������Ʈ
    public void UpdateEquipPlace(ItemType type, Sprite itemSprite)
    {
        if (_slotMap.TryGetValue(type, out EquipmentSlot slot))
        {
            SetSprite(slot, itemSprite);
        }
        else
        {
            Debug.LogWarning($"No EquipPlace found for ItemType: {type}");
        }
    }

    // ��������Ʈ ���� ó��
    private void SetSprite(EquipmentSlot slot, Sprite itemSprite)
    {
        if (slot.EquipSpriteRenderer != null)
        {
            slot.EquipSpriteRenderer.sprite = itemSprite;
        }
    }
}
