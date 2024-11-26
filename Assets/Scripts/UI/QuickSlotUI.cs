using UnityEngine;
using UnityEngine.UI;

public class QuickSlotUI : MonoBehaviour
{
    public Image[] slotImages;  // 슬롯 이미지 배열
    public Color selectedColor = Color.yellow;
    public Color defaultColor = Color.white;

    private void OnEnable()
    {
        EventBroker.QuickSlotChanged += UpdateSlotHighlight;  // 이벤트 구독
    }

    private void OnDisable()
    {
        EventBroker.QuickSlotChanged -= UpdateSlotHighlight;  // 이벤트 구독 해제
    }

    private void UpdateSlotHighlight(int selectedSlotIndex)
    {
        // 모든 슬롯 색상을 기본 색상으로 초기화
        foreach (var slot in slotImages)
        {
            slot.color = defaultColor;
        }

        // 선택된 슬롯 강조
        if (selectedSlotIndex >= 0 && selectedSlotIndex < slotImages.Length)
        {
            slotImages[selectedSlotIndex].color = selectedColor;
        }
    }
}
