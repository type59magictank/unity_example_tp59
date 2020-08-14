﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;

    public int showDamageForFrames = 2;//伤害效果帧数
    public float powerUpDropChance = 1f;//掉落道具概率

    public bool _____________________________;

    public Color[] originalColors;
    public Material[] materials;//对象以及子对象所有材质
    public int remainingDamageFrames = 0;//剩余伤害效果帧数

    public Bounds bounds;
    public Vector3 boundsCenterOffset;

    private void Awake()
    {
        materials = Utils.GetAllMaterials(gameObject);
        originalColors = new Color[materials.Length];
        for(int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }

        InvokeRepeating("CheckOffscreen", 0f, 2f);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (remainingDamageFrames > 0)
        {
            remainingDamageFrames--;
            if (remainingDamageFrames == 0)
            {
                UnShowDamage();
            }
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    void CheckOffscreen()
    {
        if (bounds.size == Vector3.zero)
        {
            bounds = Utils.CombinBoundsOfChildren(this.gameObject);

            boundsCenterOffset = bounds.center - transform.position;

        }
        bounds.center = transform.position + boundsCenterOffset;

        Vector3 off = Utils.ScreenBoundsCheck(bounds, BoundsTest.offScreen);

        if (off != Vector3.zero)
        {

            if (off.y < 0)
            {
                Destroy(this.gameObject);

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        switch (other.tag)
        {
            case "ProjectileHero":
                Projectile p = other.GetComponent<Projectile>();

                bounds.center = transform.position + boundsCenterOffset;
                if (bounds.extents == Vector3.zero || Utils.ScreenBoundsCheck(bounds, BoundsTest.offScreen) != Vector3.zero)
                {
                    Destroy(other);
                    break;
                }//检验是否进入屏幕

                health -= Main.W_DEFS[p.type].damageOnHit;
                if (health <= 0)
                {
                    Main.S.ShipDestroyed(this);//通知main对象敌机被消灭
                    Destroy(this.gameObject);
                }
                ShowDamage();
                Destroy(other);
                break;
        }
    }

    void ShowDamage()
    {
        foreach(Material m in materials)
        {
            m.color = Color.red;
        }
        remainingDamageFrames = showDamageForFrames;
    }

    void UnShowDamage()
    {
        for(int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
    }

}
