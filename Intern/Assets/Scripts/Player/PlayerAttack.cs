using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRadius = 5f;      
    public float attackCooldown = 1f;    // 공격 쿨타임

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
        // 공격 쿨타임 체크
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        // 공격 범위 내 적 탐지
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Monster")) // 적 태그 확인
            {
                // 화살 발사
                FireArrow(hitCollider.transform.position);

                // 공격 후 쿨타임 갱신
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
            // 화살 위치와 방향 설정
            arrow.transform.position = transform.position;
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float arrowSpeed = 10f; // 화살 속도
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
        // 공격 범위 시각화
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
