using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 :  Enemy
{
    //沿着贝塞尔曲线插值
    public Vector3[] points;
    public float birthTime;
    public float lifeTime = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[3];

        points[0] = pos;
        //初始位置已经在SpawnEnemy设置过
        float xMin = Utils.camBounds.min.x + Main.S.enemySpawnPadding;
        float xMax = Utils.camBounds.max.x + Main.S.enemySpawnPadding;

        Vector3 v;
        v = Vector3.zero;
        v.x = Random.Range(xMin, xMax);
        v.y = Random.Range(Utils.camBounds.min.y, 0);
        points[1] = v;
        //屏幕下面选择中间点
        v = Vector3.zero;
        v.y = pos.y;
        v.x = Random.Range(xMin, xMax);
        points[2] = v;
        //屏幕上部选取终点
        birthTime = Time.time;

    }

    public override void Move()
    {
        float u = (Time.time - birthTime) / lifeTime;

        if (u > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 p01, p12;
        u = u - 0.2f * Mathf.Sin(u * Mathf.PI * 2);//平滑加速
        p01 = (1 - u) * points[0] + u * points[1];
        p12=  (1 - u) * points[1] + u * points[2];
        pos = (1 - u) * p01 + u * p12;
        //在三点贝塞尔曲线上插值
    }

}
