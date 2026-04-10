using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            GameManager.Instance.OnPlayerDefeated();
        }
    }
}
