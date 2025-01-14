using System.Collections.Generic;
using UnityEngine;
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
        switch (mapNum)
        {
            case 2://����
                SoundManager.Instance.PlayStartBGMCalmPiano();
                break;
            case 3://����1
                SoundManager.Instance.PlayStartBGMShootingStar();
                if (spawner1 == null) spawner1 = gameObject.AddComponent<Spawner>();
                StageMine(1,spawner1);
                break;
            case 4:
                SoundManager.Instance.PlayStartBGMCalmPiano();
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
                SoundManager.Instance.PlayStartBGMCalmPiano();        
                break;
            case 24: //ĳ���� ������
                playerObject.SetActive(false);
                break;
            case 25: //ĳ���� ������
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