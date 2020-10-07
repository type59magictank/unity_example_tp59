using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public GameObject wr, wl;
    public GameObject Wall;
    public int lengthl = 10;
    public int lengthr = 10;


    bool ___________________________________;

    int[,] map = new int[130, 130];
    float[] prefabr = new float[1000];
    float[] prefabl = new float[1000];
    int[] wherel = new int[4] { 0, 0, 1, -1 };
    int[] wherer = new int[4] { 1, -1, 0, 0 };

    //GameObject[,] wallR =new GameObject[130,130];
    //GameObject[,] wallL =new GameObject[130,130];
    GameObject[,] cwall = new GameObject[130, 130];
    //int[,] wallnnum = new int[500, 2];
    bool[,] bmap = new bool[100, 100];
    int l = 0;

    // Start is called before the first frame update
    void Start()
    {
        prefabr[0] = -lengthr+1;
        prefabl[0] = -lengthl+2;
        /*for (int i = 1; i < lengthl; i++)
        {
            prefabr[i] = prefabr[i - 1] + 2;
            prefabl[i] = prefabl[i - 1] + 2;
        }*/
        /*for (int i = 0; i < lengthl; i++)
            for (int j = 0; j < lengthr-1; j++)
            {
                GameObject go = Instantiate(wl) as GameObject;
                wallL[i + 1, j + 1] = go;
                wallL[i + 1, j + 1].transform.position = new Vector3(prefabr[i], 1, prefabl[j]);
                wallL[i + 1, j + 1].transform.parent = Wall.transform;
                go = Instantiate(wr) as GameObject;
                wallR[i + 1, j + 1] = go;
                wallR[i + 1, j + 1].transform.position = new Vector3(prefabl[j], 1, prefabr[i]);
                wallR[i + 1, j + 1].transform.parent = Wall.transform;
                
            }*/
        for (int i = 0; i < lengthr*2; i++)
            for (int j = 0; j < lengthl*2-2; j++)
            {
                if (!((i%2==1)&&(j%2==1)))
                if ((i % 2 == 0)&&(j % 2 == 0))
                {
                    
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        GameObject go = Instantiate(wl) as GameObject;
                        cwall[i, j] = go;
                        cwall[i ,j].transform.parent = Wall.transform;
                        cwall[i, j].transform.position = new Vector3(prefabr[0]+i, 1, prefabl[0]+j-1);
                        cwall[i, j].transform.name = "wall"+ i.ToString()+" "+ j.ToString();
                        
                    }
                    else
                    {
                        GameObject go = Instantiate(wr) as GameObject;
                        cwall[j, i] = go;
                        go.transform.parent = Wall.transform;
                        go.transform.position = new Vector3(prefabl[0] + j, 1,prefabr[0] + i -1);
                        go.transform.name = "wall" + j.ToString() + " " + i.ToString();
                        }
                }
            }
        {

        }

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                bmap[i, j] = false;
            }
        }

        MakeMap();


 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeMap()
    {
        //prim算法生成迷宫
        GameObject[] wallnum=new GameObject[5000];
        bmap[1, 1] = true;
        l++;
        //while(l>0)
        {
            
        }


    }

    

}
