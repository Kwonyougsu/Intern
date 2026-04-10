using System.Collections.Generic;
using UnityEngine;

public static class MonsterDataLoader
{
    public static List<MonsterInfo> Load(string fileName)
    {
        var monsterList = new List<MonsterInfo>();
        var csvData = CSVReader.Read(fileName);

        foreach (var row in csvData)
        {
            try
            {
                MonsterInfo monster = new MonsterInfo(
                    row["Name"],
                    row["Grade"],
                    float.Parse(row["Speed"]),
                    int.Parse(row["Health"]),
                    row["Prefab"]
                );
                monsterList.Add(monster);
            }
            catch
            {
                Debug.Log("CSV 읽는데 실패");
            }
        }

        return monsterList;
    }
}
