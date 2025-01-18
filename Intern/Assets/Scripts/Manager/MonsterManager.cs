using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [Header("Monster")]
    public MonsterObjectPool objectPool; // 오브젝트 풀
    public MonsterDataLoader dataLoader; // 데이터 로더
    public GameObject Spawnpoint;

    // 프리팹 캐시를 위한 딕셔너리
    private Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();

    private void Awake()
    {
        objectPool = GetComponent<MonsterObjectPool>();
        dataLoader = GetComponent<MonsterDataLoader>();
    }

    private void Start()
    {
        // CSV 데이터를 기반으로 몬스터 풀에 추가
        AddAllMonstersToPool();

        // 원하는 개수의 몬스터를 바로 소환
        SpawnMonster(2); 
    }

    private void AddAllMonstersToPool()
    {
        // dataLoader의 모든 몬스터 데이터를 풀에 추가
        foreach (MonsterInfo monsterInfo in dataLoader.monsterList)
        {
            // 프리팹 가져오기
            GameObject monsterPrefab = LoadMonsterPrefab(monsterInfo);
            if (monsterPrefab != null)
            {
                // 풀에 몬스터 추가
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
            monsterComponent.Initialize(monsterInfo); // 몬스터 데이터로 초기화
        }

        objectPool.AddToPool(monster); // 풀에 추가
    }

    public void SpawnMonster(int count)
    {
        int spawnedCount = 0;

        // 풀에서 몬스터를 꺼내서 활성화
        while (spawnedCount < count)
        {
            GameObject spawnedMonster = objectPool.PoolGetMonster();

            if (spawnedMonster != null)
            {
                // 스폰 위치 설정
                spawnedMonster.transform.position = GetSpawnPosition();

                // 몬스터 활성화
                spawnedMonster.SetActive(true);
                spawnedCount++;
            }
            else
            {
                Debug.LogWarning("풀에 더 이상 몬스터가 없습니다.");
                break;
            }
        }
    }

    private Vector3 GetSpawnPosition()
    {
        return Spawnpoint.transform.position;
    }
}
