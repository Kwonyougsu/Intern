using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterClick : UIPanel
{
    public GameObject blackScreen;
    public Image showmonsterImage;
    public TextMeshProUGUI monsterName;
    public TextMeshProUGUI monsterGrade;
    public TextMeshProUGUI monsterSpeed;
    public TextMeshProUGUI monsterHealth;

    private Sprite pendingSprite;
    private string pendingName;
    private string pendingGrade;
    private float pendingSpeed;
    private int pendingHealth;

    public override void ShowPanel()
    {
        blackScreen.SetActive(true);
        base.ShowPanel();
    }

    public override void ClosePanel()
    {
        blackScreen.SetActive(false);
        base.ClosePanel();
    }

    public override void Setup()
    {
        base.Setup();
        showmonsterImage.sprite = pendingSprite;
        monsterName.text = "이름 : " + pendingName;
        monsterGrade.text = "등급 : " + pendingGrade;
        monsterSpeed.text = "속도 : " + pendingSpeed.ToString();
        monsterHealth.text = "체력 : " + pendingHealth.ToString();
    }

    public void ShowMonsterInfo(Sprite sprite, string name, string grade, float speed, int maxhealth)
    {
        pendingSprite = sprite;
        pendingName = name;
        pendingGrade = grade;
        pendingSpeed = speed;
        pendingHealth = maxhealth;
        isChangedData = true;
        Time.timeScale = 0f;
        ShowPanel();
    }

    public void CloseInfoPanel()
    {
        UIManager.Instance.speedUpBtn.RestoreTimeScale();
        ClosePanel();
    }
}
