using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : MeleeWeaponBehaviour
{
    private List<GameObject> markedEnemies;
   protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
        
    }

    // Update is called once per frame
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")&&!markedEnemies.Contains(other.gameObject))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(currentDamage);
            markedEnemies.Add(other.gameObject);
        }
        else if (other.CompareTag("Prop"))
        {
            if (other.gameObject.TryGetComponent(out breakableProps breakable)&&!markedEnemies.Contains(other.gameObject))
            {
                breakable.TakeDamage(currentDamage);
                    markedEnemies.Add(other.gameObject);
            }

        }
    }
}
