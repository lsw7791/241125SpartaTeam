using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage; // 슬롯에 표시될 아이템 이미지

    public void Initialize(ItemInstance item)
    {
        itemImage.sprite = item.icon; // 아이템의 아이콘 설정
        itemImage.enabled = true;    // 이미지 표시
    }

    public void ClearSlot(Sprite emptySprite)
    {
        itemImage.sprite = emptySprite; // 빈 슬롯 이미지 설정
        itemImage.enabled = true;      // 이미지 표시
    }
}
