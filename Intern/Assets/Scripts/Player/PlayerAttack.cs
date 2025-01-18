using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Player player;
    float attackRadius;
    float attackCooldown;    // 공격 쿨타임
    float currenttime;        // 쿨타임을 위한 변수

    public ProjectileObjectPool ProjectileObjectPool;
    
    //호출순서 문제로 start
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
        currenttime += Time.deltaTime; // 쿨타임 카운팅
        AutoAttack();
    }

    private void AutoAttack()
    {
        // 쿨타임 체크
        if (currenttime < attackCooldown)
            return;

        // 공격 범위 내 적 탐지
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, LayerMask.GetMask("Monster"));

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Monster")) // 적 태그 확인
            {
                StartCoroutine(AttackWithDelay(hitCollider.transform.position));
                currenttime = 0f;  // 쿨타임 초기화
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
                // 화살 위치와 방향 설정
                arrow.transform.position = transform.position;
                Vector2 direction = Vector2.right;

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
            else
            {
                Debug.Log("화살 없음");
            }
        }
        else
        {
            Debug.Log("오브젝트 풀 없음");
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
