using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ProjectileObjectPool projectileObjectPool;
    public MonsterManager monsterManager;
    protected override void Awake()
    {
        projectileObjectPool = GetComponent<ProjectileObjectPool>();
        monsterManager = GetComponent<MonsterManager>();
    }

}
