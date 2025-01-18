using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [Header("Monster")]
    public MonsterObjectPool objectPool; // ������Ʈ Ǯ
    public MonsterDataLoader dataLoader; // ������ �δ�
    public GameObject Spawnpoint;

    // ������ ĳ�ø� ���� ��ųʸ�
    private Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();

    private void Awake()
    {
        objectPool = GetComponent<MonsterObjectPool>();
        dataLoader = GetComponent<MonsterDataLoader>();
    }

    private void Start()
    {
        // CSV �����͸� ������� ���� Ǯ�� �߰�
        AddAllMonstersToPool();

        // ���ϴ� ������ ���͸� �ٷ� ��ȯ
        SpawnMonster(2); 
    }

    private void AddAllMonstersToPool()
    {
        // dataLoader�� ��� ���� �����͸� Ǯ�� �߰�
        foreach (MonsterInfo monsterInfo in dataLoader.monsterList)
        {
            // ������ ��������
            GameObject monsterPrefab = LoadMonsterPrefab(monsterInfo);
            if (monsterPrefab != null)
            {
                // Ǯ�� ���� �߰�
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
            monsterComponent.Initialize(monsterInfo); // ���� �����ͷ� �ʱ�ȭ
        }

        objectPool.AddToPool(monster); // Ǯ�� �߰�
    }

    public void SpawnMonster(int count)
    {
        int spawnedCount = 0;

        // Ǯ���� ���͸� ������ Ȱ��ȭ
        while (spawnedCount < count)
        {
            GameObject spawnedMonster = objectPool.PoolGetMonster();

            if (spawnedMonster != null)
            {
                // ���� ��ġ ����
                spawnedMonster.transform.position = GetSpawnPosition();

                // ���� Ȱ��ȭ
                spawnedMonster.SetActive(true);
                spawnedCount++;
            }
            else
            {
                Debug.LogWarning("Ǯ�� �� �̻� ���Ͱ� �����ϴ�.");
                break;
            }
        }
    }

    private Vector3 GetSpawnPosition()
    {
        return Spawnpoint.transform.position;
    }
}
