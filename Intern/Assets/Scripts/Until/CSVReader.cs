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

        // CSV�� ù ��: ��� ����
        string headerLine = reader.ReadLine();
        string[] headers = headerLine.Split(',');

        // ������ ���� �б�
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
