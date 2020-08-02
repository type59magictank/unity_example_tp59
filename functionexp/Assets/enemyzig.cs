using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyzig : enemy
{

    public override void move()//重写
    {
        Vector3 temppos = pos;
        temppos.x = Mathf.Sin(Time.time * Mathf.PI * 2) * 4;
        pos = temppos;
        base.move();//调用父类方法
    }

}
