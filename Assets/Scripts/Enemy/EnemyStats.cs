
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    public float despawDistance = 20f;

    Transform player;

    private void Awake()
    {
        currentDamage = enemyData.Damage;
        currentHealth = enemyData.MaxHealth;
        currentMoveSpeed = enemyData.MoveSpeed;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
    }
    void Update()
    {

        //距离远了后飞过来
        if (Vector2.Distance(transform.position, player.position) > despawDistance)
        {
ReturnEnemy();
        }
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


        
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats playerStats = col.gameObject.GetComponent<PlayerStats>();
            playerStats.TakeDamage(currentDamage);
        }
    }

     void  ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position  + es.relativeSpawnPositions[UnityEngine.Random.Range(0,es.relativeSpawnPositions.Count)].position;
    }
}
