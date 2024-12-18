using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemImage; // 슬롯에 표시될 아이템 이미지
    [SerializeField] private TMP_Text count;  // 아이템 수량 텍스트
    private InventoryItem item;  // 슬롯에 할당된 아이템

    // 슬롯 클릭 시 아이템 사용 메뉴 활성화
    private void Start()
    {
        Button slotButton = GetComponent<Button>();

        slotButton.onClick.AddListener(() =>
        {
            GameObject itemUseMenu = UIManager.Instance.GetUI<InventoryUI>().itemUseMenu;
            itemUseMenu.SetActive(true);
            itemUseMenu.TryGetComponent<ItemUseUI>(out var outUseUI);
            outUseUI.Initialize(item);  // 아이템 사용 UI 초기화
        });
    }

    // 아이템 초기화 (아이템을 슬롯에 표시)
    public void Initialize(InventoryItem inItem)
    {
        if (inItem == null)
        {
            ClearSlot();
            return;
        }

        item = inItem;
        itemImage.sprite = inItem.ItemIcon;  // 아이템 아이콘 설정
        itemImage.enabled = true;            // 이미지 표시
        count.text = inItem.Quantity.ToString();  // 수량 표시
        gameObject.SetActive(true);          // 슬롯 활성화
    }

    // 빈 슬롯 처리 (슬롯 비활성화)
    public void ClearSlot()
    {
        itemImage.enabled = false;   // 이미지 숨기기
        count.text = string.Empty;   // 수량 텍스트 비우기
        gameObject.SetActive(false); // 슬롯 비활성화
    }
}
