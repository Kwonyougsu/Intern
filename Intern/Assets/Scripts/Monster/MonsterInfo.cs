using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

[System.Serializable]
public class MonsterInfo
{
    public string Name;
    public string Grade;
    public int Speed;
    public int Health;

    public MonsterInfo(string name, string grade,int speed, int health)
    {
        Name = name;
        Grade = grade;
        Speed = speed;
        Health = health;
    }
}
