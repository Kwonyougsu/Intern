using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterStateMachine monsterstateMachine;
    public Animator animator;
    CapsuleCollider2D capsuleCollider;
    public string Name;
    public string Grade;
    public float Speed;
    public int Health;

    private void Awake()
    {
        monsterstateMachine = new MonsterStateMachine(this);
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    public void Initialize(MonsterInfo info)
    {
        Name = info.Name;
        Grade = info.Grade;
        Health = info.Health;
        Speed = info.Speed;
    }

    public void OnEnable()
    {
        monsterstateMachine.ChangedState(monsterstateMachine.moveState);
    }
    private void FixedUpdate()
    {
        monsterstateMachine.Update();
    }

    public void TakeDamage(float damage)
    {
        Health -= (int)damage;

        if (Health <= 0)
        {
            StartCoroutine(Die());
            Die();
        }
    }

    public IEnumerator Die()
    {
        Debug.Log("ав╬З╢ы");
        ResetAllBools(animator);
        yield return new WaitForSecondsRealtime(0.5f);
        animator.SetBool("Die", true);
        gameObject.SetActive(false);
        //monsterManager.objectPool.ReturnMonster(this.gameObject);
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
