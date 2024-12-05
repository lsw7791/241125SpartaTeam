using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryUI : UIBase
{
    [SerializeField] private Transform itemListContainer;  // 아이템 리스트를 표시할 UI 컨테이너
    [SerializeField] private GameObject itemSlotPrefab;    // 아이템 슬롯의 프리팹

    private Inventory inventory;

    protected override void Awake()
    {
        base.Awake();
        inventory = Player.Instance.Inventory;  // 플레이어의 인벤토리 참조
    }

    public override void Open()
    {
        base.Open();  // UIBase의 Open 메서드 호출
        UpdateInventoryUI();  // 인벤토리 UI 갱신
    }

    public override void Close()
    {
        base.Close();  // UIBase의 Close 메서드 호출
    }

    // 인벤토리 UI 업데이트
    public void UpdateInventoryUI()
    {
        // 기존 아이템 리스트 삭제
        foreach (Transform child in itemListContainer)
        {
            Destroy(child.gameObject);
        }

        // 현재 인벤토리 아이템들을 아이템 슬롯으로 변환하여 표시
        List<InventoryItem> inventoryItems = inventory.GetItems();  // 인벤토리 아이템 목록 가져오기

        foreach (var item in inventoryItems)
        {
            // 슬롯 생성
            GameObject itemSlot = Instantiate(itemSlotPrefab, itemListContainer);

            // TextMeshProUGUI 컴포넌트에 아이템 이름과 갯수 표시
            TextMeshProUGUI itemText = itemSlot.GetComponentInChildren<TextMeshProUGUI>();  // TextMeshProUGUI로 변경
            if (itemText != null)
            {
                itemText.text = $"{item.ItemName} x{item.Quantity}";  // 아이템 이름과 수량
            }

            // 아이콘 이미지 컴포넌트에 아이템 아이콘 설정
            Image itemIcon = itemSlot.GetComponentInChildren<Image>();
            if (itemIcon != null && item.ItemIcon != null)
            {
                itemIcon.sprite = item.ItemIcon;  // 아이템 아이콘 설정
            }
        }
    }
}
