using System.Collections.Generic;
using UnityEngine;

public class MonsterLoader
{
    public static List<MonsterData> LoadMonsters()
    {
        List<MonsterData> monsters = new List<MonsterData>();

        TextAsset csvFile = Resources.Load<TextAsset>("SCV/MonsterData");
        string[] lines = csvFile.text.Split('\n');
        if (lines.Length < 2) return monsters; // 최소 헤더와 1줄 이상의 데이터 필요

        for (int i = 1; i < lines.Length; i++) // 첫 번째 줄은 헤더
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Trim().Split(',');

            MonsterData monster = new MonsterData
            {
                Name = values[0],
                Health = int.Parse(values[1]),
                Speed = float.Parse(values[2]),
                Damage = int.Parse(values[3])
            };

            monsters.Add(monster);
        }

        return monsters;
    }
}
