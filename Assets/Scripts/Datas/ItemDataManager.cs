using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;
using GoogleSheet.Core.Type;
[UGS(typeof(ItemType))]
public enum ItemType
{
    Sword=1,
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
    Gold
}
public class ItemDataManager : ItemData
{
    public ItemData GetData(int id)
    {
        return ItemDataMap[id];
    }

    public List<ItemData> GetDataByType()
    {
        //return ItemDataList.FindAll(x=> x.type == 1)
        return null;
    }
    public List<ItemData> GetAllData(int id)
    {

        return new List<ItemData>();
    }

}
