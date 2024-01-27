using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerStats : MonoBehaviour
{
     CharacterScriptableObject characterData;

    // [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float CurrentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    // [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnet;

    public int experience = 0;
    public int level;
    public int experienceCap = 100;
    // public int experienceCapIncrease;

        public GameObject firstPassiveItemTest,secondPassiveItemTest;

    
    
    [System.Serializable]
    public class  LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    //受伤害后的无敌时间
    public float invincibilityDuration;
    float invincibilityTime;
    bool isInvincible;


    public List<LevelRange> levelRanges;

    InventManager inventManager;
    public int weaponIndex;
    public int passiveItemIndex;


    private void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        inventManager = GetComponent<InventManager>();


        currentHealth =characterData.MaxHealth;
        CurrentRecovery =characterData.Recovery;
        currentMoveSpeed =characterData.MoveSpeed;
        currentMight =characterData.Might;
        currentProjectileSpeed =characterData.ProjectileSpeed;
        currentMagnet = characterData.Magent;

        SpawnWeapon(characterData.StartingWeapon);
        SpawnPassiveItem(firstPassiveItemTest);
        SpawnPassiveItem(secondPassiveItemTest);
    }
    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
    }
    void Update()
    {
        if (invincibilityTime >0)
        {
            invincibilityTime -= Time.deltaTime;
        }else if (isInvincible)
        {
            isInvincible = false;
        }

        Recover();
    }
    //经验累积
    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }
    
    //升级方法
    void LevelUpChecker()
    {
        if (experience >= experienceCap)//当前经验大于登记经验就升级
        {
            level++;
            experience -= experienceCap;
            int experienceCapIncrease = 0;
            foreach (LevelRange Range in levelRanges)
            {
                if (level >= Range.startLevel &&level <= Range.endLevel)
                {
                    experienceCapIncrease += Range.experienceCapIncrease;
                    break;
                }
            }

            experienceCap += experienceCapIncrease;

        }
    }


    //
    // //经验累积
    // public Void IncreaseExperience(int amount)
    // {
    //     experience += amount;
    //     LevelUpChecker();
    // }
    //
    // //升级方法
    // void LevelUpChecker()
    // {
    //     if (experience >= experienceCap)//当前经验大于登记经验就升级
    //     {
    //         level++;
    //         experience -= experienceCap;
    //         experienceCap += experienceCapIncrease;
    //     }
    // }

    public void TakeDamage(float dmg)
    {
        if (!isInvincible)
        {
           currentHealth -= dmg;
            invincibilityTime = invincibilityDuration;  
            isInvincible = true;
        if (currentHealth <= 0)
        {
            Kill();
        } 
        }
        

    }

    public void Kill()
    {

            Debug.Log("Killed player is dead");
    }
//加血方法
    internal void RestoreHealth(int healthToRestore)
    {

        //小于最大血量就加血
        if (currentHealth < characterData.MaxHealth)
        {
            currentHealth += healthToRestore;
            //大于最大血量就保持
            if (currentHealth> characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
        
    }


//每秒自动回血
    void Recover()
    {
        if (currentHealth<characterData.MaxHealth)
        {
            currentHealth += CurrentRecovery*Time.deltaTime;
            //数值保护
            if (currentHealth>characterData.MaxHealth)
            {
                currentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon){
        if (weaponIndex >= inventManager.weaponSlots.Count-1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }
        GameObject spawonWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        //选择武器作为初始武器
        spawonWeapon.transform.parent = transform;
        inventManager.AddWeapon(weaponIndex,spawonWeapon.GetComponent<WeaponController>());
        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem){
        if (passiveItemIndex >= inventManager.passiveItemSlots.Count-1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }
        GameObject spawonPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        //选择武器作为初始武器
        spawonPassiveItem.transform.parent = transform;
        inventManager.AddPassiveItem(passiveItemIndex,spawonPassiveItem.GetComponent<PassiveItem>());
        passiveItemIndex++;
    }
}
