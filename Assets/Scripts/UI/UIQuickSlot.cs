using UnityEngine;
using UnityEngine.UI;

public class UIQuickSlot : MonoBehaviour
{
    // 슬롯을 나타낼 이미지 배열
    public Image[] slotImages;
    public Text[] slotTexts;

    // 선택된 슬롯을 강조할 이미지
    public Image selectedSlotImage;
    public Color selectedColor = Color.yellow;  // 선택된 슬롯의 색상

    // 슬롯에 할당할 아이템 (이미지와 텍스트로 표시)
    private Sprite[] itemSprites;
    private string[] itemNames;

    private int selectedSlotIndex = 0;  // 현재 선택된 슬롯 인덱스 (0부터 시작)

    private QuickSlotsController quickSlotsController;  // QuickSlotsController 스크립트 참조

    void Start()
    {
        // QuickSlotsController 스크립트 참조 가져오기
        quickSlotsController = FindObjectOfType<QuickSlotsController>();

        // 슬롯 초기화

        // 초기 선택된 슬롯 업데이트
        if (slotImages.Length > 0)
        {
            UpdateSlotHighlight();
        }
    }

    

    // 슬롯 선택 처리
    private void UpdateSlotHighlight()
    {
        // 모든 슬롯의 색을 기본으로 되돌리고
        foreach (var slot in slotImages)
        {
            slot.color = Color.white;
        }

        // 선택된 슬롯에만 색상 적용 (강조)
        if (selectedSlotIndex >= 0 && selectedSlotIndex < slotImages.Length)
        {
            slotImages[selectedSlotIndex].color = selectedColor;
        }
    }

    // 슬롯에 아이템 추가
    public void AddItemToSlot(int slotIndex, Sprite itemSprite, string itemName)
    {
        if (slotIndex >= 0 && slotIndex < slotImages.Length)
        {
            slotImages[slotIndex].sprite = itemSprite;  // 아이템 이미지 할당
            slotTexts[slotIndex].text = itemName;  // 아이템 이름 텍스트 표시
            itemSprites[slotIndex] = itemSprite;  // 내부 배열에 아이템 저장
            itemNames[slotIndex] = itemName;
        }
    }
}
