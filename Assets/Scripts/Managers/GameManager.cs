using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] public GameObject goblinPrefab;  // 몬스터 프리팹
    [SerializeField] public GameObject lizardPrefab;
    [SerializeField] public GameObject zombiePrefab;
    [SerializeField] public GameObject orcShamanPrefab;
    [SerializeField] public GameObject BigzombiePrefab;
    [SerializeField] public GameObject ImpPrefab;
    [SerializeField] public GameObject skeletPrefab;
    [SerializeField] public GameObject iceZombiePrefab;
    [SerializeField] public GameObject ogrePrefab;
    [SerializeField] public GameObject knightPrefab;
    [SerializeField] public GameObject necromancerPrefab;
    [SerializeField] public GameObject demonPrefab;

    [SerializeField] public GameObject stoneMine; //광석 프리팹
    [SerializeField] public GameObject copperMine;
    [SerializeField] public GameObject ironMine;
    [SerializeField] public GameObject goldMine;
    [SerializeField] public GameObject platinumMine;
    [SerializeField] public GameObject ignisMine;


    public MonsterPool monsterPool;// 몬스터 풀

    protected override void Awake()
    {
        base.Awake();
        // 몬스터 프리팹 로드
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
        
        // 광석 프리팹 로드
        stoneMine = Resources.Load<GameObject>("Prefabs/Mines/StoneMine");
        copperMine = Resources.Load<GameObject>("Prefabs/Mines/CopperMine");
        ironMine = Resources.Load<GameObject>("Prefabs/Mines/IronMine");
        goldMine = Resources.Load<GameObject>("Prefabs/Mines/GoldMine");
        platinumMine = Resources.Load<GameObject>("Prefabs/Mines/PlatinumMine");
        ignisMine = Resources.Load<GameObject>("Prefabs/Mines/IgnisMine");


        DataManager.Instance.Initialize();

        List<ItemInstance> items = new List<ItemInstance>(); // JSON파일에 저장된 유저 아이템 정보를 읽어서 넘겨준다.
        {
            new ItemInstance { id = 1, itemid = 1, count = 1, enhance = 0 };
            new ItemInstance { id = 2, itemid = 1, count = 1, enhance = 3 };
            new ItemInstance { id = 3, itemid = 2, count = 1, enhance = 0 };
            new ItemInstance { id = 4, itemid = 3, count = 1, enhance = 0 };

        }
        ItemManager.Instance.Initialize(items);

    }

    private void Start()
    {
        GameObject objectPoolGroup = new GameObject();
        objectPoolGroup.name = "objectPoolGroup";
        objectPoolGroup.AddComponent<MonsterPool>();

        if (!objectPoolGroup.TryGetComponent(out monsterPool))
        {
            objectPoolGroup.AddComponent<MonsterPool>();
        }

        //monsterPool = new MonsterPool();

        // 몬스터 풀 초기화 (각 몬스터 타입에 대해 풀을 생성)
        monsterPool.InitializeMonsterPool(1, goblinPrefab, 5);  // Goblin
        monsterPool.InitializeMonsterPool(2, lizardPrefab, 5);  // Lizard
        monsterPool.InitializeMonsterPool(3, zombiePrefab, 5);  // Zombie
        monsterPool.InitializeMonsterPool(4, orcShamanPrefab, 5);  // OrcShaman
        monsterPool.InitializeMonsterPool(5, BigzombiePrefab, 5);  // BigZombie
        monsterPool.InitializeMonsterPool(6, ImpPrefab, 5);  // Imp
        monsterPool.InitializeMonsterPool(7, skeletPrefab, 5);  // Skelet
        monsterPool.InitializeMonsterPool(8, iceZombiePrefab, 5);  // IceZombie
        monsterPool.InitializeMonsterPool(9, ogrePrefab, 5);  // Ogre
        monsterPool.InitializeMonsterPool(10, knightPrefab, 5);  // Knight
        monsterPool.InitializeMonsterPool(11, necromancerPrefab, 5);  // Necromancer
        monsterPool.InitializeMonsterPool(12, demonPrefab, 5);  // Demon

        monsterPool.InitializeMonsterPool(13, stoneMine, 5);  // stoneMine
        monsterPool.InitializeMonsterPool(14, copperMine, 5);  // stoneMine
        monsterPool.InitializeMonsterPool(15, ironMine, 5);  // stoneMine
        monsterPool.InitializeMonsterPool(16, goldMine, 5);  // stoneMine
        monsterPool.InitializeMonsterPool(17, platinumMine, 5);  // stoneMine
        monsterPool.InitializeMonsterPool(18, ignisMine, 5);  // stoneMine


        SpawnMonster(1, new Vector2(1f, 1f));
        

    }

    void SpawnMonster(int creatureId, Vector2 position)
    {
        // 몬스터를 풀에서 가져옵니다
        GameObject monster = monsterPool.GetMonster(creatureId, position);
        if (monster != null)
        {
            // 몬스터가 성공적으로 생성된 경우 추가 처리 가능
        }
    }
}