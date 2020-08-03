using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static public MissionDemolition S;

    public GameObject[] castles;
    public UnityEngine.UI.Text gtLevel;
    public UnityEngine.UI.Text gtScore;
    public Vector3 castlePos;

    public bool _______________________________;

    public int level;
    public int levelMax;
    public int shotsTaken;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Slingshot";

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        level = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel()
    {
        if (castle != null)//清楚原有城堡
        {
            Destroy(castle);
        }
        //清楚子弹
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach(GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }
        //实例化新城堡
        castle = Instantiate(castles[level]) as GameObject;
        castle.transform.position = castlePos;
        shotsTaken = 0;
        //重置摄像机位置
        SwitchView("Both");
        ProjectileLine.S.Clear();
        //重置目标状态
        Goal.goalMet = false;
        ShowGT();
        mode = GameMode.playing;
    }

    void ShowGT()
    {
        //界面文字设置（在检视面板绑定
        gtLevel.text = "Level: " + (level + 1) + " of " + levelMax;
        gtScore.text = "Shots Taken: " + shotsTaken;
    }

    // Update is called once per frame
    void Update()
    {
        ShowGT();

        if (mode == GameMode.playing && Goal.goalMet)
        {
            mode = GameMode.levelEnd;

            SwitchView("Both");

            Invoke("NextLevel", 2f);//2秒下一个
        }
    }

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    private void OnGUI()//绘制用户界面按钮，用于切换视图
    {
        Rect buttonRect = new Rect((Screen.width / 2) - 50, 10, 100, 24);
        switch (showing)
        {
            case "Slingshot":
                if (GUI.Button(buttonRect, "查看城堡"))
                {
                    SwitchView("Castle");
                }
                break;
            case "Castle":
                if (GUI.Button(buttonRect, "查看全部"))
                {
                    SwitchView("Both");
                }
                break;
            case "Both":
                if (GUI.Button(buttonRect, "查看弹弓"))
                {
                    SwitchView("Slingshot");
                }
                break;
        }

    }
    
    static public void SwitchView(string eView)//在代码任意位置切换视图
    {
        S.showing = eView;
        switch (S.showing)
        {
            case "Slingshot":
                FollowCam.S.poi = null;
                break;
            case "Castle":
                FollowCam.S.poi = S.castle;
                break;
            case "Both":
                FollowCam.S.poi = GameObject.Find("ViewBoth");
                break;
        }

    }

    public static void ShotFired()
    {
        S.shotsTaken++;
    }

}
