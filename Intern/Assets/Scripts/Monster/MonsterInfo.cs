[System.Serializable]
public class MonsterInfo
{
    public string Name;
    public string Grade;
    public float Speed;
    public int Health;
    public string Prefab;

    public MonsterInfo(string name, string grade,float speed, int health, string prefab)
    {
        Name = name;
        Grade = grade;
        Speed = speed;
        Health = health;
        Prefab = prefab;
    }
}
