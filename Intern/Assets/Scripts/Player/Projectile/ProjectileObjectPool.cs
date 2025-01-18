using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPool : MonoBehaviour
{
    public GameObject arrowPrefab;    // 화살 프리팹
    public int poolSize = 10;         // 풀 크기

    private Queue<GameObject> arrowPool = new Queue<GameObject>();

    private Transform ProjectileCreate;

    private void Awake()
    {
        GameObject parentObject = new GameObject("Arrow");
        ProjectileCreate = parentObject.transform;
        parentObject.SetActive(true);

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.SetActive(false);
            arrow.transform.SetParent(ProjectileCreate);  // 화살을 부모 객체에 추가
            arrowPool.Enqueue(arrow);
        }
    }

    public GameObject PoolGetArrow()
    {
        if (arrowPool.Count > 0)
        {
            GameObject arrow = arrowPool.Dequeue();
            arrow.SetActive(true);
            return arrow;
        }
        else
        {
            Debug.LogWarning("풀 사이즈 늘리기");
            return null;
        }
    }

    public void ReturnArrow(GameObject arrow)
    {
        arrow.SetActive(false);
        arrow.transform.SetParent(ProjectileCreate);  // 화살을 부모 객체에 다시 설정
        arrowPool.Enqueue(arrow);
    }
}
