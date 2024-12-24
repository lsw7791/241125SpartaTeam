using System;
using System.Collections;
using System.Collections.Generic;
using UGS;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnManager : MonoBehaviour
{
    //public MonsterPool monsterPool;// ���� Ǯ
    public MonsterPool minePool;
    public ProjectilePool projectilePool;// ���� Ǯ
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
        Debug.Log($"�� �ѹ� : {mapNum}");
        switch (mapNum)
        {
            case 2://����
                SoundManager.Instance.PlayStartBGMAsiaTravel();
                break;
            case 3://����1
                SoundManager.Instance.PlayStartBGMSciFiMoodtimeflow();
                if (spawner1 == null) spawner1 = gameObject.AddComponent<Spawner>();
                StageMine(1,spawner1);
                break;
            case 4:
                SoundManager.Instance.PlayStartBGMAsiaTravel();
                break;
            case 5://����2
                if (spawner2 == null) spawner2 = gameObject.AddComponent<Spawner>();
                StageMine(2,spawner2);
                break;
            case 6://����1
                if (spawner1 == null) spawner1 = gameObject.AddComponent<Spawner>();
                StageMine(1,spawner1);
                break;
            case 7://����3
                if (spawner3 == null) spawner3 = gameObject.AddComponent<Spawner>();
                StageMine(3,spawner3);
                break;
            case 8://����2
                if (spawner2 == null) spawner2 = gameObject.AddComponent<Spawner>();
                StageMine(2,spawner2);
                break;
            case 23://Ÿ��Ʋ>�Ͽ콺
                SoundManager.Instance.PlayStartBGMAsiaTravel();        
                break;
            case 24: //ĳ���� ������
                playerObject.SetActive(false);
                break;
            case 25: //ĳ���� ������
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
    //    // ���� ��ġ ����
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
    //    // ���� ID ���� (��ġ�� ���ε�)
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
        // ���� ��ġ ����
        spawner.spawnPoints = CreateVector2ListFromString(GameManager.Instance.DataManager.MonsterSpawn.GetMonsterPosition(stageMineNum));
        // ���� ID ���� (��ġ�� ���ε�)
        spawner.creatureIds = GameManager.Instance.DataManager.MonsterSpawn.GetMonsterID(stageMineNum);
        //monsterPool.GetMonster(7, new Vector2(47f, 14.5f));
        List<Vector2> minePos = CreateVector2ListFromString(GameManager.Instance.DataManager.MineSpawn.GetMInePosition(stageMineNum));
        List<int> mineID = GameManager.Instance.DataManager.MineSpawn.GetMIneID(stageMineNum);
        if (minePos.Count != mineID.Count)
        {
            Debug.LogError("minePos�� mineID�� ũ�Ⱑ ��ġ���� �ʽ��ϴ�. ������ �ߴ��մϴ�.");
            return;
        }
        for (int i = 0; i < minePos.Count; i++)
        {
            // ��ġ�� ���� ID�� ������� üũ �� ����
            if (i < mineID.Count) // ���� ID�� ��ġ ������ �¾ƾ� ��
            {
                minePool.InitializeMine(mineID[i], minePos[i]);
            }
        }
    }
    //public void StageMine2(Spawner spawner)
    //{
    //    if (spawner == null) spawner = gameObject.AddComponent<Spawner>();
    //    // ���� ��ġ ����
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
    //    // ���� ID ���� (��ġ�� ���ε�)
    //    spawner.creatureIds = new List<int> { 2, 2, 2, 2, 2, 8, 8, 8 }; // Goblin, Lizard, Zombie, OrcShaman
    //    //monsterPool.GetMonster(7, new Vector2(47f, 14.5f));
    //    GameObject iron1 = minePool.InitializeMine(15, new Vector2(45f, 28f));
    //    GameObject gold1 = minePool.InitializeMine(16, new Vector2(48f, 28f));
    //    GameObject iron2 = minePool.InitializeMine(15, new Vector2(51f, 28f));
    //}
    //public void StageMine3(Spawner spawner)
    //{
    //    if (spawner == null) spawner = gameObject.AddComponent<Spawner>();
    //    // ���� ��ġ ����
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
    //    // ���� ID ���� (��ġ�� ���ε�)
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
            // �� ��ȯ �� �ı����� �ʰ� ����
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

    // ��ǥ�� ���е� string ���� List<Vector2>�� ��ȯ�ϴ� �޼���
    List<Vector2> CreateVector2ListFromString(string input)
    {
        List<Vector2> vectorList = new List<Vector2>();

        // ���ڿ��� ��ǥ�� ������
        string[] values = input.Split(',');

        // �� ���� ���� Vector2�� ��ȯ
        for (int i = 0; i < values.Length; i += 2)
        {
            if (i + 1 < values.Length)
            {
                // ���� ���� �� float���� ��ȯ
                float x = float.Parse(values[i].Trim());
                float y = float.Parse(values[i + 1].Trim());

                // Vector2 ���� �� ����Ʈ�� �߰�
                vectorList.Add(new Vector2(x, y));
            }
        }

        return vectorList;
    }
}