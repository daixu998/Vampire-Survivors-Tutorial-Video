using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptabObject : ScriptableObject
{
    [SerializeField] GameObject prefab;

    public GameObject Prefab
    {
        get => prefab;
        private set => prefab = value;
    }

    [SerializeField] float damge;

    public float Damge
    {
        get => damge;
        private set => damge = value;
    }

    [SerializeField] float speed;

    public float Speed
    {
        get => speed;
        private set => speed = value;
    }

    [SerializeField] float cooldownDuration;

    public float CooldownDuration
    {
        get => cooldownDuration;
        private set => cooldownDuration = value;
    }

    [SerializeField] int pierce;

    public int Pierce
    {
        get => pierce;
        private set => pierce = value;
    }

    // destroyAfterSeconds
    [SerializeField] float destroyAfterSeconds;
    public float DestroyAfterSeconds
    {
        get => destroyAfterSeconds;
        private set => destroyAfterSeconds = value;
    }
}