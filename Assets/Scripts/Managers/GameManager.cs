using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;
using Tripolygon.UModeler.UI.ViewModels;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]private GameObject goblinPrefab;  // 고블린 프리팹
    private MonsterPool monsterPool;

    protected override void Awake()
    {
        base.Awake();
        goblinPrefab = Resources.Load<GameObject>("Prefabs/Monsters/Goblin");
    }
    private void Start()
    {

        DataManager.Instance.Initialize();

        // Use JaSon Data To user Item information
        List<ItemInstance> items = new List<ItemInstance>();
        {
        items.Add(new ItemInstance { id = 1, itemid = 1, itemtype=(ItemType)1, itemname ="실험1" });
        }
        ItemManager.Instance.Initialize(items);


        monsterPool = new MonsterPool();
        monsterPool.InitializeMonsterPool("Goblin", goblinPrefab, 10, 1);  // 고블린 풀 초기화

        SpawnGoblin(new Vector2(1f, 1f));
    }

    void SpawnGoblin(Vector2 position)
    {
        GameObject goblin = monsterPool.GetMonster("Goblin");  // 고블린 생성
        goblin.transform.position = position;  // 위치 설정
    }

    void ReturnGoblin(GameObject goblin)
    {
        monsterPool.ReturnMonster("Goblin", goblin);  // 고블린을 풀에 반환
    }
}
