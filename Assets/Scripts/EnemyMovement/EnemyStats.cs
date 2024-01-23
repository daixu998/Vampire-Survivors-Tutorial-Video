
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private float currentMoveSpeed;
    private float currentHealth;
    private float currentDamage;

    private void Awake()
    {
        currentDamage = enemyData.Damage;
        currentHealth = enemyData.MaxHealth;
        currentMoveSpeed = enemyData.MoveSpeed;
    }

    public void TakeDamage(float dmg)
    {

        currentHealth -= dmg;
        if (currentHealth <=0)
        {
            Kill();
        }
    }

    void Kill()
    {
        Destroy(gameObject);
    }
}
