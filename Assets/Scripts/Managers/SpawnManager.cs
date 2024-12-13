using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public MonsterPool monsterPool;// 몬스터 풀
    public ProjectilePool projectilePool;// 몬스터 풀

    public GameObject playerObject;
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
                StageMine1();
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
        monsterPool.GetMonster(1, new Vector2(1f, 1f));
        monsterPool.GetMonster(4, new Vector2(1f, 2f));
        monsterPool.GetMonster(5, new Vector2(1f, 3f));
        Mine123();
    }
    public void StageMine2()
    {
        monsterPool.GetMonster(1, new Vector2(1f, 1f));
        monsterPool.GetMonster(4, new Vector2(1f, 2f));
        monsterPool.GetMonster(5, new Vector2(1f, 3f));
        Mine123();
    }
    public void StageMine3()
    {
        monsterPool.GetMonster(1, new Vector2(1f, 1f));
        monsterPool.GetMonster(4, new Vector2(1f, 2f));
        monsterPool.GetMonster(5, new Vector2(1f, 3f));
        Mine123();
    }
    public void Mine123()
    {
        GameObject mine1 = monsterPool.InitializeMine(13, new Vector2(2f, 2f));
        GameObject mine2 = monsterPool.InitializeMine(14, new Vector2(2f, 1f));
        GameObject mine3 = monsterPool.InitializeMine(15, new Vector2(2f, 0f));
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
