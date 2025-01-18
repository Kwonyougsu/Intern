using UnityEngine;
using UnityEngine.UI;

public class EndPanel : MonoBehaviour
{
    public GameObject stageclearpanel;
    public GameObject endpanel;
    private void Start()
    {
        stageclearpanel.SetActive(false);
        endpanel.SetActive(false);
    }
    public void OpenPanel()
    {
        stageclearpanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Clickcontibtn()
    {
        GameManager.Instance.stageCount++;
        GameManager.Instance.monsterKillcount = 0;
        Time.timeScale = 1f;
        UIManager.Instance.speedUpBtn.stagecountupdate();
    }

    public void Clickendbtn()
    {
        endpanel.SetActive(true);
    }
}
