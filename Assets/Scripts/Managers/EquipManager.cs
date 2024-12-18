using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoSingleton<EquipManager>
{
    [System.Serializable]
    public class EquipmentSlot
    {
        public ItemType Type;          // 슬롯 타입 (Helmet, Weapon 등)
        public GameObject EquipPlace;  // 장착 위치
        [HideInInspector] public SpriteRenderer EquipSpriteRenderer; // SpriteRenderer
    }

    [Header("Equipment Slots")]
    public List<EquipmentSlot> EquipmentSlots; // 모든 장비 슬롯

    private Dictionary<ItemType, EquipmentSlot> _slotMap;

    void Awake()
    {
        // 슬롯 타입과 EquipmentSlot 매핑
        _slotMap = new Dictionary<ItemType, EquipmentSlot>();
        foreach (var slot in EquipmentSlots)
        {
            slot.EquipSpriteRenderer = slot.EquipPlace.GetComponentInChildren<SpriteRenderer>();
            _slotMap[slot.Type] = slot;

            // 초기화: 장착된 아이템이 없으면 기본 스프라이트를 null로 설정
            SetSprite(slot, null);
        }
    }

    // 특정 슬롯 업데이트
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

    // 스프라이트 변경 처리
    private void SetSprite(EquipmentSlot slot, Sprite itemSprite)
    {
        if (slot.EquipSpriteRenderer != null)
        {
            slot.EquipSpriteRenderer.sprite = itemSprite;
        }
    }
}
