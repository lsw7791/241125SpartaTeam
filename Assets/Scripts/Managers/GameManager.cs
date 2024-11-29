using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;

public class GameManager : MonoSingleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();

        DataManager.Instance.Initialize();

        ItemData testItem = DataManager.Instance.item.GetData(2);
        Debug.Log(testItem.itemData);

        TableData testtable = DataManager.Instance.table.GetData(2);
        Debug.Log(testtable.tableData);

        CreatureData testcreature = DataManager.Instance.creature.GetData(2);
        Debug.Log(testcreature.creatureData);

        PotionData testpotion = DataManager.Instance.potion.GetData(2);
        Debug.Log(testpotion.potionData);
    }
    private void Start()
    {
    }
}
