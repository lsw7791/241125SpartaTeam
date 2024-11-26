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

    // ���� ������ ����Ʈ �ε�
    public List<MonsterData> LoadMonsters()
    {
        List<MonsterData> monsters = new List<MonsterData>();

        // Resources���� CSV ���� �ε�
        TextAsset csvFile = Resources.Load<TextAsset>("CSV/MonsterData");

        string[] lines = csvFile.text.Split('\n');
        if (lines.Length < 2) return monsters; // �ּ� ����� 1�� �̻��� ������ �ʿ�

        // ù ��° ���� ���, �� ��° �ٺ��� ���� ������
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Trim().Split(',');

            MonsterData monster = new MonsterData(
                values[0], // �̸�
                int.Parse(values[1]), // ü��
                float.Parse(values[2]), // ���ݷ�
                int.Parse(values[3]) // ����
            );

            monsters.Add(monster);
        }

        return monsters;
    }

    // �̸����� ���� ������ ã��
    public MonsterData GetMonsterData(string name)
    {
        return _MonsterData.Find(m => m.Name == name);
    }

    // ���� ������ ����Ʈ ��ȯ
    public List<MonsterData> GetMonsterDataList()
    {
        return _MonsterData;
    }
}