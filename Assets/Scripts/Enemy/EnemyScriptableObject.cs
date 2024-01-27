using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    //敌人属性
    [SerializeField]
     float moveSpeed;
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    [SerializeField]
     float maxHealth;
    public float MaxHealth
    {
        get => damage;
        set => maxHealth = value;
    }

    [SerializeField]
     float damage;
    public float Damage
    {
        get => damage;
        set => damage = value;
    }






}
