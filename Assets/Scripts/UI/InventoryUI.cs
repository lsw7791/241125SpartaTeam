using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    private Inventory _inventory; // 인벤토리 참조
    private List<GameObject> _slots = new List<GameObject>(); // 슬롯 UI 목록

    public Transform slotContainer; // 슬롯 부모 객체
    public GameObject slotPrefab;   // 슬롯 프리팹

    public Sprite emptySlotSprite;  // 빈 슬롯 이미지

    /// <summary>
    /// UI를 초기화하고, 인벤토리 변경 이벤트를 구독합니다.
    /// </summary>
    /// <param name="inventory">플레이어의 인벤토리</param>
    public void Setup(Inventory inventory)
    {
        // 기존 이벤트 구독 제거 (중복 방지)
        if (_inventory != null)
        {
            _inventory.OnInventoryChanged -= Refresh;
        }

        // 새로운 인벤토리 설정 및 이벤트 구독
        _inventory = inventory;
        _inventory.OnInventoryChanged += Refresh;

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
        if (_inventory == null || _slots == null)
            return;

        var items = _inventory.GetItems(); // 아이템 리스트 받아오기

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

    /// <summary>
    /// 오브젝트 파괴 시 이벤트 구독을 해제합니다.
    /// </summary>
    private void OnDestroy()
    {
        if (_inventory != null)
        {
            _inventory.OnInventoryChanged -= Refresh;
        }
    }
}
