using System.Collections.Generic;
using UnityEngine;

public class MonsterObjectPool : MonoBehaviour
{
    public GameObject Prefab { get; private set; }
    public bool IsInitialized => Prefab != null;

    public Queue<GameObject> pool = new Queue<GameObject>();

    private int initialCount; // �⺻ Ǯ ũ��

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

    // ������Ʈ Ǯ�� �ʱ�ȭ
    public void InitializePool(GameObject prefab, int count)
    {
        Prefab = prefab;
        initialCount = count;
        
        // �ʱ� ũ�⸸ŭ Ǯ�� ä���.
        for (int i = 0; i < initialCount; i++)
        {
            GameObject monster = Instantiate(prefab);
            monster.SetActive(false);
            monster.transform.SetParent(MonsterCreatePool);
            pool.Enqueue(monster);
        }
    }

    // ���͸� Ǯ���� ������, ������ ���� ���� 
    public GameObject PoolGetMonster()
    {
        if (pool.Count == 0)
            return null;

        return pool.Dequeue();
    }

    // Ǯ�� �ʱ�ȭ�ϰ� ��� ���͸� ����
    public void ClearPool()
    {
        foreach (var monster in pool)
        {
            Destroy(monster);
        }
        pool.Clear();
        Prefab = null;
    }
    // ����� ���� ���͸� Ǯ�� ��ȯ
    public void ReturnMonster(GameObject monster)
    {
        monster.SetActive(false);
        monster.transform.SetParent(MonsterCreatePool);
        pool.Enqueue(monster);
    }

}

