using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // public EnemyScriptableObject EnemyData;

    EnemyStats enemy;
    private Transform player;
    

    
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, enemy.currentMoveSpeed * Time.deltaTime);
    }
}
