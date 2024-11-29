using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataManager : SingleTon<DataManager>
{
    public ToolDataManager tool;
    public CreatureDataManager creature;
    public ResourceDataManager resource;
    public PotionDataManager potion;

    public void Initialize()
    {
        Debug.Log("시작전");
        UnityGoogleSheet.LoadAllData();
        Debug.Log("시작후");
        tool = new ToolDataManager();
        creature = new CreatureDataManager();
        resource = new ResourceDataManager();
        potion = new PotionDataManager();

    }
}
