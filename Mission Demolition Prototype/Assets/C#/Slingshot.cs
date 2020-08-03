using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static public Slingshot S;

    public GameObject prefabProjectile;
    public float velocityMult = 4f;
    //上面需要在检视面板设置
    public bool ___________________________;

    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!aimingMode) return;
        
        Vector3 mousePos2D = Input.mousePosition;
        //获取鼠标2d窗口位置
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        //转换为3d位置
        Vector3 mouseDelta = mousePos3D - launchPos;
        //计算坐标差
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        //限制坐标差在碰撞器半径内

        Vector3 proPos = launchPos + mouseDelta;
        projectile.transform.position = proPos;
        //移动位置
        if (Input.GetMouseButtonUp(0))
        {
            //松开鼠标
            aimingMode = false;
            projectile.GetComponent<Rigidbody>().isKinematic = false;
            projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
            FollowCam.S.poi = projectile;
            projectile = null;

            MissionDemolition.ShotFired();//直接访问静态方法
        }
    }

    private void Awake()
    {
        S = this;

        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);

        launchPos = launchPointTrans.position;

    }

    private void OnMouseEnter()
    {
        //print("Slingshot:OnMouseEnter");
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        //print("Slingshot:OnMouseExit");
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {
        aimingMode = true;
        //范围内点击鼠标
        projectile = Instantiate(prefabProjectile) as GameObject;
        //实例化弹丸
        projectile.transform.position = launchPos;
        //初始位置
        projectile.GetComponent<Rigidbody>().isKinematic = true;
        //设置属性
    }

}
