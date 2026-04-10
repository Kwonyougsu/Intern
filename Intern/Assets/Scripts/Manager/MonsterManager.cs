using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    public MonsterSpawner spawner;

    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        spawner.Initialize();
        spawner.SpawnMonster();
    }

    public void OnMonsterDied()
    {
        spawner.isMonsterAlive = false;
    }
}
