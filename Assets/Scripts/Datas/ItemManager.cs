using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingleTon<ItemManager>
{
    private List<ItemInstance> _items;
    // ������ �����͸� �����ϴ� ����Ʈ
    public void Initialize(List<ItemInstance> items)
    {
        _items = items;
    }

    public ItemInstance GetByID(int id)
    { // ���� ID�� �������� �������� �˻�
        return _items.Find(x => x.id == id);
    }

    //����Ʈ�ε� ������ �� �ִ�
    //public List<ItemInstance> GetAllByID(int id)
    //{
    //    return _items.FindAll(x => x.id == id);
    //}

    public ItemInstance GetItemByID(int itemid)
    { // ������ ID�� �������� �������� �˻�
        return _items.Find(x => x.itemId == itemid);
    }
    public ItemInstance GetItemByType(ItemType itemtype)
    { // ������ Ÿ��(ItemType)�� �������� �������� �˻�
        return _items.Find(x => x.type == itemtype);
    }
    public ItemInstance GetItemByName(string itemname)
    { // ������ �̸��� �������� �������� �˻�
        return _items.Find(x => x.name == itemname);
    }
    public ItemInstance GetItemByTier(int itemtier)
    { // ������ ���(Tier)�� �������� �������� �˻�
        return _items.Find(x => x.tier == itemtier);
    }
}

