using System.Collections.Generic;
using UnityEngine;

public class MonsterDataLoader : MonoBehaviour
{
    public List<MonsterInfo> monsterList = new List<MonsterInfo>();

    private void Awake()
    {
        LoadMonsterData("MonsterData"); // CSV ���� �̸�
    }

    private void LoadMonsterData(string fileName)
    {
        var csvData = CSVReader.Read(fileName); // CSV �б�

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
                Debug.Log($"CSV �дµ� ����");
            }
        }
    }

    public MonsterInfo GetMonster(int index)
    {
        if (index >= 0 && index < monsterList.Count)
        {
            return monsterList[index];
        }
        return null;
    }
}
