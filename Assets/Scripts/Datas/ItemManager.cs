using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingleTon<ItemManager>
{
    private List<ItemInstance> _items;
    // 아이템 데이터를 저장하는 리스트
    public void Initialize(List<ItemInstance> items)
    {
        _items = items;
    }

    public ItemInstance GetByID(int id)
    { // 유일 ID를 기준으로 아이템을 검색
        return _items.Find(x => x.id == id);
    }

    //리스트로도 가져올 수 있다
    //public List<ItemInstance> GetAllByID(int id)
    //{
    //    return _items.FindAll(x => x.id == id);
    //}

    public ItemInstance GetItemByID(int itemid)
    { // 데이터 ID를 기준으로 아이템을 검색
        return _items.Find(x => x.itemId == itemid);
    }
    public ItemInstance GetItemByType(ItemType itemtype)
    { // 아이템 타입(ItemType)을 기준으로 아이템을 검색
        return _items.Find(x => x.type == itemtype);
    }
    public ItemInstance GetItemByName(string itemname)
    { // 아이템 이름을 기준으로 아이템을 검색
        return _items.Find(x => x.name == itemname);
    }
    public ItemInstance GetItemByTier(int itemtier)
    { // 아이템 등급(Tier)을 기준으로 아이템을 검색
        return _items.Find(x => x.tier == itemtier);
    }
}

