using System;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    public event EventHandler EnemyEncountered;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.ENEMY))
        {
            collision.attachedRigidbody.velocity = Vector2.zero;
            if (EnemyEncountered!= null)
            {
                var enemy = collision.gameObject.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    EnemyEncountered.Invoke(this, new BattleEventArgs(enemy));
                } else
                {
                    Debug.LogError("Enemy controller not found");
                }
                
            }
        }
    }
}
