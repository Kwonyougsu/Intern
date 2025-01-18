using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage = 200f;
    private PlayerAttack player;

    public void SetOwner(PlayerAttack playerAttack)
    {
        player = playerAttack; // PlayerAttack 참조 저장
    }

    private void Start()
    {
        Invoke("ReturnToPool", 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(damage);

                ReturnToPool();
            }
        }
    }

    private void ReturnToPool()
    {
        if (player != null)
        {
            player.ReturnArrowToPool(gameObject); 
        }
    }
}
