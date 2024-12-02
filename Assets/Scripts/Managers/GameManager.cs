using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;
using Tripolygon.UModeler.UI.ViewModels;

public class GameManager : MonoSingleton<GameManager>
{
    public ObjectPool<Monster> monsterPool;  // ���� ������ Ǯ
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
        items.Add(new ItemInstance { id = 1, itemid = 1, itemtype=(ItemType)1, itemname ="����1" });
        }
        ItemManager.Instance.Initialize(items);

        MonsterData monster = new MonsterData();
        monster.Initialize(2);
        Debug.Log(monster.creatureName);
    }
}
