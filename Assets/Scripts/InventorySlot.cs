using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemImage; // 슬롯에 표시될 아이템 이미지
    [SerializeField] private TMP_Text count;
    private InventoryItem item;

    private void Start()
    {
        Button slotButton = GetComponent<Button>();

        slotButton.onClick.AddListener(() =>
        {
            GameObject itemUseMenu = UIManager.Instance.GetUI<InventoryUI>().itemUseMenu;
            itemUseMenu.SetActive(true);
            itemUseMenu.TryGetComponent<ItemUseUI>(out var outUseUI);
            outUseUI.Initialize(item);
        });
    }

    private void Update()
    {
        if (item != null)
        {
            UpdateSlot();
        }
    }

    // 아이템 초기화
    public void Initialize(InventoryItem inItem)
    {
        itemImage.sprite = inItem.ItemIcon;  // 아이템의 아이콘 설정
        itemImage.enabled = true;            // 이미지 표시
        int quantity = inItem.Quantity;
        count.text = quantity.ToString();
        item = inItem;
    }

    // 빈 슬롯 처리
    public void ClearSlot()
    {
        gameObject.SetActive(false);  // 빈 슬롯 이미지 설정
    }

    private void UpdateSlot()
    {
        Initialize(item);
        // 아이템 정보 초기화

        if (item.IsEquipped)
        { // 아이템이 장착 중일 때
            count.text = "사용중";
        }
        else
        {
            int quantity = item.Quantity;
            count.text = quantity.ToString();
        }
    }
}
