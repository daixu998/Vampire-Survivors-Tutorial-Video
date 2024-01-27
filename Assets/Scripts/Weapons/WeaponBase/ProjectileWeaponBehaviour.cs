using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptabObject weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentColldownDuration;
    //武器穿刺次数
    protected int    currentPierce;

    private void Awake()
    {
        currentDamage = weaponData.Damge;
        currentSpeed = weaponData.Speed;
        currentColldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject,destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;
        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;


        var rot = Mathf.Atan2(direction.y, direction.x) * 180f / Mathf.PI;
        transform.rotation = Quaternion.Euler(new Vector3(0,0,rot - 45f));
        
        // if (dirx < 0 && diry == 0)
        // {
        //     transform.rotation = Quaternion.Euler(new Vector3(0,0,135f));
        // }
        // else  if (dirx < 0 && diry > 0)
        // {
        //     transform.rotation = Quaternion.Euler(new Vector3(0,0,90f));
        // }
        // else  if (dirx == 0 && diry > 0)
        // {
        //     transform.rotation = Quaternion.Euler(new Vector3(0,0,45f));
        // }
        // else  if (dirx > 0 && diry > 0)
        // {
        //     transform.rotation = Quaternion.Euler(new Vector3(0,0,0f));
        // }
        // else  if (dirx > 0 && diry == 0)
        // {
        //     transform.rotation = Quaternion.Euler(new Vector3(0,0,-45f));
        // }
        // else  if (dirx > 0 && diry < 0)
        // {
        //     transform.rotation = Quaternion.Euler(new Vector3(0,0,-90f));
        // }
        //
        // else  if (dirx == 0 && diry < 0)
        // {
        //     transform.rotation = Quaternion.Euler(new Vector3(0,0,-135f));
        // }
        // else  if (dirx <0 && diry<0)
        // {
        //     transform.rotation = Quaternion.Euler(new Vector3(0,0,-180f));
        // }
        
    }

    protected  virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage);
            ReducePierce();
        }else if (other.CompareTag("Prop"))
        {
            if (other.gameObject.TryGetComponent(out breakableProps breakable))
            {
                breakable.TakeDamage(currentDamage);
                ReducePierce();
            }

        }
       
    }
//武器攻击到次数后自毁
    void ReducePierce()
    {
        currentPierce--;
        if (currentPierce<=0)
            
        {
            Destroy(gameObject);
        }
    }
}
