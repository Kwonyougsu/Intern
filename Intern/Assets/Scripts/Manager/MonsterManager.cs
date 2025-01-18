using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [Header("Monster")]
    public MonsterObjectPool objectPool; // 오브젝트 풀
    public MonsterDataLoader dataLoader; // 데이터 로더
    public GameObject Spawnpoint;

    private Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();
    public bool isMonsterAlive = false; // 몬스터가 살아있는지 여부

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
        // dataLoader의 모든 몬스터 데이터를 풀에 추가
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
                Debug.LogError($"프리팹 로드 실패: {monsterInfo.Prefab}");
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

        objectPool.AddToPool(monster); // 풀에 추가
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
            // 풀에서 몬스터를 꺼내서 활성화
            GameObject spawnedMonster = objectPool.PoolGetMonster();

            if (spawnedMonster != null)
            {
                // 스폰 위치 설정
                spawnedMonster.transform.position = GetSpawnPosition();

                // 몬스터 활성화
                spawnedMonster.SetActive(true);
                isMonsterAlive = true;
            }
            else
            {
                Debug.LogWarning("풀에 더 이상 몬스터가 없습니다.");
            }
        }
    }

    private Vector3 GetSpawnPosition()
    {
        return Spawnpoint.transform.position;
    }


}
