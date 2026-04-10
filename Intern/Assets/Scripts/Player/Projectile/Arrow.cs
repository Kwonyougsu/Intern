using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage = 100f;

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), 3f);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(ReturnToPool));
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
        PoolManager.Instance.Return<Arrow>(gameObject);
    }
}
