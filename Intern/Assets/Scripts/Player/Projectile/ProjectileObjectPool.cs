using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPool : MonoBehaviour
{
    public GameObject arrowPrefab;    // ȭ�� ������
    public int poolSize = 10;         // Ǯ ũ��

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
            arrow.transform.SetParent(ProjectileCreate);  // ȭ���� �θ� ��ü�� �߰�
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
            Debug.LogWarning("Ǯ ������ �ø���");
            return null;
        }
    }

    public void ReturnArrow(GameObject arrow)
    {
        arrow.SetActive(false);
        arrow.transform.SetParent(ProjectileCreate);  // ȭ���� �θ� ��ü�� �ٽ� ����
        arrowPool.Enqueue(arrow);
    }
}
