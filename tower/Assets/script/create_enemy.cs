using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_enemy : MonoBehaviour
{
    public List<GameObject> enemy;
    public GameObject enemy_fa;
    public GameObject xiangdui_;

    bool _________________________________;

    List<List<List<string>>> enemy_info = new List<List<List<string>>>();
    float start_time=0;
    int[] time_enemy_int = new int[10];
    Vector3 xiangdui =new Vector3();


    // Start is called before the first frame update
    void Start()
    {
        start_time = Time.time;
        for (int num_i = 0; num_i < 1; num_i++)
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
            //Instantiate<GameObject>(enemy[num_i], enemy_fa.transform);
            enemy_info.Add(enemy_num_info);
            time_enemy_int[num_i] = 1;
        }

        xiangdui = xiangdui_.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        float time_now = Time.time - start_time;
        for (int num_i = 0; num_i < 1; num_i++)
        {
            if (time_enemy_int[num_i] < enemy_info[num_i][0].Count && (float)time_now>float.Parse(enemy_info[num_i][0][time_enemy_int[num_i]]))
            {
                time_enemy_int[num_i]++;
                GameObject enemy_test = Instantiate<GameObject>(enemy[num_i], enemy_fa.transform);
                Vector3 where_e= new Vector3(int.Parse(enemy_info[num_i][3][2])-1, 0.5f, int.Parse(enemy_info[num_i][3][1])-1) + xiangdui;
                enemy_test.transform.position = where_e;
                //Debug.Log(where_e);
            }
        }
    }
}
