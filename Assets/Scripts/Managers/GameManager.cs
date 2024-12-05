using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoSingleton<GameManager>
{
    public MonsterPool monsterPool;// ���� Ǯ
    //public ProjectilePool projectilePool; // �߻�ü Ǯ
    protected override void Awake()
    {
        base.Awake();
        DataManager.Instance.Initialize();

        List<ItemInstance> items = new List<ItemInstance>(); // JSON���Ͽ� ����� ���� ������ ������ �о �Ѱ��ش�.
       
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

        // ���� Ǯ �ʱ�ȭ (�� ���� Ÿ�Կ� ���� Ǯ�� ����)
        monsterPool.InitializeMonsterPool(1, 5);  // Goblin
        monsterPool.InitializeMonsterPool(2, 5);  // Lizard
        monsterPool.InitializeMonsterPool(3, 5);  // Zombie
        monsterPool.InitializeMonsterPool(4, 5);  // OrcShaman
        monsterPool.InitializeMonsterPool(5, 5);  // BigZombie
        monsterPool.InitializeMonsterPool(6, 5);  // Imp
        monsterPool.InitializeMonsterPool(7, 5);  // Skelet
        monsterPool.InitializeMonsterPool(8, 5);  // IceZombie
        monsterPool.InitializeMonsterPool(9, 5);  // Ogre
        monsterPool.InitializeMonsterPool(10, 5);  // Knight
        monsterPool.InitializeMonsterPool(11, 5);  // Necromancer
        monsterPool.InitializeMonsterPool(12, 5);  // Demon

        SpawnMonster(1, new Vector2(1f, 1f));
        SpawnMonster(4, new Vector2(2f, 1f));
        SpawnMonster(5, new Vector2(3f, 1f));
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