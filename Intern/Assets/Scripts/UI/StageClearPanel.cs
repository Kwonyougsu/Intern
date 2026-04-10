using UnityEngine;

public class StageClearPanel : UIPanel
{
    public void Clickcontibtn()
    {
        ClosePanel();
        GameManager.Instance.NextStage();
    }

    public void Clickendbtn()
    {
        UIManager.Instance.gameOverPanel.ShowPanel();
    }
}
