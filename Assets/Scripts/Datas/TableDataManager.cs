using MainData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableDataManager : TableData
{
    public TableData GetData(int id)
    {
        return TableDataMap[id];
    }

    public List<TableData> GetDataByType()
    {
        //return TableDataList.FindAll(x=> x.type == 1)
        return null;
    }
}
