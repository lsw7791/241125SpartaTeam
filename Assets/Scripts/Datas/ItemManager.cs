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

    public ItemInstance GetByID(int id)
    {
        return _items.Find(x => x.id == id);
    }

    //리스트로도 가져올 수 있다
    //public List<ItemInstance> GetAllByID(int id)
    //{
    //    return _items.FindAll(x => x.id == id);
    //}

    public ItemInstance GetItemByID(int itemid)
    {
        return _items.Find(x => x.itemId == itemid);
    }
    public ItemInstance GetItemByType(ItemType itemtype)
    {
        return _items.Find(x => x.type == itemtype);
    }
    public ItemInstance GetItemByName(string itemname)
    {
        return _items.Find(x => x.name == itemname);
    }
    public ItemInstance GetItemByTier(int itemtier)
    {
        return _items.Find(x => x.tier == itemtier);
    }
}

