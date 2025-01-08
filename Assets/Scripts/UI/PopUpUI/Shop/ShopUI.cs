using MainData;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : UIBase
{
    public ShopType ShopType; // ���� Ÿ��
    public GameObject ItemPrefab;
    public Transform ItemsParent;
    public TMP_Text HasGold;
    public RectTransform ScrollContent;

    // ScrollRect �߰�
    public ScrollRect scrollRect;

    // ��ư �߰�
    public Button WeaponButton;
    public Button ArmorButton;

    protected override void Awake()
    {
        base.Awake();
        HasGold.text = $"{GameManager.Instance.Player.stats.Gold}";
        ItemPrefab = Resources.Load<GameObject>("Prefabs/UI/ShopSlot");

        // ��ư Ŭ�� �̺�Ʈ ����
        WeaponButton.onClick.AddListener(() => ChangeShopType(ShopType.WeaponShop));
        ArmorButton.onClick.AddListener(() => ChangeShopType(ShopType.ArmorShop));

        // ScrollRect ��ũ�� ���� ���
        scrollRect.onValueChanged.AddListener(LimitScrollPosition);
    }

    // ��ũ�� ���� ����
    private void LimitScrollPosition(Vector2 normalizedPosition)
    {
        // ���� ScrollContent�� ��ġ ��������
        Vector2 currentPos = ScrollContent.anchoredPosition;

        // y���� 0 ���Ϸ� �������� �ʵ��� ����
        if (currentPos.y < 0)
        {
            currentPos.y = 0;
            ScrollContent.anchoredPosition = currentPos;
        }
    }

    // ���� Ÿ���� �����ϰ� UI ������Ʈ
    public void SetShopType(ShopType newShopType)
    {
        ShopType = newShopType; // ���ο� ���� Ÿ�� ����
        SetupShopUI(); // ���� UI ����
        UpdateButtons(); // ��ư ���� ������Ʈ
    }

    private void OnEnable()
    {
        ScrollContent.anchoredPosition = new Vector2(ScrollContent.anchoredPosition.x, 0);
    }

    public void SetupShopUI()
    {
        // ���� ���� Ÿ�Կ� ���� ������ �ҷ�����
        List<ItemData> items = GameManager.Instance.DataManager.Shop.GetItems(ShopType);

        // ���� �������� �����ϰ� ���ο� �������� ǥ��
        foreach (Transform child in ItemsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (ItemData itemData in items)
        {
            GameObject itemObject = Instantiate(ItemPrefab, ItemsParent);
            itemObject.TryGetComponent<ShopSlot>(out var shopslot);
            shopslot.Setup(itemData, ShopType);  // ShopType�� ����
        }
    }


    // ���� Ÿ�Կ� ���� ��ư ���� ������Ʈ
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

    // ���� Ÿ�� ���� (��ư Ŭ��)
    private void ChangeShopType(ShopType newShopType)
    {
        SetShopType(newShopType);
    }
}
