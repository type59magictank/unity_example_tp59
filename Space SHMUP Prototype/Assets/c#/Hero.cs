﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S;

    public float gameRestartDelay = 2f;
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;

    [SerializeField]
    private float _shieldLevel = 1;

    public Weapon[] weapons;//武器字段

    public bool _____________________________;

    public Bounds bounds;

    public delegate void WeaponFireDelegate();//创建委托类型（函数授权
    public WeaponFireDelegate fireDelegate;

    private void Awake()
    {
        S = this;

        bounds = Utils.CombinBoundsOfChildren(this.gameObject);

        
    }

    // Start is called before the first frame update
    void Start()
    {
        ClearWeapons();//重置武器
        weapons[0].SetType(WeaponType.blaster);
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");

        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        bounds.center = transform.position;

        Vector3 off = Utils.ScreenBoundsCheck(bounds, BoundsTest.onScreen);

        if (off != Vector3.zero)
        {
            pos -= off;
            transform.position = pos;
        }

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * pitchMult, 0);
        //使飞船旋转角度

        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {
            fireDelegate();
        }
    }

    public GameObject lastTiggerGo = null;

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = Utils.FindTaggedParent(other.gameObject);

        if (go != null)
        {
            if (go == lastTiggerGo)
            {
                return;
            }
            //确保触发对象与之前不同
            lastTiggerGo = go;

            if (go.tag == "Enemy")
            {
                shieldLevel -= 1;
                //对象是敌人将护盾-1
                Destroy(go);
                
            }else if (go.tag == "PowerUp")
            {
                AbsorbPowerUp(go);
                //护盾被升级道具触发
            }
            else
            {
                print("触发碰撞事件：" + go.name);

            }

        }
        else
        {
            print("触发碰撞事件：" + other.gameObject.name);

        }
        //print("触发碰撞事件：" + other.gameObject.name);
    }

    public float shieldLevel
    {
        get
        {
            return (_shieldLevel);
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);//出现过错误：重复赋值堆栈溢出meiyou_

            if (value < 0)
            {
                Destroy(this.gameObject);

                Main.S.DelayedRestart(gameRestartDelay);

            }
        }
    }

    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();

        switch (pu.type)
        {
            case WeaponType.shield:
                shieldLevel++;
                break;
            default:
                if (pu.type == weapons[0].type)
                {
                    Weapon w = GetEmptyWeaponSlot();
                    if (w != null)
                    {
                        w.SetType(pu.type);
                        //把它赋值给pu
                    }
                }
                else
                {
                    //不同武器类型
                    ClearWeapons();
                    weapons[0].SetType(pu.type);
                }
                break;
        }
        pu.AbsorbedBy(this.gameObject);

    }

    Weapon GetEmptyWeaponSlot()//找到空白武器位置
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == WeaponType.none)
            {
                return (weapons[i]);
            }
        }
        return (null);
    }

    void ClearWeapons()
    {
        foreach(Weapon w in weapons)
        {
            w.SetType(WeaponType.none);
        }
    }
}
