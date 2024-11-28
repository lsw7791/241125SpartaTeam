using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class DataLoadTest : MonoBehaviour
{
    void Awake()
    {
        UnityGoogleSheet.LoadAllData();
        // UnityGoogleSheet.Load<DefaultTable.Data.Load>(); it's same!
        // or call DefaultTable.Data.Load(); it's same!
    }

    void Start()
    {
        foreach (var value in WeaponData.WeaponData.WeaponDataList)
        {
            Debug.Log(value.index + "," + value.intValue + "," + value.strValue);
        }
        var dataFromMap = WeaponData.WeaponData.WeaponDataMap[0];
        Debug.Log("dataFromMap : " + dataFromMap.index + ", " + dataFromMap.intValue + "," + dataFromMap.strValue);

        UnityGoogleSheet.LoadFromGoogle<int, ArmorData.Data>((list, map) => {
            list.ForEach(x => {
                Debug.Log(x.intValue);
            });
        }, true);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
