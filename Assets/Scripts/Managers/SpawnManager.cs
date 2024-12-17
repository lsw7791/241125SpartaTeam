using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    //public MonsterPool monsterPool;// 몬스터 풀
    public MonsterPool minePool;
    public ProjectilePool projectilePool;// 몬스터 풀

    public GameObject playerObject;
    public Spawner spawner1;
    public Spawner spawner2;
    public Spawner spawner3;


    public void Initialize(int mapNum)
    {
        if(projectilePool==null)
        {
        projectilePool = GroupSpawn("Projectile").AddComponent<ProjectilePool>();
        }
        if (spawner1 != null) Destroy(spawner1);
        if (spawner2 != null) Destroy(spawner2);
        if (spawner3 != null) Destroy(spawner3);

        SpawnProjectilePool();
        //SpawnMonsterPool();
        //if(monsterPool1==null) monsterPool1 = GroupSpawn("Monster1").AddComponent<MonsterPool>();
        //if (monsterPool2 == null) monsterPool2 = GroupSpawn("Monster2").AddComponent<MonsterPool>();
        //if (monsterPool3 == null) monsterPool3 = GroupSpawn("Monster3").AddComponent<MonsterPool>();

        SetPlayerPosition(mapNum);
                SpawnMInePool();
        Debug.Log($"맵 넘버 : {mapNum}");
        switch (mapNum)
        {
            case 1://마을
                break;
            case 2://숲
                break;
            case 3://마인1
                if (spawner1 == null) spawner1 = gameObject.AddComponent<Spawner>();
                StageMine1(spawner1);
                break;
            case 4://마인2
                if (spawner2 == null) spawner2 = gameObject.AddComponent<Spawner>();
                StageMine2(spawner2);
                break;
            case 5://마인3
                if (spawner3 == null) spawner3 = gameObject.AddComponent<Spawner>();
                StageMine3(spawner3);
                break;           
            case 24: //캐릭터 셀렉션
                playerObject.SetActive(false);
                break;
            default:
                break;
        }   
    }
    //public void SpawnMonsterPool(MonsterPool pool)
    //{
    //    pool = GroupSpawn("Monster").AddComponent<MonsterPool>();
    //    // 몬스터 풀 초기화 (각 몬스터 타입에 대해 풀을 생성)
    //    pool.InitializeMonsterPool(2, 5);  // Lizard
    //    pool.InitializeMonsterPool(3, 5);  // Zombie
    //    pool.InitializeMonsterPool(1, 5);  // Goblin
    //    pool.InitializeMonsterPool(4, 5);  // OrcShaman
    //    pool.InitializeMonsterPool(5, 5);  // BigZombie
    //    pool.InitializeMonsterPool(6, 5);  // Imp
    //    pool.InitializeMonsterPool(7, 5);  // Skelet
    //    pool.InitializeMonsterPool(8, 5);  // IceZombie
    //    pool.InitializeMonsterPool(9, 5);  // Ogre
    //    pool.InitializeMonsterPool(10, 5);  // Knight
    //    pool.InitializeMonsterPool(11, 5);  // Necromancer
    //    pool.InitializeMonsterPool(12, 5);  // Demon
    //}
    public void StageMine1(Spawner spawner)
    {
        if (spawner == null) spawner = gameObject.AddComponent<Spawner>();

        // 스폰 위치 설정
        spawner.spawnPoints = new List<Vector2>
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
        spawner.creatureIds = new List<int> { 1, 1, 1, 1,7,7,7 }; // Goblin, Lizard, Zombie, OrcShaman

        //monsterPool.GetMonster(7, new Vector2(47f, 14.5f));

        GameObject stone1 = minePool.InitializeMine(13, new Vector2(29f, 23f));
        GameObject stone2 = minePool.InitializeMine(13, new Vector2(33f, 23f));
        GameObject stone3 = minePool.InitializeMine(13, new Vector2(36.5f, 27f));
        GameObject stone4 = minePool.InitializeMine(13, new Vector2(39f, 27f));

        GameObject copper1 = minePool.InitializeMine(14, new Vector2(53f, 21.5f));
        GameObject copper2 = minePool.InitializeMine(14, new Vector2(53f, 19.5f));
    }
    public void StageMine2(Spawner spawner)
    {

        if (spawner == null) spawner = gameObject.AddComponent<Spawner>();

        // 스폰 위치 설정
        spawner.spawnPoints = new List<Vector2>
    {
        new Vector2(33f, 25f),
        new Vector2(36f, 25f),

        new Vector2(30f, 19f),
        new Vector2(33f, 19f),

        new Vector2(22f, 18f),
        new Vector2(25f, 18f),
        new Vector2(22f, 15f),
        new Vector2(25f, 15f),

    };

        // 몬스터 ID 설정 (위치와 매핑됨)
        spawner.creatureIds = new List<int> { 2, 2, 2, 2, 2, 8, 8,8 }; // Goblin, Lizard, Zombie, OrcShaman

        //monsterPool.GetMonster(7, new Vector2(47f, 14.5f));

        GameObject iron1 = minePool.InitializeMine(15, new Vector2(45f, 28f));
        GameObject gold1 = minePool.InitializeMine(16, new Vector2(48f, 28f));
        GameObject iron2 = minePool.InitializeMine(15, new Vector2(51f, 28f));


    }
    public void StageMine3(Spawner spawner)
    {
        if (spawner == null) spawner = gameObject.AddComponent<Spawner>();

        // 스폰 위치 설정
        spawner.spawnPoints = new List<Vector2>
    {
        new Vector2(50f, 19f),
        new Vector2(52f, 19f),
        new Vector2(50f, 17f),
        new Vector2(52f, 17f),

        new Vector2(35f, 14f),
        new Vector2(38f, 14f),
        new Vector2(35f, 16f),
        new Vector2(38f, 16f),

        new Vector2(33f, 23f),
        new Vector2(36f, 23f),
        new Vector2(33f, 21f),
        new Vector2(36f, 21f)
    };

        // 몬스터 ID 설정 (위치와 매핑됨)
        spawner.creatureIds = new List<int> { 3, 3, 3, 9, 3, 3, 9, 9, 3, 9, 9, 9, }; // Goblin, Lizard, Zombie, OrcShaman

        //monsterPool.GetMonster(7, new Vector2(47f, 14.5f));

        GameObject stone1 = minePool.InitializeMine(16, new Vector2(39f, 26f));
        GameObject copper1 = minePool.InitializeMine(16, new Vector2(37f, 26f));
        GameObject iron1 = minePool.InitializeMine(16, new Vector2(35f, 26f));
        GameObject platium1 = minePool.InitializeMine(16, new Vector2(33f, 26f));
        GameObject Ignis1 = minePool.InitializeMine(16, new Vector2(31f, 26f));


        GameObject platium2 = minePool.InitializeMine(17, new Vector2(36.5f, 27f));
        GameObject platium3 = minePool.InitializeMine(17, new Vector2(39f, 27f));

        GameObject Ignis2 = minePool.InitializeMine(18, new Vector2(20f, 25f));
        GameObject Ignis3 = minePool.InitializeMine(18, new Vector2(20f, 21f));
    }
    public void SpawnProjectilePool()
    {
        projectilePool.InitializeProjectilePool("Arrow", 5);
        projectilePool.InitializeProjectilePool("FireBall", 5);
        projectilePool.InitializeProjectilePool("TestEnemyRangeAttack", 5);
    }
    public void SpawnMInePool()
    {
        if (minePool != null) Destroy(minePool);
            if (minePool == null) minePool = GameManager.Instance.SpawnManager.GroupSpawn($"Mine{GameManager.Instance.SceneNum}").AddComponent<MonsterPool>();
    }
    public GameObject GroupSpawn(string inGroupName)
    {
        GameObject outPoolGroup = new GameObject();
        outPoolGroup.name = $"{inGroupName} PoolGroup";
        return outPoolGroup;
    }
    GameObject SpawnPlayer() 
    {
        playerObject = Instantiate(Resources.Load<GameObject>("Prefabs/TestPlayer_Backup"));
        GameManager.Instance.Player = playerObject.GetComponent<Player>();
        DontDestroyOnLoad(playerObject);
        playerObject.SetActive(false);

        return playerObject;
    }
    public GameObject SpawnPlayer(int mapNum)
    {
        if (GameManager.Instance.Player == null)
        {
            playerObject = Instantiate(Resources.Load<GameObject>("Prefabs/TestPlayer_Backup"));
            GameManager.Instance.Player = playerObject.GetComponent<Player>();
            playerObject.transform.position = GameManager.Instance.DataManager.Scene.GetMoveTransform(mapNum);

            // 씬 전환 시 파괴되지 않게 설정
            DontDestroyOnLoad(playerObject);
            playerObject.SetActive(false);
            return playerObject;
        }
        else
        {
            return null;
        }
    }
    public void SetPlayerPosition(int mapNum)
    {
        playerObject.SetActive(true);
        playerObject.transform.position= GameManager.Instance.DataManager.Scene.GetMoveTransform(mapNum);
    }

}
