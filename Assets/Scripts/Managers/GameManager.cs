using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;
using Tripolygon.UModeler.UI.ViewModels;

public class GameManager : MonoSingleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        DataManager.Instance.Initialize();

        // Use JaSon Data To user Item information
        List<ItemInstance> items = new List<ItemInstance>();
        {
        items.Add(new ItemInstance { id = 1, itemid = 1, itemtype=(ItemType)1, itemname ="½ÇÇè1" });
        }
        ItemManager.Instance.Initialize(items);

        Monster monster = new Monster();
        monster.Initialize(2);
        Debug.Log(monster.creatureName);
    }
}
