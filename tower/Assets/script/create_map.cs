﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_map : MonoBehaviour
{
    public GameObject cube_start;
    public GameObject cube_end;
    public GameObject map_;


    bool ____________________________________________;

    public List<List<int>> map_lei=new List<List<int>>();

    // Start is called before the first frame update
    void Start()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("map_1");
        string[] map_row_string = textAsset.text.Trim().Split('\n');
        int map_row_max_cells = 0;
        List<List<string>> map_Collections = new List<List<string>>();
        for (int i = 0; i < map_row_string.Length; i++)
        {
            List<string> map_row = new List<string>(map_row_string[i].Split(','));
            if (map_row_max_cells < map_row.Count)
            {
                map_row_max_cells = map_row.Count;
            }
            map_Collections.Add(map_row);
        }
        //float num_t= (float)-0.5;
        GameObject map_plane = GameObject.CreatePrimitive(PrimitiveType.Plane);//生成一个Plane
        map_plane.transform.position = new Vector3(0, 0, 0);//放到(0,0,0)这个位置
        map_plane.transform.parent = map_.transform;
        //求其原始大小
        float map_plane_original_x_size = map_plane.GetComponent<MeshFilter>().mesh.bounds.size.x;
        float map_plane_original_z_size = map_plane.GetComponent<MeshFilter>().mesh.bounds.size.z;
        //缩放这个Map到所需大小，刚好和二维表匹配
        float map_plane_x = map_row_max_cells / map_plane_original_x_size;
        float map_plane_z = map_Collections.Count / map_plane_original_z_size;
        map_plane.transform.localScale = new Vector3(map_plane_x, 1, map_plane_z);

        /*在Plane上放Cube*/
        for (int i = 0; i < map_Collections.Count; i++)//Z方向是长度就是容器的大小，也就是map.csv有多少有效的行
        {
            List<int> map__row = new List<int>();

            for (int j = 0; j < map_Collections[i].Count; j++)
            {//X方向的宽度就是容器一行中的最大的长度，也就是map.csv中每行最大长度
                int cube_num = int.Parse(map_Collections[i][j]);//将每个单元格的数字转换成整形  

                map__row.Add(cube_num);

                for (int k = 0; k < cube_num; k++)
                {//根据数字，在一个单元格内生成cube
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(-(map_row_max_cells / 2) + i, (float)0.5 + k, -(map_Collections.Count / 2) + j);
                    cube.transform.parent = map_.transform;
                    /*
                     cube所处的坐标就是(-(map_row_max_cells / 2) + i, (float)0.5 + k, -(map_Collections.Count / 2) + j)
                     */
                }
                
                if (cube_num == -1)
                {
                    Instantiate<GameObject>(cube_start, map_.transform);
                    cube_start.transform.position = new Vector3(-(map_row_max_cells / 2) + i, (float)0.5 , -(map_Collections.Count / 2) + j);
                }

                if (cube_num == -2)
                {
                    Instantiate<GameObject>(cube_end, map_.transform);
                    cube_end.transform.position = new Vector3(-(map_row_max_cells / 2) + i, (float)0.5, -(map_Collections.Count / 2) + j);
                }
                
            }

            map_lei.Add(map__row);

        }
     }

        // Update is called once per frame
        void Update()
    {
        
    }
}
