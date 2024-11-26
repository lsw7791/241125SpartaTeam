using System.Collections.Generic;
using UnityEngine;

public class MonsterLoader
{
    public static List<MonsterData> LoadMonsters()
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
                values[0],
                int.Parse(values[1]),
                float.Parse(values[2]),
                int.Parse(values[3])
            );

            monsters.Add(monster);
        }

        return monsters;
    }
}
