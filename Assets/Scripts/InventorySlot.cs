using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage; // 슬롯에 표시될 아이템 이미지

    // 아이템 초기화
    public void Initialize(InventoryItem item)
    {
        itemImage.sprite = item.ItemIcon;  // 아이템의 아이콘 설정
        itemImage.enabled = true;            // 이미지 표시

    }

    // 빈 슬롯 처리
    public void ClearSlot(Sprite emptySprite)
    {
        itemImage.sprite = emptySprite;   // 빈 슬롯 이미지 설정
        itemImage.enabled = true;         // 이미지 표시
    }
}
