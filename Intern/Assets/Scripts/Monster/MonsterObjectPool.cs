using System.Collections.Generic;
using UnityEngine;

public class MonsterObjectPool : MonoBehaviour
{
    public Queue<GameObject> pool = new Queue<GameObject>();
    private Transform MonsterCreatePool;

    private void Awake()
    {
        // Ǯ�� ��Ƴ��� ������Ʈ ���� 
        GameObject parentObject = new GameObject("MonsterPool");
        MonsterCreatePool = parentObject.transform;
        parentObject.SetActive(true);
    }
    public void AddToPool(GameObject monster)
    {
        if (monster == null) return;

        monster.SetActive(false);
        monster.transform.SetParent(MonsterCreatePool);
        pool.Enqueue(monster);
    }

    // ���͸� Ǯ���� ������, ������ ���� ���� 
    public GameObject PoolGetMonster()
    {
        if (pool.Count == 0)
            return null;

        return pool.Dequeue();
    }

    // ����� ���� ���͸� Ǯ�� ��ȯ
    public void ReturnMonster(GameObject monster)
    {
        monster.SetActive(false);
        monster.transform.SetParent(MonsterCreatePool);
        Monster monsterComponent = monster.GetComponent<Monster>();
        if (monsterComponent != null)
        {
            monsterComponent.currentHealth = monsterComponent.maxHealth; 
            monsterComponent.death = false; 
        }
        pool.Enqueue(monster);
    }

    // Ǯ�� �ʱ�ȭ�ϰ� ��� ���͸� ����
    //public void ClearPool()
    //{
    //    foreach (var monster in pool)
    //    {
    //        Destroy(monster);
    //    }
    //    pool.Clear();
    //    
    //}

}

