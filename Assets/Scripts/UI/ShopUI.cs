using MainData;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : UIBase
{
    public GameObject itemPrefab;  // 아이템 UI 프리팹
    public Transform itemsParent;  // 아이템들이 배치될 부모 객체

    private void Awake()
    {
        base.Awake();
        // 프리팹 로드 확인
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
        Debug.Log("Items count: " + items.Count); // 아이템 수 확인

        if (items.Count == 0)
        {
            Debug.LogWarning("No items found for the shop.");
            return;
        }

        // 아이템을 동적으로 생성하여 UI에 추가
        foreach (ItemData itemData in items)
        {
            Debug.Log("Item: " + itemData.name); // 각 아이템 데이터 확인

            // 아이템 UI 슬롯 생성
            GameObject itemObject = Instantiate(itemPrefab, itemsParent);

            // 생성된 슬롯에서 ShopSlot 컴포넌트를 찾아서 데이터를 설정
            ShopSlot shopSlot = itemObject.GetComponent<ShopSlot>();
            if (shopSlot != null)
            {
                shopSlot.Setup(itemData); // 아이템 데이터를 슬롯에 설정
            }
            else
            {
                Debug.LogError("ShopSlot component not found on the instantiated itemPrefab.");
            }
        }
    }
}
