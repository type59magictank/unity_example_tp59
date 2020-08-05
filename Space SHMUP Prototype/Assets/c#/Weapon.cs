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
    void Start()
    {
        collar = transform.Find("Collar").gameObject;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
