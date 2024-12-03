using UnityEngine;

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

    private MonsterPool monsterPool;

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

        monsterPool = new MonsterPool();

        // 몬스터 풀 초기화 (여기서 풀을 생성합니다)
        monsterPool.InitializeMonsterPool("Goblin", goblinPrefab, 5, 1);
        monsterPool.InitializeMonsterPool("Lizard", lizardPrefab, 5, 2);
        monsterPool.InitializeMonsterPool("Zombie", zombiePrefab, 5, 3);
        monsterPool.InitializeMonsterPool("OrcShaman", orcShamanPrefab, 5, 4);
        monsterPool.InitializeMonsterPool("BigZombie", BigzombiePrefab, 5, 5);
        monsterPool.InitializeMonsterPool("Imp", ImpPrefab, 5, 6);
        monsterPool.InitializeMonsterPool("Skelet", skeletPrefab, 5, 7);
        monsterPool.InitializeMonsterPool("IceZombie", iceZombiePrefab, 5, 8);
        monsterPool.InitializeMonsterPool("Ogre", ogrePrefab, 5, 9);
        monsterPool.InitializeMonsterPool("Knight", knightPrefab, 5, 10);
        monsterPool.InitializeMonsterPool("Necromancer", necromancerPrefab, 5, 11);
        monsterPool.InitializeMonsterPool("Demon", demonPrefab, 5, 12);



        // 예시로 고블린 몬스터를 생성하고, 위치를 설정합니다
        SpawnMonster("Goblin", new Vector2(1f, 1f));
        SpawnMonster("Zombie", new Vector2(3f, 3f));
    }

    void SpawnMonster(string monsterType, Vector2 position)
    {

        // 몬스터를 풀에서 가져옵니다
        GameObject monster = monsterPool.GetMonster(monsterType, position);
        if (monster != null)
        {

        }
    }
}