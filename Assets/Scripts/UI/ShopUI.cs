using MainData;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUI : UIBase
{
    public GameObject itemPrefab;  // ������ UI ������
    public Transform itemsParent;  // �����۵��� ��ġ�� �θ� ��ü
    public TMP_Text _hasGold;

    private void Awake()
    {
        base.Awake();
        _hasGold.text = $"{GameManager.Instance.player.stats.Gold}";

        // ������ �ε� Ȯ��
        itemPrefab = Resources.Load<GameObject>("Prefabs/UI/ShopSlot");
        SetupShopUI();
    }

    public void SetupShopUI()
    {
        List<ItemData> items = GameManager.Instance.dataManager.GetItemsForShop();
        Debug.Log("Items count: " + items.Count); // ������ �� Ȯ��

        if (items.Count == 0)
        {
            Debug.LogWarning("No items found for the shop.");
            return;
        }

        // �������� �������� �����Ͽ� UI�� �߰�
        foreach (ItemData itemData in items)
        {
            Debug.Log("Item: " + itemData.name); // �� ������ ������ Ȯ��

            // ������ UI ���� ����
            GameObject itemObject = Instantiate(itemPrefab, itemsParent);

            // ������ ���Կ��� ShopSlot ������Ʈ�� ã�Ƽ� �����͸� ����
            ShopSlot shopSlot = itemObject.GetComponent<ShopSlot>();
            if (shopSlot != null)
            {
                shopSlot.Setup(itemData); // ������ �����͸� ���Կ� ����
            }
        }
    }

}
