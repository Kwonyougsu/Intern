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
    }

    public void Clickcontibtn()
    {
        GameManager.Instance.stageCount++;
        GameManager.Instance.monsterKillcount = 0;
        if (UIManager.Instance.speedUpBtn.speedupbtn.activeSelf)
        {
            Time.timeScale = 1f;
        }
        else Time.timeScale = 2f;
        UIManager.Instance.speedUpBtn.stagecountupdate();
        stageclearpanel.SetActive(false);
        GameManager.Instance.monsterManager.objectPool.ClearPool();
        GameManager.Instance.monsterManager.Start();

    }

    public void Clickendbtn()
    {
        endpanel.SetActive(true);
    }
}
