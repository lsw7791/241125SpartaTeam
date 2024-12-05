using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    private List<GameObject> _slots = new List<GameObject>(); // 슬롯 UI 목록

    public Transform slotContainer; // 슬롯 부모 객체
    public GameObject slotPrefab;   // 슬롯 프리팹

    public Sprite emptySlotSprite;  // 빈 슬롯 이미지

    private void Awake()
    {
        Setup(Player.Instance.inventory);
    }
    public void Setup(Inventory inventory)
    {

        Player.Instance.inventory.OnInventoryChanged += Refresh;

        // 슬롯 생성 및 초기화
        InitializeSlots();
        Refresh(); // 초기 UI 갱신
    }


    /// <summary>
    /// 인벤토리 UI 슬롯을 생성합니다.
    /// </summary>
    private void InitializeSlots()
    {
        // 기존 슬롯 제거
        foreach (var slot in _slots)
        {
            Destroy(slot);
        }
        _slots.Clear();

        // 슬롯 생성
        for (int i = 0; i < 12; i++) // 슬롯 수 고정 (필요시 수정 가능)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            _slots.Add(slot);
        }
    }

    /// <summary>
    /// 인벤토리 변경에 따라 UI를 갱신합니다.
    /// </summary>
    private void Refresh()
    {

        var items = Player.Instance.inventory.GetItems(); // 아이템 리스트 받아오기

        for (int i = 0; i < _slots.Count; i++)
        {
            var slotComponent = _slots[i].GetComponent<InventorySlot>();

            if (i < items.Count)
            {
                // 아이템이 있는 경우, 슬롯에 아이템 정보 표시
                slotComponent.Initialize(items[i]);
            }
            else
            {
                // 빈 슬롯 처리
                slotComponent.ClearSlot(emptySlotSprite);
            }
        }
    }

}
