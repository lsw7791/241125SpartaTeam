using System;
using System.Collections.Generic;

public class StringArrayParser
{
    // 구글 시트에서 가져온 값을 int[][] 형식으로 변환하는 메서드
    public string ConvertToFormattedString(int[][] map)
    {
        var result = new List<string>();

        foreach (var row in map)
        {
            // 각 행을 "{ 1, 1, 1, ... }" 형태로 변환
            string formattedRow = "{ " + string.Join(", ", row) + " }";
            result.Add(formattedRow);
        }

        // 전체 배열을 한 번에 문자열로 반환 (각 행은 줄바꿈으로 구분)
        return string.Join(",\n", result);
    }

    // 구글 시트 데이터를 받아서 int[][]로 변환하는 메서드
    public int[][] ParseMap(string sheetData)
    {
        // 데이터를 행 단위로 나누기
        var rows = sheetData.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        var map = new List<int[]>();

        foreach (var row in rows)
        {
            // 각 행을 쉼표로 구분하여 숫자 배열로 변환
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

        return map.ToArray(); // List<int[]>를 int[][] 배열로 변환하여 반환
    }
}
