using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<Type, object> queues = new Dictionary<Type, object>();
    private Dictionary<Type, Component> prefabs = new Dictionary<Type, Component>();
    private Dictionary<Type, Transform> parents = new Dictionary<Type, Transform>();

    public void RegisterPool<T>(Transform parent) where T : Component
    {
        if (queues.ContainsKey(typeof(T))) return;
        queues[typeof(T)] = new Queue<T>();
        parents[typeof(T)] = parent;
    }

    public void RegisterPool<T>(T prefab, int count, Transform parent) where T : Component
    {
        if (queues.ContainsKey(typeof(T))) return;
        Queue<T> queue = new Queue<T>();
        queues[typeof(T)] = queue;
        parents[typeof(T)] = parent;
        prefabs[typeof(T)] = prefab;

        for (int i = 0; i < count; i++)
        {
            T obj = Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            queue.Enqueue(obj);
        }
    }

    public void Add<T>(T obj) where T : Component
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(parents[typeof(T)]);
        ((Queue<T>)queues[typeof(T)]).Enqueue(obj);
    }

    public GameObject Get<T>() where T : Component
    {
        Queue<T> queue = (Queue<T>)queues[typeof(T)];
        if (queue.Count == 0)
        {
            if (!prefabs.TryGetValue(typeof(T), out Component prefab))
                return null;

            T obj = Instantiate(prefab as T, parents[typeof(T)]);
            obj.gameObject.SetActive(false);
            return obj.gameObject;
        }
        return queue.Dequeue().gameObject;
    }

    public void Return<T>(GameObject obj) where T : Component
    {
        T component = obj.GetComponent<T>();
        if (component == null) return;
        component.gameObject.SetActive(false);
        component.transform.SetParent(parents[typeof(T)]);
        ((Queue<T>)queues[typeof(T)]).Enqueue(component);
    }

    public void Clear<T>() where T : Component
    {
        Queue<T> queue = (Queue<T>)queues[typeof(T)];
        foreach (T obj in queue)
            Destroy(obj.gameObject);
        queue.Clear();
    }
}
