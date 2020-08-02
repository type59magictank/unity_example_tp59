using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class simple : MonoBehaviour
{
    // Start is called before the first frame update

    public List<string>  ls;
    public string[] sa;
    void Start()
    {
        ls = new List<string>();
        ls.Add("waht");
        ls.Add("???");
        print(ls.Count);
        print(ls[0]);
        for (int i = 1; i <= 3; i++)
        {
            print("loop" + i);
        }
        string str = "hello";
        foreach(char chr in str)
        {
            print(chr);
        }
        int p = 10;
        switch (p)
        {
            case (10):break;
        }
        List<string> sl = new List<string>();
        sl.Add("??");sl.Add("wat");

        sa = new string[10];
        sa[0] = "???"; 
        sa[1] = "what";
        string sr = "";
        foreach (string st in sa)
        {
            if (st == null) continue;
            sr += "|" + st;
        }
        print(sr);
    }

    // Update is called once per frame
    void Update()
    {
        print(ls[1]);
        //print(sl[1]);
    }
}
