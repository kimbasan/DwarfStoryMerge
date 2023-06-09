using UnityEngine;

public class EnemyObjectPool : ObjectPool
{
    [SerializeField] private PlayerLeveling leveling;
    private EnemyStatsFactory enemyStatsFactory;
    private void Awake()
    {
        enemyStatsFactory = new EnemyStatsFactory(leveling);
    }

    
    public new GameObject GetObject()
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

        var enemyController = newObj.GetComponent<EnemyController>();
        enemyController.Init(enemyStatsFactory.CreateEnemyStats());

        return newObj;
    }
}
