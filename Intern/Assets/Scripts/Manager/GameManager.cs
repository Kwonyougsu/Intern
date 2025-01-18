using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ProjectileObjectPool projectileObjectPool;
    public MonsterManager monsterManager;
    public int monsterKillcount;
    public int stageCount;
    private void Start()
    {
        monsterKillcount = 0;
        stageCount = 1;
    }
    protected override void Awake()
    {
        projectileObjectPool = GetComponent<ProjectileObjectPool>();
        monsterManager = GetComponent<MonsterManager>();
    }
    public void CountCheck()
    {
        if(monsterKillcount >= 5)
        {
            Time.timeScale = 0f;
            UIManager.Instance.endPanel.OpenPanel();
        }
    }
}
