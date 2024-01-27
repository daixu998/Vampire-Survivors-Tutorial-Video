using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptabObject weaponData;
    public float destroyAfterSeconds;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int   currentPierce;
    private void Awake()
    {
        currentDamage = weaponData.Damge;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
        // destroyAfterSeconds = weaponData.DestroyAfterSeconds;
    }

    
    //获取武器强度
    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().currentMight;
    }
    protected  virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    protected  virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemyStats = other.GetComponent<EnemyStats>();
                enemyStats.TakeDamage(GetCurrentDamage());
        }else if (other.CompareTag("Prop"))
        {
            if (other.gameObject.TryGetComponent(out breakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());

            }

        }
    }
}
