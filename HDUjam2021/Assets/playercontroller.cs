using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    static public playercontroller S;
    public long timei=0;
    public GameObject light_;
    public GameObject main;
    public GameObject mainout;

    //MeshRenderer组件
    public MeshRenderer thisRenderer;
    //创建一个常量接收时间变化值
    float shankeTime = 0f;
    //是否开始闪烁
    public bool isShake = false;
    
    [SerializeField]
    public float beginscale;
    public float beginrouge;
    public float bigcount;
    public float smallcount;


    private Vector3 x;
    private int old_i=0;
    public bool trigger = false;

    private Color start;
    private int colorint = 0;
    private bool red_on = false;

    // Start is called before the first frame update
    void Start()
    {
        x = new Vector3(beginscale, beginscale, beginscale);
        light_.GetComponent<Light>().range = beginrouge;
        main.transform.localScale = x;
        start= light_.GetComponent<Light>().color;

    }

    private void FixedUpdate()
    {
        timei++;
        int i = (int)timei / 50;
        if (i  != old_i)
        {

            //i = (int)i / 5;
            x = main.transform.localScale;
                x = x + Vector3.one*bigcount;
                light_.GetComponent<Light>().range = light_.GetComponent<Light>().range + bigcount*3;
                light_.GetComponent<Light>().intensity = light_.GetComponent<Light>().intensity + bigcount;

                //main.GetComponent<SphereCollider>().radius = main.GetComponent<SphereCollider>().radius * bigcount;
                main.transform.localScale = x;
            //float y = main.GetComponent<SphereCollider>().radius;
            
            //main.GetComponent<SphereCollider>().radius = x.x;//半径
                old_i = i;

                //isShake = false;

        }
        colorint++;
        if (trigger == false)
        {
            if (colorint > 15)
            {
                light_.GetComponent<Light>().color = start;
                colorint = 0;
            }
            
        }
        else
        {
            //isShake = true;
            light_.GetComponent<Light>().color = Color.red;
            main.transform.localScale = x*0.99f;
            trigger = false;
        }


    }

    // Update is called once per frame
    void Update()
    {
        ToChangeColor();
    }

    void ToChangeColor()
    {
        if (isShake)
        {
            Debug.LogError("-1");
            shankeTime += Time.deltaTime;
            if (shankeTime % 1 > 0.1f)
            {
                thisRenderer.material.color = Color.blue;
            }
            else
            {
                thisRenderer.material.color = Color.white;
            }
        }
    }

}

