using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_1 : MonoBehaviour
{
    

    bool ____________________________________;

    List<List<string>> enemy_info = new List<List<string>>();
    List<List<int>> map = new List<List<int>>();
    float health;
    float speed;
    int m, n;


    Vector3 now_w = new Vector3();
    Vector3 xiangdui_ = new Vector3();
    Vector3 end_w = new Vector3();
    Vector3 next_w = new Vector3();
    int[] w_r = new int[4] { 0, 0, 1, -1 };
    int[] w_l = new int[4] { 1, -1, 0, 0 };
    bool[,] map_bool=new bool[100,100];
    bool find_way = false;

    bool finish_w = true;
    Vector3 go_way;
    Vector3 speed_move;

    // Start is called before the first frame update
    void Start()
    {
        List<List<string>> enemy_num_info = new List<List<string>>();
        TextAsset textAsset = (TextAsset)Resources.Load("enemy1");
        string[] enemy_info_string = textAsset.text.Trim().Split('\n');
        //int enemy_info_cells = 0;
        for (int i = 0; i < enemy_info_string.Length; i++)
        {
            List<string> enemy_row = new List<string>(enemy_info_string[i].Split(','));
            enemy_num_info.Add(enemy_row);
        }
        enemy_info = enemy_num_info;
        health = float.Parse(enemy_info[1][1]);
        speed = float.Parse(enemy_info[2][1]);
        now_w = new Vector3(int.Parse(enemy_info[3][2]) - 1, 0.5f, int.Parse(enemy_info[3][1]) - 1);
        xiangdui_ = create_map.S.xiangdui.transform.position;
        end_w= new Vector3(int.Parse(enemy_info[4][2]) - 1, 0.5f, int.Parse(enemy_info[4][1]) - 1);
        next_w = Findway_deep();
        m = create_map.S.m;
        n = create_map.S.n;
    }

    // Update is called once per frame
    void Update()
    {
        map = create_map.S.map_lei;
        if (finish_w == true)
        {
            now_w = this.transform.position-xiangdui_;
            go_way = Findway_deep();
            finish_w = false;
            speed_move = (go_way - this.transform.position) / (100/speed);
            Debug.Log(go_way);
        }
        else
        {
            this.transform.position = this.transform.position + speed_move ;
            if (Abs(this.transform.position.x - go_way.x)<0.01f && Abs(this.transform.position.z - go_way.z)<0.01f)
            {
                finish_w = true;
            }
            //Debug.Log(Abs(this.transform.position.x - go_way.x)+ Abs(this.transform.position.x - go_way.x) < 0.01f);
        }

    }

    float Abs(float x)
    {
        if (x < 0) return -x;
        else return x;
    }

    Vector3 Findway_deep()
    {
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
            {
                map_bool[i, j] = false;
            }
        find_way = false;
        map_bool[(int)now_w.x, (int)now_w.z] = true;
        
        return Search_deep(now_w,0)+xiangdui_;
    }

    Vector3 Search_deep(Vector3 now,int cen)
    {
        //Debug.Log(now+ "   now");
        int x = (int)now.x;
        int y = (int)now.z;
        for (int i = 0; i < 4; i++)
        {
            if (x + w_l[i]>=0&& x + w_l[i]<m&& y + w_r[i]>=0&& y + w_r[i]<n&&
                map_bool[x+w_l[i],y+w_r[i]]==false&&map[x + w_l[i]][ y + w_r[i]] == 0)
            {
                map_bool[x + w_l[i], y + w_r[i]] = true;
                Search_deep(new Vector3(x + w_l[i], 0.5f, y + w_r[i]),cen+1);
                map_bool[x + w_l[i], y + w_r[i]] = false;
            }else if (x + w_l[i] >= 0 && x + w_l[i] < m && y + w_r[i] >= 0 && y + w_r[i] < n &&
                map[x + w_l[i]][y + w_r[i]] == -2)
            {
                find_way = true;
                return Vector3.zero;
            }
            if (find_way == true && cen == 0)
            {
                return new Vector3(x + w_l[i], 0.5f, y + w_r[i]);
            }
            else return Vector3.zero;
        }
        return Vector3.zero;
    }

}
