using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    public MonsterObjectPool monsterPool;
    public ProjectileObjectPool projectilePool;

    private void Awake()
    {
        monsterPool = GetComponentInChildren<MonsterObjectPool>();
        projectilePool = GetComponentInChildren<ProjectileObjectPool>();
    }
}
