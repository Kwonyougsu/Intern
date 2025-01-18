using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator.SetBool("Walk", true);
    }

    void Update()
    {
        
    }
}
