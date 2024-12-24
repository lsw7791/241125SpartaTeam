using System;
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
        if (projectilePool == null)
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
            case 2://마을
                SoundManager.Instance.PlayStartBGMAsiaTravel();
                break;
            case 3://마인1
                SoundManager.Instance.PlayStartBGMSciFiMoodtimeflow();
                if (spawner1 == null) spawner1 = gameObject.AddComponent<Spawner>();
                StageMine(1,spawner1);
                break;
            case 4:
                SoundManager.Instance.PlayStartBGMAsiaTravel();
                break;
            case 5://마인2
                if (spawner2 == null) spawner2 = gameObject.AddComponent<Spawner>();
                StageMine(2,spawner2);
                break;
            case 6://마인1
                if (spawner1 == null) spawner1 = gameObject.AddComponent<Spawner>();
                StageMine(1,spawner1);
                break;
            case 7://마인3
                if (spawner3 == null) spawner3 = gameObject.AddComponent<Spawner>();
                StageMine(3,spawner3);
                break;
            case 8://마인2
                if (spawner2 == null) spawner2 = gameObject.AddComponent<Spawner>();
                StageMine(2,spawner2);
                break;
            case 23://타이틀>하우스
                SoundManager.Instance.PlayStartBGMAsiaTravel();        
                break;
            case 24: //캐릭터 셀렉션
                playerObject.SetActive(false);
                break;
            case 25: //캐릭터 셀렉션
                SoundManager.Instance.PlayStartBGMMystical();
                playerObject.SetActive(false);
                break;
            default:
                break;
        }
    }
    //public void StageMine1(Spawner spawner)
    //{
    //    if (spawner == null) spawner = gameObject.AddComponent<Spawner>();
    //    // 스폰 위치 설정
    //    spawner.spawnPoints = new List<Vector2>
    //{
    //    new Vector2(24.5f, 13.5f),
    //    new Vector2(26f, 13.5f),
    //    new Vector2(25f, 12.5f),
    //    new Vector2(26.5f, 12.5f),
    //    new Vector2(45f, 12.5f),
    //    new Vector2(46f, 13.5f),
    //    new Vector2(47f, 14.5f)
    //};
    //    // 몬스터 ID 설정 (위치와 매핑됨)
    //    spawner.creatureIds = new List<int> { 1, 1, 1, 1, 7, 7, 7 }; // Goblin, Lizard, Zombie, OrcShaman
    //    //monsterPool.GetMonster(7, new Vector2(47f, 14.5f));
    //    GameObject stone1 = minePool.InitializeMine(13, new Vector2(29f, 23f));
    //    GameObject stone2 = minePool.InitializeMine(13, new Vector2(33f, 23f));
    //    GameObject stone3 = minePool.InitializeMine(13, new Vector2(36.5f, 27f));
    //    GameObject stone4 = minePool.InitializeMine(13, new Vector2(39f, 27f));
    //    GameObject copper1 = minePool.InitializeMine(14, new Vector2(53f, 21.5f));
    //    GameObject copper2 = minePool.InitializeMine(14, new Vector2(53f, 19.5f));
    //}
    public void StageMine(int stageMineNum,Spawner spawner)
    {
        spawner.ActiveMonsterCount = GameManager.Instance.DataManager.MonsterSpawn.GetMonsterID(stageMineNum).Count;
        if (spawner == null) spawner = gameObject.AddComponent<Spawner>();
        // 스폰 위치 설정
        spawner.spawnPoints = CreateVector2ListFromString(GameManager.Instance.DataManager.MonsterSpawn.GetMonsterPosition(stageMineNum));
        // 몬스터 ID 설정 (위치와 매핑됨)
        spawner.creatureIds = GameManager.Instance.DataManager.MonsterSpawn.GetMonsterID(stageMineNum);
        //monsterPool.GetMonster(7, new Vector2(47f, 14.5f));
        List<Vector2> minePos = CreateVector2ListFromString(GameManager.Instance.DataManager.MineSpawn.GetMInePosition(stageMineNum));
        List<int> mineID = GameManager.Instance.DataManager.MineSpawn.GetMIneID(stageMineNum);
        if (minePos.Count != mineID.Count)
        {
            Debug.LogError("minePos와 mineID의 크기가 일치하지 않습니다. 스폰을 중단합니다.");
            return;
        }
        for (int i = 0; i < minePos.Count; i++)
        {
            // 위치와 몬스터 ID를 기반으로 체크 및 스폰
            if (i < mineID.Count) // 몬스터 ID가 위치 개수와 맞아야 함
            {
                minePool.InitializeMine(mineID[i], minePos[i]);
            }
        }
    }
    //public void StageMine2(Spawner spawner)
    //{
    //    if (spawner == null) spawner = gameObject.AddComponent<Spawner>();
    //    // 스폰 위치 설정
    //    spawner.spawnPoints = new List<Vector2>
    //{
    //    new Vector2(33f, 25f),
    //    new Vector2(36f, 25f),
    //    new Vector2(30f, 19f),
    //    new Vector2(33f, 19f),
    //    new Vector2(22f, 18f),
    //    new Vector2(25f, 18f),
    //    new Vector2(22f, 15f),
    //    new Vector2(25f, 15f),
    //};
    //    // 몬스터 ID 설정 (위치와 매핑됨)
    //    spawner.creatureIds = new List<int> { 2, 2, 2, 2, 2, 8, 8, 8 }; // Goblin, Lizard, Zombie, OrcShaman
    //    //monsterPool.GetMonster(7, new Vector2(47f, 14.5f));
    //    GameObject iron1 = minePool.InitializeMine(15, new Vector2(45f, 28f));
    //    GameObject gold1 = minePool.InitializeMine(16, new Vector2(48f, 28f));
    //    GameObject iron2 = minePool.InitializeMine(15, new Vector2(51f, 28f));
    //}
    //public void StageMine3(Spawner spawner)
    //{
    //    if (spawner == null) spawner = gameObject.AddComponent<Spawner>();
    //    // 스폰 위치 설정
    //    spawner.spawnPoints = new List<Vector2>
    //{
    //    new Vector2(50f, 19f),
    //    new Vector2(52f, 19f),
    //    new Vector2(50f, 17f),
    //    new Vector2(52f, 17f),
    //    new Vector2(35f, 14f),
    //    new Vector2(38f, 14f),
    //    new Vector2(35f, 16f),
    //    new Vector2(38f, 16f),
    //    new Vector2(33f, 23f),
    //    new Vector2(36f, 23f),
    //    new Vector2(33f, 21f),
    //    new Vector2(36f, 21f)
    //};
    //    // 몬스터 ID 설정 (위치와 매핑됨)
    //    spawner.creatureIds = new List<int> { 3, 3, 3, 9, 3, 3, 9, 9, 3, 9, 9, 9 }; // Goblin, Lizard, Zombie, OrcShaman
    //    //monsterPool.GetMonster(7, new Vector2(47f, 14.5f));
    //    GameObject stone1 = minePool.InitializeMine(16, new Vector2(39f, 26f));
    //    GameObject copper1 = minePool.InitializeMine(16, new Vector2(37f, 26f));
    //    GameObject iron1 = minePool.InitializeMine(16, new Vector2(35f, 26f));
    //    GameObject platium1 = minePool.InitializeMine(16, new Vector2(33f, 26f));
    //    GameObject Ignis1 = minePool.InitializeMine(16, new Vector2(31f, 26f));
    //    GameObject platium2 = minePool.InitializeMine(17, new Vector2(36.5f, 27f));
    //    GameObject platium3 = minePool.InitializeMine(17, new Vector2(39f, 27f));
    //    GameObject Ignis2 = minePool.InitializeMine(18, new Vector2(20f, 25f));
    //    GameObject Ignis3 = minePool.InitializeMine(18, new Vector2(20f, 21f));
    //}
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
        playerObject.transform.position = GameManager.Instance.DataManager.Scene.GetMoveTransform(mapNum);
    }

    // 쉼표로 구분된 string 값을 List<Vector2>로 변환하는 메서드
    List<Vector2> CreateVector2ListFromString(string input)
    {
        List<Vector2> vectorList = new List<Vector2>();

        // 문자열을 쉼표로 나누기
        string[] values = input.Split(',');

        // 두 개씩 묶어 Vector2로 변환
        for (int i = 0; i < values.Length; i += 2)
        {
            if (i + 1 < values.Length)
            {
                // 공백 제거 후 float으로 변환
                float x = float.Parse(values[i].Trim());
                float y = float.Parse(values[i + 1].Trim());

                // Vector2 생성 후 리스트에 추가
                vectorList.Add(new Vector2(x, y));
            }
        }

        return vectorList;
    }
}