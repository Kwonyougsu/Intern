using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage = 100f;
    private PlayerAttack player; 
    public void SetOwner(PlayerAttack playerAttack)
    {
        player = playerAttack; // PlayerAttack 참조 저장
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            Monster monster = collision.GetComponent<Monster>();
            if (monster != null)
            {
                monster.TakeDamage(damage);

                if (player != null)
                {
                    player.ReturnArrowToPool(gameObject);
                }
            }
        }
    }
}
