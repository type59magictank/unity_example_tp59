using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class code : MonoBehaviour
{
    public int num=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        num++;
        countup();
    }

    void countup()
    {
        string op = "cishu:" + num;
        print(op);
    }
}
