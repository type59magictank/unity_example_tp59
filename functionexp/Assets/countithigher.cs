using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countithigher : MonoBehaviour
{
    [SerializeField]//允许查看私有值
    private int _num=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(nextnum);
    }
    public int nextnum//只读
    {
        get
        {
            _num++;
            return (_num);
        }
    }

    public int currentnum//读写
    {
        get { return (_num); }
        set { _num = value; }
    }

}
