using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Arrow arrowPrefab;
    public int arrowPoolSize = 10;

    private Player player;
    private float attackRadius;
    private float attackCooldown;
    private float currenttime;

    private void Start()
    {
        player = GetComponent<Player>();
        attackRadius = 14f;
        attackCooldown = 1f;
        currenttime = 0f;
        PoolManager.Instance.RegisterPool<Arrow>(arrowPrefab, arrowPoolSize, new GameObject("ArrowPool").transform);
    }

    private void Update()
    {
        currenttime += Time.deltaTime;
        AutoAttack();
    }

    private void AutoAttack()
    {
        if (currenttime < attackCooldown)
            return;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, LayerMask.GetMask("Monster"));

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Monster"))
            {
                StartCoroutine(AttackWithDelay());
                currenttime = 0f;
                break;
            }
        }
    }

    private IEnumerator AttackWithDelay()
    {
        player.animator.SetTrigger("Attack");
        yield return null;
    }

    public void FireArrow()
    {
        GameObject arrow = PoolManager.Instance.Get<Arrow>();
        if (arrow == null)
        {
            Debug.Log("화살 없음");
            return;
        }

        arrow.transform.position = transform.position;
        arrow.SetActive(true);

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            float arrowSpeed = 10f;
            rb.linearVelocity = Vector2.right * arrowSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
