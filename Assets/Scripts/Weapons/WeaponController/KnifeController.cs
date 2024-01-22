using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        //必须在内部调用自己的函数
        base.Start();
    }


   protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(prefab);
        spawnedKnife.transform.position = transform.position;
        //调用父类的pm属性赋值给 KnifeBehaviour父类的方法
        spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(pm.lsatDir);
    }
}
