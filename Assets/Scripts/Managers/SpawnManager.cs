using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;

public class SpawnManager : MonoSingleton<SpawnManager>
{
    public MonsterPool monsterPool;// 몬스터 풀
    public void Initialize()
    {
        GameObject objectPoolGroup = new GameObject();
        objectPoolGroup.name = "objectPoolGroup";
        objectPoolGroup.AddComponent<MonsterPool>();

        if (!objectPoolGroup.TryGetComponent(out monsterPool))
        {
            objectPoolGroup.AddComponent<MonsterPool>();
        }
    }
    private void Start()
    {
        SpawnPool();
        monsterPool.GetMonster(1, new Vector2(1f, 1f));
        monsterPool.GetMonster(4, new Vector2(2f, 1f));
        monsterPool.GetMonster(5, new Vector2(3f, 1f));
    }
    public void SpawnPool()
    {
        // 몬스터 풀 초기화 (각 몬스터 타입에 대해 풀을 생성)
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
    }   
}
