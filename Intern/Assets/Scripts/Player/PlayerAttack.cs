using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Player player;
    float attackRadius;
    float attackCooldown;    // ���� ��Ÿ��
    float currenttime;        // ��Ÿ���� ���� ����

    public ProjectileObjectPool ProjectileObjectPool;
    
    //ȣ����� ������ start
    private void Start()
    {
        player = GetComponent<Player>();
        ProjectileObjectPool = GameManager.Instance.projectileObjectPool;
        attackRadius = 14f;
        attackCooldown = 1f;
        currenttime = 0f;
    }

    void Update()
    {
        currenttime += Time.deltaTime; // ��Ÿ�� ī����
        AutoAttack();
    }

    private void AutoAttack()
    {
        // ��Ÿ�� üũ
        if (currenttime < attackCooldown)
            return;

        // ���� ���� �� �� Ž��
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, LayerMask.GetMask("Monster"));

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Monster")) // �� �±� Ȯ��
            {
                StartCoroutine(AttackWithDelay(hitCollider.transform.position));
                currenttime = 0f;  // ��Ÿ�� �ʱ�ȭ
                break;
            }
        }
    }

    private IEnumerator AttackWithDelay(Vector2 targetPosition)
    {
        player.animator.SetTrigger("Attack");
        yield return null;
    }

    public void FireArrow()
    {
        if (ProjectileObjectPool != null)
        {
            GameObject arrow = ProjectileObjectPool.PoolGetArrow();

            if (arrow != null)
            {
                // ȭ�� ��ġ�� ���� ����
                arrow.transform.position = transform.position;
                Vector2 direction = Vector2.right;

                Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    float arrowSpeed = 10f; // ȭ�� �ӵ�
                    rb.linearVelocity = direction * arrowSpeed;
                }

                Arrow arrowComponent = arrow.GetComponent<Arrow>();
                if (arrowComponent != null)
                {
                    arrowComponent.SetOwner(this);
                }
            }
            else
            {
                Debug.Log("ȭ�� ����");
            }
        }
        else
        {
            Debug.Log("������Ʈ Ǯ ����");
        }
    }

    public void ReturnArrowToPool(GameObject arrow)
    {
        ProjectileObjectPool.ReturnArrow(arrow);
    }

    private void OnDrawGizmosSelected()
    {
        // ���� ���� �ð�ȭ
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
