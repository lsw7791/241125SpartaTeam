using MainData;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : UIBase
{
    public GameObject itemPrefab;  // ������ UI ������
    public Transform itemsParent;  // �����۵��� ��ġ�� �θ� ��ü

    private void Awake()
    {
        base.Awake();
        // ������ �ε� Ȯ��
        itemPrefab = Resources.Load<GameObject>("Prefabs/UI/ShopSlot");
        if (itemPrefab == null)
        {
            Debug.LogError("Item prefab is not found!");
        }
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
            else
            {
                Debug.LogError("ShopSlot component not found on the instantiated itemPrefab.");
            }
        }
    }
}
