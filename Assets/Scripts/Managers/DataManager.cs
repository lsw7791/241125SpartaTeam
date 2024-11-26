using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoSingleton<DataManager>
{
    List<MonsterData> _MonsterData;

    protected override void Awake()
    {
        base.Awake();
        _MonsterData = LoadMonsters();
    }

    // 몬스터 데이터 리스트 로드
    public List<MonsterData> LoadMonsters()
    {
        List<MonsterData> monsters = new List<MonsterData>();

        // Resources에서 CSV 파일 로드
        TextAsset csvFile = Resources.Load<TextAsset>("CSV/MonsterData");

        string[] lines = csvFile.text.Split('\n');
        if (lines.Length < 2) return monsters; // 최소 헤더와 1줄 이상의 데이터 필요

        // 첫 번째 줄은 헤더, 두 번째 줄부터 몬스터 데이터
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Trim().Split(',');

            MonsterData monster = new MonsterData(
                values[0], // 이름
                int.Parse(values[1]), // 체력
                float.Parse(values[2]), // 공격력
                int.Parse(values[3]) // 레벨
            );

            monsters.Add(monster);
        }

        return monsters;
    }

    // 이름으로 몬스터 데이터 찾기
    public MonsterData GetMonsterData(string name)
    {
        return _MonsterData.Find(m => m.Name == name);
    }

    // 몬스터 데이터 리스트 반환
    public List<MonsterData> GetMonsterDataList()
    {
        return _MonsterData;
    }
}