using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;
using Tripolygon.UModeler.UI.ViewModels;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]private GameObject goblinPrefab;  // ��� ������
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
        items.Add(new ItemInstance { id = 1, itemid = 1, itemtype=(ItemType)1, itemname ="����1" });
        }
        ItemManager.Instance.Initialize(items);


        monsterPool = new MonsterPool();
        monsterPool.InitializeMonsterPool("Goblin", goblinPrefab, 10, 1);  // ��� Ǯ �ʱ�ȭ

        SpawnGoblin(new Vector2(1f, 1f));
    }

    void SpawnGoblin(Vector2 position)
    {
        GameObject goblin = monsterPool.GetMonster("Goblin");  // ��� ����
        goblin.transform.position = position;  // ��ġ ����
    }

    void ReturnGoblin(GameObject goblin)
    {
        monsterPool.ReturnMonster("Goblin", goblin);  // ����� Ǯ�� ��ȯ
    }
}
