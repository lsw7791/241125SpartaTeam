using System.Collections.Generic;
using UnityEngine;
using MainData;
using GoogleSheet.Core.Type;

// ���� ��Ʈ�� "ItemType" ���� ���εǴ� ������
[UGS(typeof(ItemType))]
public enum ItemType
{
    Sword = 1,
    Bow,
    Staff,
    Shield,
    Pickaxe,
    Helmet,
    Top,
    Bottom,
    Armor,
    Cape,
    Mine,
    Jewel,
    Ladder,
    Other,
    Gold,
    HealthPotion,
    StaminaPotion
}

// ������ �����͸� �����ϴ� �Ŵ��� Ŭ����
public class ItemDataManager : ItemData
{
    // ��� ������ �����͸� ��ȯ
    public List<ItemData> GetItemDatas()
    {
        return ItemDataList;
    }

    // Ư�� ID�� ������ �����͸� ��ȯ
    public ItemData GetData(int id)
    {
        if (ItemDataMap.ContainsKey(id))
        {
            return ItemDataMap[id];
        }

        return null;
    }

    // Ư�� Ÿ��(ItemType)�� ��� ������ �����͸� ��ȯ
    public List<ItemData> GetDataByType(ItemType type)
    {
        return ItemDataList.FindAll(x => x.itemType == type);
    }

    // �������� ������ ��� �����͸� ��ȯ
    public ItemData GetRandomDropItem(List<int> dropItemIds)
    {
        List<ItemData> validItems = new List<ItemData>();

        // ����� �� �ִ� ������ �����͸� ���͸�
        foreach (int id in dropItemIds)
        {
            if (ItemDataMap.ContainsKey(id))
            {
                validItems.Add(ItemDataMap[id]);
            }
        }

        if (validItems.Count > 0)
        {
            // �������� ��� ������ ����
            int randomIndex = Random.Range(0, validItems.Count);
            return validItems[randomIndex];
        }

        return null;
    }

    // Ư�� ID�� ������ �����͸� ��ȯ (�߰��� �޼���)
    public ItemData GetItemDataById(int itemId)
    {
        if (ItemDataMap.ContainsKey(itemId))
        {
            return ItemDataMap[itemId];
        }

        return null;
    }

}
