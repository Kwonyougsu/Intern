using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRadius = 5f;      
    public float attackCooldown = 1f;    // ���� ��Ÿ��

    private float lastAttackTime;

    public ProjectileObjectPool ProjectileObjectPool; 

    private void Awake()
    {
        ProjectileObjectPool = GameManager.Instance.projectileObjectPool;
    }
    void Update()
    {
        AutoAttack();
    }

    private void AutoAttack()
    {
        // ���� ��Ÿ�� üũ
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        // ���� ���� �� �� Ž��
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Monster")) // �� �±� Ȯ��
            {
                // ȭ�� �߻�
                FireArrow(hitCollider.transform.position);

                // ���� �� ��Ÿ�� ����
                lastAttackTime = Time.time;
                break; 
            }
        }
    }

    private void FireArrow(Vector2 targetPosition)
    {
        GameObject arrow = ProjectileObjectPool.GetArrow();
        if (arrow != null)
        {
            // ȭ�� ��ġ�� ���� ����
            arrow.transform.position = transform.position;
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float arrowSpeed = 10f; // ȭ�� �ӵ�
                rb.velocity = direction * arrowSpeed;
            }

            Arrow arrowComponent = arrow.GetComponent<Arrow>();
            if (arrowComponent != null)
            {
                arrowComponent.SetOwner(this);
            }
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
