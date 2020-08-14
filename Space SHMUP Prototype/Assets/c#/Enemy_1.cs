using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy//enemy1是enemy派生类
{

    public float waveFrequency = 2;
    //正弦周期时间
    public float waveWidth = 4;
    public float waveRoty = 45;
    private float x0 = -12345;
    private float birthTime;

    // Start is called before the first frame update
    void Start()
    {
        x0 = pos.x;
        //设置初始坐标
        birthTime = Time.time;
    }
    //子类会覆盖父类函数
    //子类会覆盖父类函数
    //子类会覆盖父类函数
    // Update is called once per frame
    public override void Move()
    {
        Vector3 tempPos = pos;

        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = x0 + waveWidth * sin;
        pos = tempPos;
        //基于时间调整theta值
        Vector3 rot = new Vector3(0, sin * waveRoty, 0);
        this.transform.rotation = Quaternion.Euler(rot);
        //绕y轴稍微旋转
        base.Move();
    }

}
