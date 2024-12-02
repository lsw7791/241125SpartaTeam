using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataManager : SingleTon<DataManager>
{
    public ItemDataManager item;
    public CreatureDataManager creature;
    public CraftingDataManager table;

    public void Initialize()
    {
        Debug.Log("������");
        UnityGoogleSheet.LoadAllData();
        Debug.Log("������");
        item = new ItemDataManager();
        creature = new CreatureDataManager();
        table = new CraftingDataManager();

    }
}
