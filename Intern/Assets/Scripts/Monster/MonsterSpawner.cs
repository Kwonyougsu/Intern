using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject Spawnpoint;
    public bool isMonsterAlive = false;

    private void Awake()
    {
        PoolManager.Instance.RegisterPool<Monster>(new GameObject("MonsterPool").transform);
    }

    public void Initialize()
    {
        List<MonsterInfo> monsterList = MonsterDataLoader.Load("MonsterData");
        foreach (MonsterInfo monsterInfo in monsterList)
        {
            GameObject prefab = Resources.Load<GameObject>(monsterInfo.Prefab);
            if (prefab == null)
            {
                Debug.LogError($"프리팹 로드 실패: {monsterInfo.Prefab}");
                continue;
            }

            GameObject monster = Instantiate(prefab);
            monster.SetActive(false);

            Monster monsterComponent = monster.GetComponent<Monster>();
            if (monsterComponent != null)
            {
                monsterComponent.Initialize(monsterInfo);
                monsterComponent.OnMonsterDeath += MonsterDeath;
            }

            PoolManager.Instance.Add<Monster>(monsterComponent);
        }
    }

    private void MonsterDeath(GameObject deadMonster)
    {
        PoolManager.Instance.Return<Monster>(deadMonster);
        SpawnMonster();
    }

    public void SpawnMonster()
    {
        if (GameManager.Instance.monsterKillcount < 5)
        {
            GameObject spawnedMonster = PoolManager.Instance.Get<Monster>();

            if (spawnedMonster != null)
            {
                spawnedMonster.transform.position = Spawnpoint.transform.position;
                spawnedMonster.SetActive(true);
                isMonsterAlive = true;
            }
            else
            {
                Debug.LogWarning("풀에 더 이상 몬스터가 없습니다.");
            }
        }
    }
}
