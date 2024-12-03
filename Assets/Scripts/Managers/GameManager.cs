using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] public GameObject goblinPrefab;  // ��� ������
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

    public MonsterPool monsterPool;

    protected override void Awake()
    {
        base.Awake();
        // ������ �ε�
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

        // ���� Ǯ �ʱ�ȭ (�� ���� Ÿ�Կ� ���� Ǯ�� ����)
        monsterPool.InitializeMonsterPool(1, goblinPrefab, 5);  // Goblin
        monsterPool.InitializeMonsterPool(2, zombiePrefab, 5);  // Zombie
        monsterPool.InitializeMonsterPool(3, ImpPrefab, 5);  // Imp
        monsterPool.InitializeMonsterPool(4, lizardPrefab, 5);  // Lizard
        monsterPool.InitializeMonsterPool(5, orcShamanPrefab, 5);  // OrcShaman
        monsterPool.InitializeMonsterPool(6, BigzombiePrefab, 5);  // BigZombie
        monsterPool.InitializeMonsterPool(7, skeletPrefab, 5);  // Skelet
        monsterPool.InitializeMonsterPool(8, iceZombiePrefab, 5);  // IceZombie
        monsterPool.InitializeMonsterPool(9, ogrePrefab, 5);  // Ogre
        monsterPool.InitializeMonsterPool(10, knightPrefab, 5);  // Knight
        monsterPool.InitializeMonsterPool(11, necromancerPrefab, 5);  // Necromancer
        monsterPool.InitializeMonsterPool(12, demonPrefab, 5);  // Demon

        SpawnMonster(1, new Vector2(1f, 1f));
    }

    void SpawnMonster(int creatureId, Vector2 position)
    {
        // ���͸� Ǯ���� �����ɴϴ�
        GameObject monster = monsterPool.GetMonster(creatureId, position);
        if (monster != null)
        {
            // ���Ͱ� ���������� ������ ��� �߰� ó�� ����
        }
    }
}