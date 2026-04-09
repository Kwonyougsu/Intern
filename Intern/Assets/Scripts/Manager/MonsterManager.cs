using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>
{
    [Header("Monster")]
    public MonsterObjectPool objectPool; // ������Ʈ Ǯ
    public MonsterDataLoader dataLoader; // ������ �δ�
    public GameObject Spawnpoint;

    private Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();
    public bool isMonsterAlive = false; // ���Ͱ� ����ִ��� ����

    private void Awake()
    {
        objectPool = GetComponent<MonsterObjectPool>();
        dataLoader = GetComponent<MonsterDataLoader>();
    }

    public void Start()
    {
        AddAllMonstersToPool();
        SpawnMonster();
    }

    private void AddAllMonstersToPool()
    {
        // dataLoader�� ��� ���� �����͸� Ǯ�� �߰�
        foreach (MonsterInfo monsterInfo in dataLoader.monsterList)
        {
            GameObject monsterPrefab = LoadMonsterPrefab(monsterInfo);
            if (monsterPrefab != null)
            {
                InitializeObjectPool(monsterPrefab, monsterInfo);
            }
        }
    }

    private GameObject LoadMonsterPrefab(MonsterInfo monsterInfo)
    {
        if (prefabCache.TryGetValue(monsterInfo.Prefab, out GameObject cachedPrefab))
        {
            return cachedPrefab;
        }
        else
        {
            GameObject monsterPrefab = Resources.Load<GameObject>(monsterInfo.Prefab);
            if (monsterPrefab == null)
            {
                Debug.LogError($"������ �ε� ����: {monsterInfo.Prefab}");
                return null;
            }
            prefabCache[monsterInfo.Prefab] = monsterPrefab;
            return monsterPrefab;
        }
    }

    private void InitializeObjectPool(GameObject monsterPrefab, MonsterInfo monsterInfo)
    {
        GameObject monster = Instantiate(monsterPrefab);
        monster.SetActive(false);

        Monster monsterComponent = monster.GetComponent<Monster>();
        if (monsterComponent != null)
        {
            monsterComponent.Initialize(monsterInfo);
            monsterComponent.OnMonsterDeath += MonsterDeath;
        }

        objectPool.AddToPool(monster); // Ǯ�� �߰�
    }

    private void MonsterDeath(GameObject deadMonster)
    {
        objectPool.ReturnMonster(deadMonster);
        SpawnMonster();
    }

    public void SpawnMonster()
    {
        if (GameManager.Instance.monsterKillcount < 5)
        {
            // Ǯ���� ���͸� ������ Ȱ��ȭ
            GameObject spawnedMonster = objectPool.PoolGetMonster();

            if (spawnedMonster != null)
            {
                // ���� ��ġ ����
                spawnedMonster.transform.position = GetSpawnPosition();

                // ���� Ȱ��ȭ
                spawnedMonster.SetActive(true);
                isMonsterAlive = true;
            }
            else
            {
                Debug.LogWarning("Ǯ�� �� �̻� ���Ͱ� �����ϴ�.");
            }
        }
    }

    private Vector3 GetSpawnPosition()
    {
        return Spawnpoint.transform.position;
    }
}
