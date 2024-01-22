using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSeconds;
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
}
