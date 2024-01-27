using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class breakableProps : MonoBehaviour
{
    public float health;
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Kill();
            //Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
public void Kill()
{
    Destroy(gameObject);
}

}

