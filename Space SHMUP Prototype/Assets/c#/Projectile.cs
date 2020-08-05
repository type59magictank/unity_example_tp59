using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private WeaponType _type;

    public WeaponType type
    {
        get
        {
            return (_type);
        }
        set
        {
            
        }
    }

    private void Awake()
    {
        InvokeRepeating("CheckOffScreen", 2f, 2f);//检查本对象是否离开屏幕
    }

    public void SetType(WeaponType eType)
    {
        _type = eType;

        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        this.GetComponent<Renderer>().material.color = def.projectileColor;
    }

    void CheckOffscreen()
    {
        if (Utils.ScreenBoundsCheck(this.GetComponent<Collider>().bounds, BoundsTest.offScreen) != Vector3.zero)
        {
            Destroy(this.gameObject);
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
