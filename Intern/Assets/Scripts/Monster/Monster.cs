using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Monster : MonoBehaviour, IPointerClickHandler
{
    public MonsterStateMachine monsterstateMachine;
    public Animator animator;
    public Image hpBar;
    public event Action<GameObject> OnMonsterDeath;

    public string monsterName;
    public string grade;
    public float speed;
    public int maxHealth;
    public int currentHealth;
    public bool death;

    private SpriteRenderer monsterImage;
    private int baseHealth;
    private float baseSpeed;

    private void Awake()
    {
        monsterstateMachine = new MonsterStateMachine(this);
        animator = GetComponent<Animator>();
        monsterImage = GetComponent<SpriteRenderer>();
    }

    public void Initialize(MonsterInfo info)
    {
        monsterName = info.Name;
        grade = info.Grade;
        baseHealth = info.Health;
        baseSpeed = info.Speed;
    }

    public void OnEnable()
    {
        int stage = GameManager.Instance.stageCount;
        maxHealth = stage >= 2 ? baseHealth * stage : baseHealth;
        speed = stage >= 2 ? baseSpeed * stage : baseSpeed;
        currentHealth = maxHealth;
        death = false;
        monsterstateMachine.ChangeState(MonsterState.Move);
    }

    private void Update()
    {
        hpBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    private void FixedUpdate()
    {
        if (!death)
            monsterstateMachine.Update();
    }

    public void TakeDamage(float damage)
    {
        if (death) return;
        currentHealth -= (int)damage;
        if (currentHealth <= 0)
            StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        death = true;
        animator.SetBool("Move", false);
        animator.SetBool("Die", true);
        yield return new WaitForSecondsRealtime(0.3f);
        gameObject.SetActive(false);
        MonsterManager.Instance.OnMonsterDied();
        GameManager.Instance.AddKill();
        OnMonsterDeath?.Invoke(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        string displayName = monsterName.Replace("(clone)", "").Trim();
        UIManager.Instance.monsterClick.ShowMonsterInfo(monsterImage.sprite, displayName, grade, speed, maxHealth);
    }
}
