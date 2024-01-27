using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    //注释
    [Header("Weapon Stats")] 
    public WeaponScriptabObject weaponData;
    

    
    //系统当前冷却时间
    private float currentCooldown;
    

    protected PlayerMovement pm;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        //设置初始冷却时间
        currentCooldown = weaponData.CooldownDuration;
        // Destroy(gameObject,weaponData.);
    }

    // Update is called once per frame
   protected virtual void Update()
    {
        
        //执行冷却时间
        currentCooldown -= Time.deltaTime;

        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }


    protected  virtual void Attack()
    {
        
        //刷新冷却时间
        currentCooldown =weaponData.CooldownDuration;
        
    }
}
