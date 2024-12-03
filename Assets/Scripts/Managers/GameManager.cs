using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;
using Tripolygon.UModeler.UI.ViewModels;
using UnityEngine.UIElements;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GameObject goblinPrefab;  // ��� ������
    [SerializeField] private GameObject lizardPrefab;  
    [SerializeField] private GameObject zombiePrefab;  
    [SerializeField] private GameObject orcShamanPrefab;  
    [SerializeField] private GameObject BigzombiePrefab;  
    [SerializeField] private GameObject ImpPrefab;
    [SerializeField] private GameObject skeletPrefab;
    [SerializeField] private GameObject iceZombiePrefab;
    [SerializeField] private GameObject ogrePrefab;
    [SerializeField] private GameObject knightPrefab;
    [SerializeField] private GameObject necromancerPrefab;
    [SerializeField] private GameObject demonPrefab;


    private MonsterPool goblinPool;
    private MonsterPool lizardPool;
    private MonsterPool zombiePool;
    private MonsterPool orcShamanPool;
    private MonsterPool bigZombiePool;
    private MonsterPool impPool;
    private MonsterPool skeletonPool;
    private MonsterPool iceZombiePool;
    private MonsterPool ogrePool;
    private MonsterPool knightPool;
    private MonsterPool necromancerPool;
    private MonsterPool demonPool;

    [SerializeField]
    protected override void Awake()
    {
        base.Awake();
        goblinPrefab = Resources.Load<GameObject>("Prefabs/Monsters/Goblin");
        lizardPrefab = Resources.Load<GameObject>("Prefabs/Monsters/Lizard");
        zombiePrefab = Resources.Load<GameObject>("Prefabs/Monsters/Zombie");
        orcShamanPrefab = Resources.Load<GameObject>("Prefabs/Monsters/OrcShaman");
        BigzombiePrefab = Resources.Load<GameObject>("Prefabs/Monsters/BigZombie");
        ImpPrefab = Resources.Load<GameObject>("Prefabs/Monsters/Imp");
        skeletPrefab = Resources.Load<GameObject>("Prefabs/Monsters/Skelet");
        iceZombiePrefab = Resources.Load<GameObject>("Prefabs/Monsters/IceZombie");
        ogrePrefab = Resources.Load<GameObject>("Prefabs/Monsters/Ogre");
        knightPrefab = Resources.Load<GameObject>("Prefabs/Monsters/Knight");
        necromancerPrefab = Resources.Load<GameObject>("Prefabs/Monsters/Necromancer");
        demonPrefab = Resources.Load<GameObject>("Prefabs/Monsters/Demon");

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


        // ���� Ǯ �ʱ�ȭ
        goblinPool = new MonsterPool();
        goblinPool.InitializeMonsterPool("Goblin", goblinPrefab, 2, 1);  // ��� Ǯ �ʱ�ȭ

        lizardPool = new MonsterPool();
        lizardPool.InitializeMonsterPool("Lizard", lizardPrefab, 2, 2);  // ������ Ǯ �ʱ�ȭ

        zombiePool = new MonsterPool();
        zombiePool.InitializeMonsterPool("Zombie", zombiePrefab, 2, 3);  // ���� Ǯ �ʱ�ȭ

        orcShamanPool = new MonsterPool();
        orcShamanPool.InitializeMonsterPool("OrcShaman", orcShamanPrefab, 2, 4);  // ��ũ ���� Ǯ �ʱ�ȭ

        bigZombiePool = new MonsterPool();
        bigZombiePool.InitializeMonsterPool("BigZombie", BigzombiePrefab, 2, 5);  // ū ���� Ǯ �ʱ�ȭ

        impPool = new MonsterPool();
        impPool.InitializeMonsterPool("Imp", ImpPrefab, 2, 6);  // ���� Ǯ �ʱ�ȭ

        skeletonPool = new MonsterPool();
        skeletonPool.InitializeMonsterPool("Skeleton", skeletPrefab, 2, 7);  // ���̷��� Ǯ �ʱ�ȭ

        iceZombiePool = new MonsterPool();
        iceZombiePool.InitializeMonsterPool("IceZombie", iceZombiePrefab, 2, 8);  // ���̽� ���� Ǯ �ʱ�ȭ

        ogrePool = new MonsterPool();
        ogrePool.InitializeMonsterPool("Ogre", ogrePrefab, 2, 9);  // ����� Ǯ �ʱ�ȭ

        knightPool = new MonsterPool();
        knightPool.InitializeMonsterPool("Knight", knightPrefab, 2, 10);  // ����Ʈ Ǯ �ʱ�ȭ

        necromancerPool = new MonsterPool();
        necromancerPool.InitializeMonsterPool("Necromancer", necromancerPrefab, 2, 11);  // ��ũ�θǼ� Ǯ �ʱ�ȭ

        demonPool = new MonsterPool();
        demonPool.InitializeMonsterPool("Demon", demonPrefab, 5, 12);  // ���� Ǯ �ʱ�ȭ
        Debug.Log("SpawnMonster��");

        // ���÷� ��� ���͸� ����
        SpawnMonster(goblinPool, new Vector2(1f, 1f));
    }

    void SpawnMonster(MonsterPool pool, Vector2 position)
    {
        Debug.Log("SpawnMonster ȣ���");
        Debug.Log("Pool name: " + pool.name);  // pool.name Ȯ��

        GameObject monster = pool.GetMonster(pool.name);  // Ǯ���� ���� ��������

        if (monster != null)
        {
            Debug.Log("���� ������: " + monster.name);  // ���� ���� ���� ���� Ȯ��
            monster.transform.position = position;  // ������ ��ġ�� ���� ��ġ
        }
        else
        {
            Debug.LogError("���͸� Ǯ���� �������� ���߽��ϴ�.");
        }
    }
    void ReturnGoblin(GameObject goblin)
    {
        goblinPool.ReturnMonster("Goblin", goblin);  // ����� Ǯ�� ��ȯ
    }
}
