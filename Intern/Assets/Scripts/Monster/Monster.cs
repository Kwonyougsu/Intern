using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public MonsterStateMachine monsterstateMachine;
    public MonsterManager monsterManager;
    public Animator animator;

    public string Name;
    public string Grade;
    public float Speed;
    public int MaxHealth;
    public int CurrentHealth;
    public bool Death;
    public Image HpBar;
    public event Action<GameObject> OnMonsterDeath;


    private void Awake()
    {
        monsterstateMachine = new MonsterStateMachine(this);
        animator = GetComponent<Animator>();
        monsterManager = GameManager.Instance.monsterManager;
    }

    public void Initialize(MonsterInfo info)
    {
        Name = info.Name;
        Grade = info.Grade;
        MaxHealth = info.Health;
        Speed = info.Speed;

    }

    public void OnEnable()
    {
        CurrentHealth = MaxHealth;
        Death = false;
        monsterstateMachine.ChangedState(monsterstateMachine.moveState);
    }

    private void Update()
    {
        HpBar.fillAmount = (float)CurrentHealth / (float)MaxHealth;
    }
    private void FixedUpdate()
    {
        if(!Death)
        {
            monsterstateMachine.Update();
        }
    }

    public void TakeDamage(float damage)
    {
        if (Death) return; 

        CurrentHealth -= (int)damage;

        if (CurrentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }


    public IEnumerator Die()
    {
        Death = true;
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
}
