using System;
using UnityEngine;

public class SideDespawner : MonoBehaviour
{
    public event EventHandler DespawnedPlatformEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.PLATFORM))
        {
            collision.gameObject.SetActive(false);
            if (DespawnedPlatformEvent != null) {
                DespawnedPlatformEvent.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
