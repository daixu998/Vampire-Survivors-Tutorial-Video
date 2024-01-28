using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerStats : MonoBehaviour
{
    CharacterScriptableObject characterData;

    // [HideInInspector]
    float currentHealth;


    float currentRecovery;

    float currentMoveSpeed;

    float currentMight;

    float currentProjectileSpeed;

    float currentMagnet;
    #region  Current Stats Properties
    public float CurrentHealth
    {
        get => currentHealth;
        set
        {

            //检查是否改变
            if (currentHealth != value)
            {

                currentHealth = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Health" + currentHealth.ToString();
                }
            }

        }
    }

    public float CurrentRecovery
    {
        get => currentRecovery;
        set
        {

            //检查是否改变
            if (currentRecovery != value)
            {

                currentRecovery = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "currentRecovery" + currentRecovery.ToString();
                }
            }

        }
    }
    public float CurrentMoveSpeed
    {
        get => currentMoveSpeed;
        set
        {

            //检查是否改变
            if (currentMoveSpeed != value)
            {

                currentMoveSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "currentMoveSpeed" + currentMoveSpeed.ToString();
                }
            }

        }
    }
    public float CurrentMight
    {
        get => currentMight;
        set
        {

            //检查是否改变
            if (currentMight != value)
            {

                currentMight = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "currentMight" + currentMight.ToString();
                }
            }

        }
    }
    public float CurrentProjectileSpeed
    {
        get => currentProjectileSpeed;
        set
        {

            //检查是否改变
            if (currentProjectileSpeed != value)
            {

                currentProjectileSpeed = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentProjectilespeedDisplay.text = "CurrentProjectileSpeed" + currentProjectileSpeed.ToString();
                }
            }

        }
    }
    public float CurrentMagnet
    {
        get => currentMagnet;
        set
        {

            //检查是否改变
            if (currentMagnet != value)
            {

                currentMagnet = value;
                if (GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "CurrentMagnet" + currentMagnet.ToString();
                }
            }

        }
    }

    #endregion
    public int experience = 0;
    public int level;
    public int experienceCap = 100;
    // public int experienceCapIncrease;

    public GameObject firstPassiveItemTest, secondPassiveItemTest;



    [System.Serializable]
    public class LevelRange
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


        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magent;

        SpawnWeapon(characterData.StartingWeapon);
        SpawnPassiveItem(firstPassiveItemTest);
        SpawnPassiveItem(secondPassiveItemTest);
    }
    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;

        GameManager.instance.currentHealthDisplay.text = "Health:" + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery:" + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.instance.currentProjectilespeedDisplay.text = "project Speed: " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;


        GameManager.instance.AssignChosenCharacterUI(characterData);
    }
    void Update()
    {
        if (invincibilityTime > 0)
        {
            invincibilityTime -= Time.deltaTime;
        }
        else if (isInvincible)
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
                if (level >= Range.startLevel && level <= Range.endLevel)
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
            CurrentHealth -= dmg;
            invincibilityTime = invincibilityDuration;
            isInvincible = true;
            if (CurrentHealth <= 0)
            {
                Kill();
            }
        }


    }

    public void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.GameOver();
            GameManager.instance.AssignChosenweaponsAndPassiveItemsUI(inventManager.weaponUISlots,inventManager.passiveItemUISlots);
        }
        Debug.Log("Killed player is dead");
    }
    //加血方法
    internal void RestoreHealth(int healthToRestore)
    {

        //小于最大血量就加血
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += healthToRestore;
            //大于最大血量就保持
            if (CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }

    }


    //每秒自动回血
    void Recover()
    {
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;
            //数值保护
            if (CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if (weaponIndex >= inventManager.weaponSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }
        GameObject spawonWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        //选择武器作为初始武器
        spawonWeapon.transform.parent = transform;
        inventManager.AddWeapon(weaponIndex, spawonWeapon.GetComponent<WeaponController>());
        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventManager.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }
        GameObject spawonPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        //选择武器作为初始武器
        spawonPassiveItem.transform.parent = transform;
        inventManager.AddPassiveItem(passiveItemIndex, spawonPassiveItem.GetComponent<PassiveItem>());
        passiveItemIndex++;
    }
}
