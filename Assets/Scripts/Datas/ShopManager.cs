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
                for (int i = 90; i <= 95; i++)
                {
                    ItemData itemData = GameManager.Instance.dataManager.GetItemDataById(i);
                    items.Add(itemData);
                }
                break;

            case ShopType.WeaponShop:
                for (int i = 96; i <= 106; i++)
                {
                    ItemData itemData = GameManager.Instance.dataManager.GetItemDataById(i);
                    items.Add(itemData);
                }
                break;

            case ShopType.ArmorShop:
                // ArmorShop 아이템 처리
                break;
        }

        return items;
    }
}


