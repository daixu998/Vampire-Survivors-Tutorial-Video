using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : MonoBehaviour ,ICollectible
{

    public int experienceGranted;
    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
        // Destroy(gameObject);
    }

    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D col){

        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }
}
