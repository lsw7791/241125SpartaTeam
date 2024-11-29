using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataManager : SingleTon<DataManager>
{
    public ItemDataManager item;
    public CreatureDataManager creature;
    public TableDataManager table;
    public PotionDataManager potion;

    public void Initialize()
    {
        Debug.Log("������");
        UnityGoogleSheet.LoadAllData();
        Debug.Log("������");
        item = new ItemDataManager();
        creature = new CreatureDataManager();
        table = new TableDataManager();
        potion = new PotionDataManager();

    }
}
