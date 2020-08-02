using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movealong : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        countithigher cih = this.gameObject.GetComponent<countithigher>();
        //cih局部变量引用countithigher实例。GetComponent<countithigher>找到绑定的脚本组件
        if (cih != null)
        {
            float tx = cih.currentnum / 10f;
            Vector3 temploc = pos;
            temploc.x = tx;
            pos = temploc;
        }
    }

    public Vector3 pos
    {
        get { return (this.transform.position); }
        set { this.transform.position = value; }
    }

}
