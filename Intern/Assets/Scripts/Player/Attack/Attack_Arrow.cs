using System.Collections;
using UnityEngine;

public class Attack_Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private float bulletSpeed = 20f;
    private float bulletDamage = 100f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // 화살이 오른쪽으로 이동하도록 초기 속도 설정
        rb.velocity = Vector2.right * bulletSpeed*Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // 충돌한 객체가 "Monster" 태그를 가지고 있는지 확인
        if (collider.CompareTag("Monster"))
        {
            // Monster의 체력 감소 처리
            //MonsterHealth monsterHealth = collider.GetComponent<MonsterHealth>();
            //if (monsterHealth != null)
            //{
            //    monsterHealth.ChangeHealth(-bulletDamage);
            //}

            // 충돌 후 화살 제거
            Destroy(gameObject);
        }
    }
}
