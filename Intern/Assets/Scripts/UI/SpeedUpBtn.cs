using System;
using TMPro;
using UnityEngine;

public class SpeedUpBtn : MonoBehaviour
{
    private bool isSpeedUp = false;
    public GameObject speedupbtn;
    public GameObject normalupbtn;
    public TextMeshProUGUI stagecount;

    private void Start()
    {
        stagecountupdate();
    }
    public void ToggleSpeed()
    {
        if (isSpeedUp)
        {
            Time.timeScale = 1f;  
            speedupbtn.SetActive(true);
            normalupbtn.SetActive(false);
        }
        else
        {
            Time.timeScale = 2f;
            speedupbtn.SetActive(false);
            normalupbtn.SetActive(true);
        }

        isSpeedUp = !isSpeedUp;
    }
    public void stagecountupdate()
    {
        stagecount.text = "Stage: "+GameManager.Instance.stageCount.ToString();
    }
}
