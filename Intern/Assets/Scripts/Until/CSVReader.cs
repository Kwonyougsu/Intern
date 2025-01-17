using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader
{
    public static List<Dictionary<string, string>> Read(string filePath)
    {
        var data = new List<Dictionary<string, string>>();
        TextAsset csvFile = Resources.Load<TextAsset>(filePath);
        StringReader reader = new StringReader(csvFile.text);

        // CSV의 첫 줄: 헤더 정보
        string headerLine = reader.ReadLine();
        string[] headers = headerLine.Split(',');

        // 데이터 라인 읽기
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            var entry = new Dictionary<string, string>();

            for (int i = 0; i < headers.Length; i++)
            {
                entry[headers[i]] = values[i];
            }
            data.Add(entry);
        }

        return data;
    }
}
