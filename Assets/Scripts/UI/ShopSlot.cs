using MainData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    // UI 요소들
    public TMP_Text itemNameText;   // 아이템 이름 표시
    public TMP_Text itemPriceText;  // 아이템 가격 표시
    public Image icon;              // 아이템 아이콘
    public TMP_Text itemDescText;   // 아이템 설명 표시
    public Button buyButton;        // 아이템 구매 버튼

    private ItemData currentItemData; // 현재 슬롯에 할당된 아이템 데이터

    public delegate void ItemClickHandler(ItemData itemData);
    public event ItemClickHandler OnItemClick; // 아이템 클릭 시 발생하는 이벤트

   
    // 아이템 데이터를 슬롯에 설정
    public void Setup(ItemData itemData)
    {
        currentItemData = itemData;

        // UI에 아이템 이름, 가격, 설명, 아이콘 설정
        itemNameText.text = itemData.name;
        itemPriceText.text = $"{itemData.buy} Gold";
        itemDescText.text = itemData.desc;
        icon.sprite = Resources.Load<Sprite>(itemData.spritePath); // 아이콘 경로로부터 이미지 로드

        // 구매 버튼 활성화 여부 설정
        buyButton.interactable = GameManager.Instance.player.stats.Gold >= itemData.buy;

        // 아이템 클릭 시 발생하는 이벤트 설정
        buyButton.onClick.AddListener(() => OnItemClick?.Invoke(itemData));
    }
  

    // 슬롯 클릭 시 호출 (아이템 클릭 시 BuyUI 활성화)
    public void OnclickedSlot()
    {
        // BuyUI 활성화 및 아이템 데이터 전달
        if (currentItemData != null)
        {
            GameManager.Instance.uIManager.ToggleUI<BuyUI>();

            // BuyUI에 아이템 데이터 전달
            BuyUI buyUI = GameManager.Instance.uIManager.GetUI<BuyUI>();
            if (buyUI != null)
            {
                buyUI.SetUp(currentItemData); // BuyUI에 아이템 데이터 설정
            }
        }
    }
}
