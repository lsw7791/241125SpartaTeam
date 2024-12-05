using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIBase
{
    private List<GameObject> _slots = new List<GameObject>(); // 슬롯 UI 목록

    public Transform slotContainer; // 슬롯 부모 객체
    public GameObject slotPrefab;   // 슬롯 프리팹

    public Sprite emptySlotSprite;  // 빈 슬롯 이미지

   //인벤토리가 끄고 켤때마다가 아니라 실시간으로 업데이트되는 기능이 구현되어야함
    private void OnEnable()
    {
        Setup(Player.Instance.inventory);
    }

    public void Setup(Inventory inventory)
    {
        // 기존 이벤트 등록 제거
        Player.Instance.inventory.OnInventoryChanged -= Refresh;

        // 새 이벤트 등록
        Player.Instance.inventory.OnInventoryChanged += Refresh;

        // 슬롯 생성 및 초기화
        InitializeSlots();
        Refresh(); // 초기 UI 갱신
    }

    private void InitializeSlots()
    {
        foreach (var slot in _slots)
        {
            Destroy(slot);
        }
        _slots.Clear();

        for (int i = 0; i < 12; i++) // 슬롯 수 고정 (필요시 수정 가능)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);
            _slots.Add(slot);
        }
    }

    private void Refresh()
    {
        Debug.Log($"Inventory Refresh 호출 at {Time.time}");

        var items = Player.Instance.inventory.GetItems(); // 아이템 리스트 받아오기

        for (int i = 0; i < _slots.Count; i++)
        {
            var slotComponent = _slots[i].GetComponent<InventorySlot>();

            if (i < items.Count)
            {
                slotComponent.Initialize(items[i]);
            }
            else
            {
                slotComponent.ClearSlot();
            }
        }
    }
}
