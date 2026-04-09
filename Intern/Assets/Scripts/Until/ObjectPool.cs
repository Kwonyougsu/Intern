using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private Queue<T> pool = new Queue<T>();

    private T prefab;
    private Transform parent;

    public ObjectPool(T prefab, int count, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private void CreateObject()
    {
        T obj = GameObject.Instantiate(prefab, parent);
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    public T Get()
    {
        if (pool.Count == 0)
        {
            CreateObject(); // 자동 확장
        }

        T obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(parent);
        pool.Enqueue(obj);
    }
}