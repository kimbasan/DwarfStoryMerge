using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected List<GameObject> pool;
    [SerializeField] protected List<GameObject> prefabs;
    
    public GameObject GetObject()
    {
        GameObject newObj = null;

        foreach (var obj in pool)
        {
            if (!obj.activeSelf)
            {
                newObj = obj;
            }
        }
        if (newObj == null)
        {
            var prefab = prefabs[Random.Range(0, prefabs.Count)];
            var instance = Instantiate(prefab);
            pool.Add(instance);
            newObj = instance;
        }

        return newObj;
    }
}
