using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    const int mapm = 50;
    const int mapn = 50;
    static int[,] way = { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };
    public static int[,] MapCreaterPrim(int x1,int y1,int x2,int y2)
    {
        int[,] mapa = new int[mapm, mapn];
        List <int[]> wallbag = new List<int[]>();
        int[,] pointmap = new int[mapm, mapn];
        for (int i = 0; i < mapm; i++)
        {
            for (int j = 0; j < mapn; j++)
            {
                if (i%2==1&&j%2==1)
                {
                    mapa[i, j] = 1;
                }else
                mapa[i, j] = 0;
                if (i == 0 || i == mapm - 1 || j == 0 || j == mapn - 1)
                {
                    mapa[i, j] = -1;
                }
            }
        }
        int[] start = { x1, y1 };
        int[] end = { x2, y2 };
        for (int we = 0; we < 4; we++)
        {
            if (mapa[start[0] + way[we, 0], start[1] + way[we, 1]] == 0)
            {
                int[] k = {start[0] +way[we, 0], start[1] + way[we, 1]};
                wallbag.Add(k);

            }
        }
        while (true)
        {

        }
        return (mapa);
    }

    

}
