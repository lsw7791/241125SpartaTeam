using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainData;
using Tripolygon.UModeler.UI.ViewModels;
using UnityEngine.UIElements;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private GameObject goblinPrefab;  // 고블린 프리팹
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
        items.Add(new ItemInstance { id = 1, itemid = 1, itemtype=(ItemType)1, itemname ="실험1" });
        }
        ItemManager.Instance.Initialize(items);


        // 몬스터 풀 초기화
        goblinPool = new MonsterPool();
        goblinPool.InitializeMonsterPool("Goblin", goblinPrefab, 2, 1);  // 고블린 풀 초기화

        lizardPool = new MonsterPool();
        lizardPool.InitializeMonsterPool("Lizard", lizardPrefab, 2, 2);  // 도마뱀 풀 초기화

        zombiePool = new MonsterPool();
        zombiePool.InitializeMonsterPool("Zombie", zombiePrefab, 2, 3);  // 좀비 풀 초기화

        orcShamanPool = new MonsterPool();
        orcShamanPool.InitializeMonsterPool("OrcShaman", orcShamanPrefab, 2, 4);  // 오크 샤먼 풀 초기화

        bigZombiePool = new MonsterPool();
        bigZombiePool.InitializeMonsterPool("BigZombie", BigzombiePrefab, 2, 5);  // 큰 좀비 풀 초기화

        impPool = new MonsterPool();
        impPool.InitializeMonsterPool("Imp", ImpPrefab, 2, 6);  // 임프 풀 초기화

        skeletonPool = new MonsterPool();
        skeletonPool.InitializeMonsterPool("Skeleton", skeletPrefab, 2, 7);  // 스켈레톤 풀 초기화

        iceZombiePool = new MonsterPool();
        iceZombiePool.InitializeMonsterPool("IceZombie", iceZombiePrefab, 2, 8);  // 아이스 좀비 풀 초기화

        ogrePool = new MonsterPool();
        ogrePool.InitializeMonsterPool("Ogre", ogrePrefab, 2, 9);  // 오우거 풀 초기화

        knightPool = new MonsterPool();
        knightPool.InitializeMonsterPool("Knight", knightPrefab, 2, 10);  // 나이트 풀 초기화

        necromancerPool = new MonsterPool();
        necromancerPool.InitializeMonsterPool("Necromancer", necromancerPrefab, 2, 11);  // 네크로맨서 풀 초기화

        demonPool = new MonsterPool();
        demonPool.InitializeMonsterPool("Demon", demonPrefab, 5, 12);  // 데몬 풀 초기화
        Debug.Log("SpawnMonster전");

        // 예시로 고블린 몬스터를 생성
        SpawnMonster(goblinPool, new Vector2(1f, 1f));
    }

    void SpawnMonster(MonsterPool pool, Vector2 position)
    {
        Debug.Log("SpawnMonster 호출됨");
        Debug.Log("Pool name: " + pool.name);  // pool.name 확인

        GameObject monster = pool.GetMonster(pool.name);  // 풀에서 몬스터 가져오기

        if (monster != null)
        {
            Debug.Log("몬스터 생성됨: " + monster.name);  // 몬스터 생성 성공 여부 확인
            monster.transform.position = position;  // 지정된 위치에 몬스터 배치
        }
        else
        {
            Debug.LogError("몬스터를 풀에서 가져오지 못했습니다.");
        }
    }
    void ReturnGoblin(GameObject goblin)
    {
        goblinPool.ReturnMonster("Goblin", goblin);  // 고블린을 풀에 반환
    }
}
