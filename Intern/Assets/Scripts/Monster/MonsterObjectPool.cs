using System.Collections.Generic;
using UnityEngine;

public class MonsterObjectPool : MonoBehaviour
{
    public GameObject Prefab { get; private set; }
    public bool IsInitialized => Prefab != null;

    public Queue<GameObject> pool = new Queue<GameObject>();

    private int initialCount; // 기본 풀 크기

    private Transform MonsterCreatePool;

    private void Awake()
    {
        // 풀을 담아놓을 오브젝트 생성 
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

    // 오브젝트 풀을 초기화
    public void InitializePool(GameObject prefab, int count)
    {
        Prefab = prefab;
        initialCount = count;
        
        // 초기 크기만큼 풀을 채운다.
        for (int i = 0; i < initialCount; i++)
        {
            GameObject monster = Instantiate(prefab);
            monster.SetActive(false);
            monster.transform.SetParent(MonsterCreatePool);
            pool.Enqueue(monster);
        }
    }

    // 몬스터를 풀에서 꺼낸다, 없으면 동작 안함 
    public GameObject PoolGetMonster()
    {
        if (pool.Count == 0)
            return null;

        return pool.Dequeue();
    }

    // 풀을 초기화하고 모든 몬스터를 제거
    public void ClearPool()
    {
        foreach (var monster in pool)
        {
            Destroy(monster);
        }
        pool.Clear();
        Prefab = null;
    }
    // 사용이 끝난 몬스터를 풀에 반환
    public void ReturnMonster(GameObject monster)
    {
        monster.SetActive(false);
        monster.transform.SetParent(MonsterCreatePool);
        pool.Enqueue(monster);
    }

}

