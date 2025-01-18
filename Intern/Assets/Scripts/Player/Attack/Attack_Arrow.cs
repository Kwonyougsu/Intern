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
        // ȭ���� ���������� �̵��ϵ��� �ʱ� �ӵ� ����
        rb.velocity = Vector2.right * bulletSpeed*Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // �浹�� ��ü�� "Monster" �±׸� ������ �ִ��� Ȯ��
        if (collider.CompareTag("Monster"))
        {
            // Monster�� ü�� ���� ó��
            //MonsterHealth monsterHealth = collider.GetComponent<MonsterHealth>();
            //if (monsterHealth != null)
            //{
            //    monsterHealth.ChangeHealth(-bulletDamage);
            //}

            // �浹 �� ȭ�� ����
            Destroy(gameObject);
        }
    }
}
