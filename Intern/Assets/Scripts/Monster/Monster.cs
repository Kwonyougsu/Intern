using System;
using System.Collections;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Monster : MonoBehaviour, IPointerClickHandler
{
    public MonsterStateMachine monsterstateMachine;
    public MonsterManager monsterManager;
    public Animator animator;

    public SpriteRenderer monsterImage;
    public string monsterName;
    public string grade;
    public float speed;
    public int maxHealth;
    public int currentHealth;
    public bool death;
    public Image hpBar;
    public event Action<GameObject> OnMonsterDeath;
    public Sprite sendimage;

    private void Awake()
    {
        monsterstateMachine = new MonsterStateMachine(this);
        animator = GetComponent<Animator>();
        monsterManager = GameManager.Instance.monsterManager;
        monsterImage = GetComponent<SpriteRenderer>();
    }

    public void Initialize(MonsterInfo info)
    {
        monsterName = info.Name;
        grade = info.Grade;
        maxHealth = info.Health;
        speed = info.Speed;
    }

    public void OnEnable()
    {
        currentHealth = maxHealth;
        death = false;
        monsterstateMachine.ChangedState(monsterstateMachine.moveState);
    }

    private void Update()
    {
        hpBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    private void FixedUpdate()
    {
        if (!death)
        {
            monsterstateMachine.Update();
        }
    }

    public void TakeDamage(float damage)
    {
        if (death) return;

        currentHealth -= (int)damage;

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        death = true;
        ResetAllBools(animator);
        animator.SetBool("Die", true);
        yield return new WaitForSecondsRealtime(0.5f);
        gameObject.SetActive(false);
        OnMonsterDeath?.Invoke(this.gameObject);
        monsterManager.isMonsterAlive = false;
        yield return null;
    }

    public void ResetAllBools(Animator animator)
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(parameter.name, false);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        sendimage = monsterImage.sprite;
        monsterName = monsterName.Replace("(clone)", "").Trim();
        // 클릭 시 몬스터 정보 표시
        UIManager.Instance.monsterClick.ShowMonsterInfo(sendimage, monsterName, grade, speed, maxHealth);
    }
}
