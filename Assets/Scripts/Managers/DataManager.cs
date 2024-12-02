using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataManager : SingleTon<DataManager>
{
    public ItemDataManager item;
    public CreatureDataManager creature;
    public CraftingDataManager crafting;

    public void Initialize()
    {
        UnityGoogleSheet.LoadAllData();
        item = new ItemDataManager();
        creature = new CreatureDataManager();
        crafting = new CraftingDataManager();

    }
}
