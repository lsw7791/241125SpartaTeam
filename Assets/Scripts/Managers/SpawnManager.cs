using System.Collections.Generic;
using UnityEngine;
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
        switch (mapNum)
        {
            case 2://마을
                SoundManager.Instance.PlayStartBGMCalmPiano();
                break;
            case 3://마인1
                SoundManager.Instance.PlayStartBGMShootingStar();
                if (spawner1 == null) spawner1 = gameObject.AddComponent<Spawner>();
                StageMine(1,spawner1);
                break;
            case 4:
                SoundManager.Instance.PlayStartBGMCalmPiano();
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
                SoundManager.Instance.PlayStartBGMCalmPiano();        
                break;
            case 24: //캐릭터 셀렉션
                playerObject.SetActive(false);
                break;
            case 25: //캐릭터 셀렉션
                SoundManager.Instance.PlayStartBGMRelaxingPiano();
                playerObject.SetActive(false);
                break;
            default:
                break;
        }
    }

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