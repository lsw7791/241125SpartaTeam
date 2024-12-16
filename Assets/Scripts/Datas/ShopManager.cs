using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShopType
{
    PotionShop,
    WeaponShop,
    ArmorShop 
}


public class ShopManager : MonoBehaviour
{
    public List<ItemData> GetItems(ShopType shopType)
    {
        List<ItemData> items = new List<ItemData>();

        switch (shopType)
        {
            case ShopType.PotionShop:
                for (int i = 89; i <= 94; i++)
                {
                    ItemData itemData = GameManager.Instance.dataManager.GetItemDataById(i);
                    items.Add(itemData);
                }
                break;

            case ShopType.WeaponShop:
                for (int i = 95; i <= 105; i++)
                {
                    ItemData itemData = GameManager.Instance.dataManager.GetItemDataById(i);
                    items.Add(itemData);
                }
                break;

            case ShopType.ArmorShop:
                for (int i = 31; i <= 60; i++) // 방어구 아이템 ID 설정
                {
                    ItemData itemData = GameManager.Instance.dataManager.GetItemDataById(i);
                    items.Add(itemData);
                }
                break;
        }

        return items;
    }
}

