using System;
using UnityEngine;

public class EnemyDespawner : MonoBehaviour
{
    public event EventHandler DespawnedEnemyEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.ENEMY))
        {
            collision.gameObject.SetActive(false);
            if (DespawnedEnemyEvent != null)
            {
                DespawnedEnemyEvent.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
