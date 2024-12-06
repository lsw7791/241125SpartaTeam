using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage; // 슬롯에 표시될 아이템 이미지
    public TextMeshProUGUI count;
    

    // 아이템 초기화
    public void Initialize(InventoryItem item)
    {
        itemImage.sprite = item.ItemIcon;  // 아이템의 아이콘 설정
        itemImage.enabled = true;            // 이미지 표시
        int quantity = item.Quantity;
        count.text = quantity.ToString();
    }

    // 빈 슬롯 처리
    public void ClearSlot()
    {
        gameObject.SetActive(false);  // 빈 슬롯 이미지 설정
    }
}
