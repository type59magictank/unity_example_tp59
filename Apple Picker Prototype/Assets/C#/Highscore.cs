using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{

    static public int score = 0;
    //全局静态变量调用
    void Awake()
    {
        //把信息存储在计算机上
        if (PlayerPrefs.HasKey("ApplePickerHighScore"))//判断是否已经存在
        {
            score = PlayerPrefs.GetInt("ApplePickerHighScore");
        }
        //赋值
        PlayerPrefs.SetInt("ApplePickerHighScore", score);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Text gt = this.GetComponent<Text>();

        gt.text = "High Score:" + score;

        if (score> PlayerPrefs.GetInt("ApplePickerHighScore"))
        {
            PlayerPrefs.SetInt("ApplePickerHighScore", score);
        }
    }
}
