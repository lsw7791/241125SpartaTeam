using MainData;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    // UI 요소들
    public TMP_Text itemNameText;   // 아이템 이름 표시
    public TMP_Text itemPriceText;  // 아이템 가격 표시
    public Image icon;              // 아이템 아이콘
    public TMP_Text itemDescText;   // 아이템 설명 표시
    public Button buyButton;        // 아이템 구매 버튼
    private ShopType type;
    private ItemData currentItemData; // 현재 슬롯에 할당된 아이템 데이터

    public delegate void ItemClickHandler(ItemData itemData);
    public event ItemClickHandler OnItemClick; // 아이템 클릭 시 발생하는 이벤트


    // 아이템 데이터를 슬롯에 설정
    public void Setup(ItemData itemData, ShopType shopType)
    {
        currentItemData = itemData;
        type = shopType;
        // UI에 아이템 이름, 가격, 설명, 아이콘 설정
        itemNameText.text = itemData.name;
        if(shopType == ShopType.BuyShop)
        {
            itemPriceText.text = $"{itemData.sell} Gold";
        }
        else
        {
        itemPriceText.text = $"{itemData.buy} Gold";
        }
        itemDescText.text = itemData.desc;

        // ShopType에 따른 아이콘 경로 설정
        if (shopType == ShopType.PotionShop)
        {
            icon.sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);
        }
        else if(shopType == ShopType.WeaponShop || shopType == ShopType.ArmorShop)
        {
            icon.sprite = UIManager.Instance.craftingAtlas.GetSprite(itemData.atlasPath);
        }
        else if(shopType == ShopType.TarvenShop)
        {
            icon.sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);
        }
        else if(shopType == ShopType.BuyShop)
        {
            icon.sprite = UIManager.Instance.ItemAtlas.GetSprite(itemData.atlasPath);
        }
        // 구매 버튼 활성화 여부 설정
        buyButton.interactable = true;

        // 아이템 클릭 시 발생하는 이벤트 설정
        buyButton.onClick.AddListener(() => OnItemClick?.Invoke(itemData));
    }



    // 슬롯 클릭 시 호출 (아이템 클릭 시 BuyUI 활성화)
    public void OnclickedSlot()
    {
        if(type== ShopType.BuyShop)
        {
            // BuyUI 활성화 및 아이템 데이터 전달
            UIManager.Instance.ToggleUI<SellUI>();
            SellUI sellUI = UIManager.Instance.OpenUI<SellUI>();
            sellUI.transform.SetAsLastSibling();

            // BuyUI에 아이템 데이터 전달
            sellUI.SetUp(currentItemData); // BuyUI에 아이템 데이터 설정  
        }
        else
        {
        // BuyUI 활성화 및 아이템 데이터 전달
        UIManager.Instance.ToggleUI<BuyUI>();
        BuyUI buyUI = UIManager.Instance.OpenUI<BuyUI>();
        buyUI.transform.SetAsLastSibling();

        // BuyUI에 아이템 데이터 전달
        buyUI.SetUp(currentItemData); // BuyUI에 아이템 데이터 설정        
        }
    }
}
