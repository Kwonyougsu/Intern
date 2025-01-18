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
        monsterName.text = "�̸� : " + name;
        monsterGrade.text = "��� : " + grade;
        monsterSpeed.text = "���ǵ� : " + speed.ToString();
        monsterHealth.text = "ü�� : " + maxhealth.ToString();
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
