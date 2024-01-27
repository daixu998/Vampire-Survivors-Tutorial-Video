using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour , ICollectible
{
    public int healthToRestore;

    public void Collect()
    {
       PlayerStats  playerStats = FindObjectOfType<PlayerStats>();
       playerStats.RestoreHealth(healthToRestore);
       Destroy(gameObject); // destroy the potion when collected
    }


}
