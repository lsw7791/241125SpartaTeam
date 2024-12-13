using MainData;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : UIBase
{
    public ShopType shopType; // ���� Ÿ��
    public GameObject itemPrefab;
    public Transform itemsParent;
    public TMP_Text _hasGold;
    public ScrollRect scrollRect;  // ��ũ�� Rect

    private void Awake()
    {
        base.Awake();
        _hasGold.text = $"{GameManager.Instance.player.stats.Gold}";
        itemPrefab = Resources.Load<GameObject>("Prefabs/UI/ShopSlot");
    }

    // ���� Ÿ���� �����ϰ� UI ������Ʈ
    public void SetShopType(ShopType newShopType)
    {
        shopType = newShopType;  // ���ο� ���� Ÿ�� ����
        SetupShopUI();  // ���� UI ����
    }

    public void SetupShopUI()
    {
        // ���� ���� Ÿ�Կ� ���� ������ �ҷ�����
        List<ItemData> items = GameManager.Instance.dataManager.shop.GetItems(shopType);

        // ���� �������� �����ϰ� ���ο� �������� ǥ��
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
