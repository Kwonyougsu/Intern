using UnityEngine;

public class GameOverPanel : UIPanel
{
    public void ClickRetryBtn()
    {
        ClosePanel();
        GameManager.Instance.RetryGame();
    }

    public void ClickEndGameBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
