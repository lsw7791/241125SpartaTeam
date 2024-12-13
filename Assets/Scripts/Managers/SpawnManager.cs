using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public MonsterPool monsterPool;// 몬스터 풀
    public ProjectilePool projectilePool;// 몬스터 풀

    public GameObject playerObject;
    public Spawner spawner1;
    public Spawner spawner2;
    public Spawner spawner3;


    public void Initialize(int mapNum)
    {
        projectilePool = GroupSpawn("Projectile").AddComponent<ProjectilePool>();
        SpawnProjectilePool();
        SpawnMonsterPool();
        switch (mapNum)
        {
            case 1://마을
                StageVillage();
                break;
            case 2://숲
                StageForest();
                break;
            case 3://마인1
                if (spawner1 == null) spawner1 = gameObject.AddComponent<Spawner>();
                StageMine1();
                break;
            case 4://마인2
                if (spawner2 == null) spawner1 = gameObject.AddComponent<Spawner>();
                StageMine2();
                break;
            case 5://마인3
                if (spawner3 == null) spawner1 = gameObject.AddComponent<Spawner>();
                StageMine3();
                break;
            default:
                break;
        }
    }
    public void SpawnMonsterPool()
    {
        monsterPool = GroupSpawn("Monster").AddComponent<MonsterPool>();
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
    public void StageVillage()
    {

    }
    public void StageForest()
    {

    }
    public void StageMine1()
    {
        if (spawner1 == null) spawner1 = gameObject.AddComponent<Spawner>();

        spawner1.monsterPool = monsterPool; // 몬스터 풀 연결

        // 스폰 위치 설정
        spawner1.spawnPoints = new List<Vector2>
    {
        new Vector2(24.5f, 13.5f),
        new Vector2(26f, 13.5f),
        new Vector2(25f, 12.5f),
        new Vector2(26.5f, 12.5f),
        new Vector2(45f, 12.5f),
        new Vector2(46f, 13.5f),
        new Vector2(47f, 14.5f)
    };

        // 몬스터 ID 설정 (위치와 매핑됨)
        spawner1.creatureIds = new List<int> { 1, 1, 1, 1,7,7,7 }; // Goblin, Lizard, Zombie, OrcShaman

        //monsterPool.GetMonster(7, new Vector2(47f, 14.5f));

        GameObject stone1 = monsterPool.InitializeMine(13, new Vector2(29f, 23f));
        GameObject stone2 = monsterPool.InitializeMine(13, new Vector2(33f, 23f));
        GameObject stone3 = monsterPool.InitializeMine(13, new Vector2(36.5f, 27f));
        GameObject stone4 = monsterPool.InitializeMine(13, new Vector2(39f, 27f));

        GameObject copper1 = monsterPool.InitializeMine(14, new Vector2(53f, 21.5f));
        GameObject copper2 = monsterPool.InitializeMine(14, new Vector2(53f, 19.5f));
    }
    public void StageMine2()
    {
       
    }
    public void StageMine3()
    {
       
    }
    public void SpawnProjectilePool()
    {
        projectilePool.InitializeProjectilePool("Arrow", 5);
        projectilePool.InitializeProjectilePool("FireBall", 5);
        projectilePool.InitializeProjectilePool("TestEnemyRangeAttack", 5);
    }

    GameObject GroupSpawn(string inGroupName)
    {
        GameObject outPoolGroup = new GameObject();
        outPoolGroup.name = $"{inGroupName} PoolGroup";
        return outPoolGroup;
    }
    GameObject SpawnPlayer() 
    {
        playerObject = Instantiate(Resources.Load<GameObject>("Prefabs/TestPlayer_Backup"));
        GameManager.Instance.player = playerObject.GetComponent<Player>();
        return playerObject;
    }
    public GameObject SpawnPlayer(int mapNum)
    {
        playerObject = Instantiate(Resources.Load<GameObject>("Prefabs/TestPlayer_Backup"));
        GameManager.Instance.player = playerObject.GetComponent<Player>();
        playerObject.transform.position = GameManager.Instance.dataManager.scene.GetMoveTransform(mapNum);
        return playerObject;
    }
    
}
