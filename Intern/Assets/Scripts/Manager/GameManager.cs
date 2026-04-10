using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int monsterKillcount { get; private set; }
    public int stageCount { get; private set; }

    private void Start()
    {
        monsterKillcount = 0;
        stageCount = 1;
    }

    public void AddKill()
    {
        monsterKillcount++;
        if (monsterKillcount >= 5)
        {
            Time.timeScale = 0f;
            UIManager.Instance.stageClearPanel.ShowPanel();
        }
    }

    public void NextStage()
    {
        stageCount++;
        monsterKillcount = 0;
        UIManager.Instance.speedUpBtn.RestoreTimeScale();
        UIManager.Instance.speedUpBtn.stagecountupdate();
        PoolManager.Instance.Clear<Monster>();
        MonsterManager.Instance.Restart();
    }

    public void OnPlayerDefeated()
    {
        Time.timeScale = 0f;
        UIManager.Instance.gameOverPanel.ShowPanel();
    }

    public void RetryGame()
    {
        stageCount = 1;
        monsterKillcount = 0;
        UIManager.Instance.speedUpBtn.RestoreTimeScale();
        UIManager.Instance.speedUpBtn.stagecountupdate();
        PoolManager.Instance.Clear<Monster>();
        MonsterManager.Instance.Restart();
    }
}
