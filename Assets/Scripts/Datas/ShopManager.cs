using MainData;
using System.Collections.Generic;

public enum ShopType
{
    PotionShop,
    WeaponShop,
    ArmorShop,
    TarvenShop,
    BuyShop
}


public class ShopManager
{
    public List<ItemData> GetItems(ShopType shopType)
    {
        List<ItemData> items = new List<ItemData>();

        switch (shopType)
        {
            case ShopType.PotionShop:
                for (int i = 89; i <= 94; i++)
                {
                    ItemData itemData = GameManager.Instance.DataManager.GetItemDataById(i);
                    items.Add(itemData);
                }
                break;

            case ShopType.WeaponShop:
                for (int i = 95; i <= 105; i++)
                {
                    ItemData itemData = GameManager.Instance.DataManager.GetItemDataById(i);
                    items.Add(itemData);
                }
                break;

            case ShopType.ArmorShop:
                for (int i = 31; i <= 60; i++) // 방어구 아이템 ID 설정
                {
                    ItemData itemData = GameManager.Instance.DataManager.GetItemDataById(i);
                    items.Add(itemData);
                }
                break;
                case ShopType.TarvenShop:
                for (int i = 107; i <= 107; i++) // 방어구 아이템 ID 설정
                {
                    ItemData itemData = GameManager.Instance.DataManager.GetItemDataById(i);
                    items.Add(itemData);
                }
                break;
            case ShopType.BuyShop:
                
                    foreach (InventoryItem item in GameManager.Instance.Player.Inventory.Items)
                    {
                        ItemData itemData = GameManager.Instance.DataManager.GetItemDataById(item.ItemID);
                        items.Add(itemData);
                    }
                        //ItemData itemData = GameManager.Instance.DataManager.GetItemDataById(i);
                break;
        }

        return items;
    }
}

