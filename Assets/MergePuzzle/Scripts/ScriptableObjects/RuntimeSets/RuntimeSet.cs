using System.Collections.Generic;
using UnityEngine;

public class RuntimeSet<T> : ScriptableObject
{
    [Header("Debug")]
    [SerializeField]
    private List<T> list = new List<T>();

    private void OnEnable()
    {
        Debug.Log("Clearing list");
        list.Clear();
    }

    public void Add(T obj)
    {
        list.Add(obj);
    }

    public void Remove(T obj)
    {
        list.Remove(obj);
    }

    public List<T> GetList() { return list; }

    public T GetRandom()
    {
        if (list.Count > 0)
        {
           return list[Random.Range(0, list.Count)];
        } else
        {
            return default;
        }
    }

}
