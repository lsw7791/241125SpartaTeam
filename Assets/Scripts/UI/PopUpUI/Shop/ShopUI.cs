using MainData;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : UIBase
{
    public ShopType ShopType; // 상점 타입
    public GameObject ItemPrefab;
    public Transform ItemsParent;
    public TMP_Text HasGold;
    public RectTransform ScrollContent;

    // ScrollRect 추가
    public ScrollRect scrollRect;

    // 버튼 추가
    public Button WeaponButton;
    public Button ArmorButton;

    protected override void Awake()
    {
        base.Awake();
        HasGold.text = $"{GameManager.Instance.Player.stats.Gold}";
        ItemPrefab = Resources.Load<GameObject>("Prefabs/UI/ShopSlot");

        // 버튼 클릭 이벤트 연결
        WeaponButton.onClick.AddListener(() => ChangeShopType(ShopType.WeaponShop));
        ArmorButton.onClick.AddListener(() => ChangeShopType(ShopType.ArmorShop));

        // ScrollRect 스크롤 제한 등록
        scrollRect.onValueChanged.AddListener(LimitScrollPosition);
    }

    // 스크롤 범위 제한
    private void LimitScrollPosition(Vector2 normalizedPosition)
    {
        // 현재 ScrollContent의 위치 가져오기
        Vector2 currentPos = ScrollContent.anchoredPosition;

        // y값이 0 이하로 내려가지 않도록 제한
        if (currentPos.y < 0)
        {
            currentPos.y = 0;
            ScrollContent.anchoredPosition = currentPos;
        }
    }

    // 상점 타입을 설정하고 UI 업데이트
    public void SetShopType(ShopType newShopType)
    {
        ShopType = newShopType; // 새로운 상점 타입 설정
        SetupShopUI(); // 상점 UI 설정
        UpdateButtons(); // 버튼 상태 업데이트
    }

    private void OnEnable()
    {
        ScrollContent.anchoredPosition = new Vector2(ScrollContent.anchoredPosition.x, 0);
    }

    public void SetupShopUI()
    {
        // 현재 상점 타입에 따라 아이템 불러오기
        List<ItemData> items = GameManager.Instance.DataManager.Shop.GetItems(ShopType);

        // 기존 아이템을 제거하고 새로운 아이템을 표시
        foreach (Transform child in ItemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemData itemData in items)
        {
            GameObject itemObject = Instantiate(ItemPrefab, ItemsParent);
            itemObject.TryGetComponent<ShopSlot>(out var shopslot);
            shopslot.Setup(itemData, ShopType);  // ShopType을 전달
        }
    }


    // 상점 타입에 따른 버튼 상태 업데이트
    private void UpdateButtons()
    {
        if (ShopType == ShopType.PotionShop)
        {
            WeaponButton.gameObject.SetActive(false);
            ArmorButton.gameObject.SetActive(false);
        }
        else
        {
            WeaponButton.gameObject.SetActive(true);
            ArmorButton.gameObject.SetActive(true);
        }
    }

    // 상점 타입 변경 (버튼 클릭)
    private void ChangeShopType(ShopType newShopType)
    {
        SetShopType(newShopType);
    }
}
