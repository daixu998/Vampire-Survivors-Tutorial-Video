using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;


    private float currentHealth;
    private float CurrentRecovery;
    private float currentMoveSpeed;
    private float currentMight;
    private float currentProjectileSpeed;


    public int experience = 0;
    public int level;
    public int experienceCap = 100;
    // public int experienceCapIncrease;
    
    
    [System.Serializable]
    public class  LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;
    private void Awake()
    {
        currentHealth =characterData.MaxHealth;
        CurrentRecovery =characterData.Recovery;
        currentMoveSpeed =characterData.MoveSpeed;
        currentMight =characterData.Might;
        currentProjectileSpeed =characterData.ProjectileSpeed;
    }
    private void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
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
}
