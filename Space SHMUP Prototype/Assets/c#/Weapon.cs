using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    none,
    blaster,//高爆弹武器
    spread,//双发
    phaser,//波形
    missile,//制导
    laser,//随时间加伤害
    shield//加护盾
}

[System.Serializable]//通知unity查看类
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public string letter;
    public Color color = Color.white;
    public GameObject projectilePrefab;//子弹预设
    public Color projectileColor = Color.white;
    public float damageOnHit = 0;//伤害
    public float continuesDamage = 0;//每秒伤害
    public float delayBetweenShots = 0;
    public float velocity = 20;//子弹速度
}
public class Weapon : MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;

    public bool ___________________________;

    [SerializeField]
    private WeaponType _type = WeaponType.blaster;
    public WeaponDefinition def;
    public GameObject collar;
    public float lastShot;

    // Start is called before the first frame update

    private void Awake()
    {
        collar = transform.Find("Collar").gameObject;//在start中竟态条件
    }

    void Start()
    {
        

        SetType(_type);

        if (PROJECTILE_ANCHOR == null)
        {
            GameObject go = new GameObject("_PROJECTILE_ANCHOR");
            PROJECTILE_ANCHOR = go.transform;
        }

        GameObject parentGo = transform.parent.gameObject;
        if (parentGo.tag == "Hero")
        {
            Hero.S.fireDelegate += Fire;
        }//查找父对象委托函数fireDelegate

    }

    public WeaponType type
    {
        get
        {
            return (_type);
        }
        set
        {
            SetType(value);
        }
    }

    public void SetType(WeaponType wt)
    {
        _type = wt;
        if (type == WeaponType.none)
        {
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }
        def = Main.GetWeaponDefinition(_type);
        collar.GetComponent<Renderer>().material.color = def.color;
        lastShot = 0;
    }

    public void Fire()
    {
        if (!gameObject.activeInHierarchy) return;
        //未激活返回
        if (Time.time - lastShot < def.delayBetweenShots)
        {
            return;
        }//射击时间间隔

        Projectile p;
        switch (type)
        {
            case WeaponType.blaster:
                p = MakeProjectile();
                p.GetComponent<Rigidbody>().velocity = Vector3.up * def.velocity;
                break;
            case WeaponType.laser:
                p = MakeProjectile();
                p.GetComponent<Rigidbody>().velocity = Vector3.up * def.velocity;
                p = MakeProjectile();
                p.GetComponent<Rigidbody>().velocity = new Vector3(-.2f, 0.9f, 0) * def.velocity;
                p = MakeProjectile();
                p.GetComponent<Rigidbody>().velocity = new Vector3(.2f, 0.9f, 0) * def.velocity;
                break;
        }
    }

    public Projectile MakeProjectile()
    {
        GameObject go = Instantiate(def.projectilePrefab) as GameObject;
        if (transform.parent.gameObject.tag == "Hero")
        {
            go.tag = "ProjectileHero";
            go.layer = LayerMask.NameToLayer("ProjectileHero");
        }
        else
        {
            go.tag = "ProjectileEnemy";
            go.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }
        go.transform.position = collar.transform.position;
        go.transform.parent = PROJECTILE_ANCHOR;
        Projectile p = go.GetComponent<Projectile>();
        p.type = type;
        lastShot = Time.time;
        return (p);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
