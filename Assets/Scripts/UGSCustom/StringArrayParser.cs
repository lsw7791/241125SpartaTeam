using System;
using System.Collections.Generic;

public class StringArrayParser
{
    // ���� ��Ʈ���� ������ ���� int[][] �������� ��ȯ�ϴ� �޼���
    public string ConvertToFormattedString(int[][] map)
    {
        var result = new List<string>();

        foreach (var row in map)
        {
            // �� ���� "{ 1, 1, 1, ... }" ���·� ��ȯ
            string formattedRow = "{ " + string.Join(", ", row) + " }";
            result.Add(formattedRow);
        }

        // ��ü �迭�� �� ���� ���ڿ��� ��ȯ (�� ���� �ٹٲ����� ����)
        return string.Join(",\n", result);
    }

    // ���� ��Ʈ �����͸� �޾Ƽ� int[][]�� ��ȯ�ϴ� �޼���
    public int[][] ParseMap(string sheetData)
    {
        // �����͸� �� ������ ������
        var rows = sheetData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        var map = new List<int[]>();

        foreach (var row in rows)
        {
            // �� ���� ��ǥ�� �����Ͽ� ���� �迭�� ��ȯ
            var values = row.Split(',');

            var intArray = new int[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                if (int.TryParse(values[i].Trim(), out int value))
                {
                    intArray[i] = value;
                }
            }

            map.Add(intArray);
        }

        return map.ToArray(); // List<int[]>�� int[][] �迭�� ��ȯ�Ͽ� ��ȯ
    }
}
