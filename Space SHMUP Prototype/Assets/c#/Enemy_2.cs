using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    //正弦曲线加平滑的线性插值
    //u=u+0.6*sin（2Π*u）
    public Vector3[] points;
    public float birthTime;
    public float lifeTime=10;
    public float sinEccentricity = 0.6f;//正弦波形对运动影响程度

    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[2];

        Vector3 cbMin = Utils.camBounds.min;
        Vector3 cbMax = Utils.camBounds.max;
        //找到边界
        Vector3 v = Vector3.zero;

        v.x = cbMin.x - Main.S.enemySpawnPadding;
        v.y = Random.Range(cbMin.y, cbMax.y);
        points[0] = v;
        //屏幕左侧选取一点
        v = Vector3.zero;
        v.x = cbMax.x + Main.S.enemySpawnPadding;
        v.y = Random.Range(cbMin.y, cbMax.y);
        points[1] = v;
        //屏幕右侧一点
        if (Random.value < 0.5f)
        {
            points[0].x *= -1;
            points[1].x *= -1;
        }

        birthTime = Time.time;

    }

    public override void Move()
    {
        float u = (Time.time - birthTime) / lifeTime;

        if (u > 1)//时间间隔大于lifeTime
        {
            Destroy(this.gameObject);
            return;
        }

        u = u + sinEccentricity * (Mathf.Sin(u * Mathf.PI * 2));
        //通过叠加一个基于正弦曲线的平滑曲线调整u值
        pos = (1 - u) * points[0] + u * points[1];
        //两点间插值
    }

}
