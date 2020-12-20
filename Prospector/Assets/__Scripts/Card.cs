using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Set dynamically")]
    public string suit;//花色
    public int rank;//点数
    public Color color = Color.black;//花色符号颜色
    public string colS = "Black";//颜色

    //存储所有decorator对象
    public List<GameObject> decoGOs = new List<GameObject>();
    //存储所有pip对象
    public List<GameObject> pipGOs = new List<GameObject>();

    public GameObject back;//纸牌背面

    public CardDefinition def;

    public bool faceUp
    {
        get
        {
            return (!back.activeSelf);
        }
        set
        {
            back.SetActive(!value);
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


[System.Serializable]//序列化类
public class Decorator
{
    //存储来自deckxml信息
    public string type;//花色符号=pip
    public Vector3 loc;//spite在牌面位置
    public bool flip = false;//是否垂直翻转
    public float scale = 1f;//spite缩放比例
}

[System.Serializable]
public class CardDefinition
{
    //存储点数牌面信息
    public string face;//花牌（JQK）用的sprite
    public int rank;//点数1-13
    public List<Decorator> pips = new List<Decorator>();//所用花色
}
