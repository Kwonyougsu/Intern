using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterClick : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject blackScreen;
    public Image showmonsterImage;
    public TextMeshProUGUI monsterName;
    public TextMeshProUGUI monsterGrade;
    public TextMeshProUGUI monsterSpeed;
    public TextMeshProUGUI monsterHealth;

    private void Start()
    {
        blackScreen.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void ShowMonsterInfo(Sprite monsterimage,string name, string grade, float speed, int maxhealth)
    {
        Time.timeScale = 0f;
        blackScreen.SetActive(true);
        infoPanel.SetActive(true);
        showmonsterImage.sprite = monsterimage;
        monsterName.text = "이름 : " + name;
        monsterGrade.text = "등급 : " + grade;
        monsterSpeed.text = "스피드 : " + speed.ToString();
        monsterHealth.text = "체력 : " + maxhealth.ToString();
    }

    public void CloseInfoPanel()
    {
        if(UIManager.Instance.speedUpBtn.speedupbtn.activeSelf)
        {
            Time.timeScale = 1f;
        }
        else Time.timeScale = 2f;

        
        blackScreen.SetActive(false);
        infoPanel.SetActive(false);
    }

}
