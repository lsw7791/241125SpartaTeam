using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingleTon<ItemManager>
{
    private List<ItemInstance> _items;
    public void Initialize(List<ItemInstance> items)
    {
        _items = items;
    }

    public ItemInstance GetID(int id)
    {
        return _items.Find(x => x.id == id);
    }
    public ItemInstance GetItemID(int itemid)
    {
        return _items.Find(x => x.itemid == itemid);
    }
    public ItemInstance GetItemType(ItemType itemtype)
    {
        return _items.Find(x => x.itemtype == itemtype);
    }
    public ItemInstance GetItemName(string itemname)
    {
        return _items.Find(x => x.itemname == itemname);
    }
    public ItemInstance GetItemDesc(string itemdesc)
    {
        return _items.Find(x => x.itemdesc == itemdesc);
    }
    public ItemInstance GetItemTier(int itemtier)
    {
        return _items.Find(x => x.itemtier == itemtier);
    }
    public ItemInstance GetItemHealth(int itemhealth)
    {
        return _items.Find(x => x.itemhealth == itemhealth);
    }
    public ItemInstance GetItemStamina(int itemstamina)
    {
        return _items.Find(x => x.itemstamina == itemstamina);
    }
    public ItemInstance GetItemDefense(int itemdefense)
    {
        return _items.Find(x => x.itemdefense == itemdefense);
    }
    public ItemInstance GetItemAttackP(int itemattackp)
    {
        return _items.Find(x => x.itemattackp == itemattackp);
    }
    public ItemInstance GetItemAttackM(int itemattackm)
    {
        return _items.Find(x => x.itemattackm == itemattackm);
    }
    public ItemInstance GetItemAttackMining(int itemattackmining)
    {
        return _items.Find(x => x.itemattackmining == itemattackmining);
    }
    public ItemInstance GetItemSell(int itemsell)
    {
        return _items.Find(x => x.itemsell == itemsell);
    }
    public ItemInstance GetItemBuy(int itembuy)
    {
        return _items.Find(x => x.itembuy == itembuy);
    }
    public ItemInstance GetItemSpeed(float itemSpeed)
    {
        return _items.Find(x => x.itemSpeed == itemSpeed);
    }
    public ItemInstance GetItemDrop(float itemdrop)
    {
        return _items.Find(x => x.itemdrop == itemdrop);
    }
}

