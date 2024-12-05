using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    private const int TotalSlots = 12; // 슬롯 개수 고정
    private List<ItemInstance> _items;
    private List<GameObject> _slots;

    public Transform slotContainer; // 슬롯 부모 객체
    public GameObject slotPrefab;   // 슬롯 프리팹

    public Sprite emptySlotSprite;  // 빈 슬롯에 사용할 이미지

    public void Initialize(List<ItemInstance> items)
    {
        _items = items;
        CreateSlots();  // 고정된 슬롯 생성
        Refresh();
    }

    private void CreateSlots()
    {
        for (int i = 0; i < TotalSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            _slots.Add(slot);
        }
    }

    private void Refresh()
    {
        // 슬롯 초기화
        for (int i = 0; i < TotalSlots; i++)
        {
            var slotComponent = _slots[i].GetComponent<InventorySlot>();

            if (i < _items.Count)
            {
                // 아이템이 있는 경우 슬롯에 아이템 표시
                slotComponent.Initialize(_items[i]);
            }
            else
            {
                // 아이템이 없는 경우 빈 슬롯으로 설정
                slotComponent.ClearSlot(emptySlotSprite);
            }
        }
    }
}
