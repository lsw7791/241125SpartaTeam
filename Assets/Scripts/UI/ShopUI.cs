using MainData;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : UIBase
{
    public ShopType shopType; // 상점 타입
    public GameObject itemPrefab;
    public Transform itemsParent;
    public TMP_Text _hasGold;
    public ScrollRect scrollRect;  // 스크롤 Rect

    private void Awake()
    {
        base.Awake();
        _hasGold.text = $"{GameManager.Instance.player.stats.Gold}";
        itemPrefab = Resources.Load<GameObject>("Prefabs/UI/ShopSlot");
    }

    // 상점 타입을 설정하고 UI 업데이트
    public void SetShopType(ShopType newShopType)
    {
        shopType = newShopType;  // 새로운 상점 타입 설정
        SetupShopUI();  // 상점 UI 설정
    }

    public void SetupShopUI()
    {
        // 현재 상점 타입에 따라 아이템 불러오기
        List<ItemData> items = GameManager.Instance.dataManager.shop.GetItems(shopType);

        // 기존 아이템을 제거하고 새로운 아이템을 표시
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemData itemData in items)
        {
            GameObject itemObject = Instantiate(itemPrefab, itemsParent);
            ShopSlot shopSlot = itemObject.GetComponent<ShopSlot>();
            if (shopSlot != null)
            {
                shopSlot.Setup(itemData);
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(itemsParent.GetComponent<RectTransform>());
    }
}
